using DataMeshGroup.Fusion.Model;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DataMeshGroup.Fusion
{
    public class FusionClient : IFusionClient
    {
        private WebSocket ws;

        /// <summary>
        /// Token used to manage when the socket is supposed to be connected
        /// </summary>
        private CancellationTokenSource cts;

        /// <summary>
        /// A token fired when the socket is disconnected. This allows any 
        /// listens to stop listening and handle the network error 
        /// </summary>
        private CancellationTokenSource socketCloseCTS; // TODO: Check if this and cts and can wrapped into one

        /// <summary>
        /// Indicates if a login is required. Set to 'true' by default, and after 
        /// a disconnect. Set to 'false' after a successful login response
        /// </summary>
        private bool loginRequired = true;

        /// <summary>
        /// Temp storage for a request when doing an auto login
        /// </summary>
        private MessagePayload parkedRequestMessage;

        /// <summary>
        /// Temp storage for a serviceID when doing an auto login
        /// </summary>
        private string parkedServiceID;

        /// <summary>
        /// Temp storage for the cancellationToken when doing an auto login
        /// </summary>
        private CancellationToken parkedCancellationToken;

        /// <summary>
        /// Used to signal an available message in the receive queue
        /// </summary>
        private readonly SemaphoreSlim recvQueueSemaphore;

        /// <summary>
        /// Receive queue for messages when in async mode
        /// </summary>
        private readonly Queue<MessagePayload> recvQueue;

        public IWebSocketFactory WebSocketFactory { get; set; } = new DefaultWebSocketFactory();

        /// <summary>
        /// Constructs a client which can be used to communicate with the DataMesh Unify payments system
        /// </summary>
        /// <param name="useTestEnvironment">True to default to test environment params, false for production.</param>
        public FusionClient(bool useTestEnvironment = false)
        {
            MessageParser = new NexoMessageParser() { UseTestKeyIdentifier = useTestEnvironment, EnableMACValidation = true };
            URL = useTestEnvironment ? UnifyURL.Test : UnifyURL.Production;
            DefaultTimeout = TimeSpan.FromSeconds(60);
            DefaultHeartbeatTimeout = TimeSpan.FromSeconds(15);
            LoginRequest = null;
            LoginResponse = null;
            ReceiveBufferSize = 1024;
            LogLevel = LogLevel.None;
            recvQueueSemaphore = new SemaphoreSlim(0);
            recvQueue = new Queue<MessagePayload>();
            RootCA = useTestEnvironment ? UnifyRootCA.Test : UnifyRootCA.Production;

            ServicePointManager.ServerCertificateValidationCallback += CertificateValidation.RemoteCertificateValidationCallback; // unsubscribe in dispose
        }

        /// <summary>
        /// Sets ServiceId to a hex timestamp of the milliseconds since 1st January
        /// Potential issue if sending > 1 transaction per MS but should not be an issue here
        /// </summary>
        public string UpdateServiceId()
        {
            ServiceID = Convert.ToInt64((DateTime.UtcNow - new DateTime(DateTime.UtcNow.Year, 1, 1)).TotalMilliseconds).ToString("X", System.Globalization.CultureInfo.InvariantCulture);
            return ServiceID;
        }

        /// <summary>
        /// Connects to the <see cref="URL"/>, or <see cref="CustomURL"/> if <see cref="URL"/> is <see cref="UnifyURL.Custom"/>
        /// </summary>
        public async Task<bool> ConnectAsync()
        {
            string urlString = null;
            try
            {
                if (ws != null)
                {
                    if (ws.State == WebSocketState.Open)
                    {
                        return true;
                    }

                    ws.Dispose();
                }

                if (cts != null)
                {
                    cts.Dispose();
                }
                cts = new CancellationTokenSource();


                if (socketCloseCTS != null)
                {
                    socketCloseCTS.Dispose();
                }
                socketCloseCTS = new CancellationTokenSource();


                CertificateValidation.RootCA = RootCA;

                switch (URL)
                {
                    case UnifyURL.Test:
                        urlString = "wss://www.cloudposintegration.io/nexouat1";
                        break;
                    case UnifyURL.Production:
                        urlString = "wss://prod1.datameshgroup.io:5000";
                        break;
                    case UnifyURL.Custom:
                        urlString = CustomURL;
                        break;
                    default:
                        throw new NotImplementedException();
                };

                if (!Uri.TryCreate(urlString, UriKind.Absolute, out Uri url))
                {
                    throw new ArgumentException($"Invalid URL {urlString}", nameof(URL));
                }

                Log(LogLevel.Debug, $"Connecting to {url}...");
                ws = await WebSocketFactory.ConnectAsync(url, DefaultHeartbeatTimeout, cts.Token);
                bool isConnected = ws.State == WebSocketState.Open;

                if (isConnected)
                {
                    OnConnect?.Invoke(this, EventArgs.Empty);
                    RecvLoop();
                }

                Log(LogLevel.Information, $"Connected = {isConnected}");
                return isConnected;
            }
            catch (Exception e) // TODO handle possible errors better here
            {
                Log(LogLevel.Error, $"A network error occured connecting to {urlString}.", e);
                OnConnectError?.Invoke(this, EventArgs.Empty);
                throw new NetworkException(e.Message, e.InnerException);
            }
        }

        /// <summary>
        /// Disconnects and releases resources
        /// </summary>
        public async Task DisconnectAsync()
        {
            Log(LogLevel.Debug, $"Disconnecting...");
            loginRequired = true;

            try
            {
                if (ws is null)
                {
                    return;
                }

                if (socketCloseCTS != null && !socketCloseCTS.IsCancellationRequested)
                {
                    socketCloseCTS.Cancel();
                }

                if (ws.State == WebSocketState.Open)
                {
                    cts?.CancelAfter(TimeSpan.FromSeconds(2));

                    // this will send a socket close request for a clean socket disconnect it may 
                    // throw an exception if the transport stream is closed, but we still need to 
                    // ensure the socket itself is closed
                    try
                    {
                        await ws.CloseOutputAsync(WebSocketCloseStatus.Empty, "", CancellationToken.None);
                    }
                    finally
                    {
                        await ws.CloseAsync(WebSocketCloseStatus.NormalClosure, "", CancellationToken.None);
                    }
                }
            }
            catch (Exception e)
            {
                Log(LogLevel.Error, $"Exception occured in DisconnectAsync()", e);
            }
            finally
            {
                ws?.Dispose();
                ws = null;
                cts?.Dispose();
                cts = null;
            }

            OnDisconnect?.Invoke(this, EventArgs.Empty);
            Log(LogLevel.Information, $"Disconnected");
        }

        /// <summary>
        /// Ensures we are connected, and a successful login has occured. 
        /// </summary>
        /// <returns></returns>
        /// <exception cref="ArgumentException">Thrown if a login is required, yet LoginRequest is null</exception>
        private async Task<bool> EnsureConnectedAndLoginComplete(bool isLoginInProgress, System.Threading.CancellationToken cancellationToken)
        {
            _ = await ConnectAsync();

            if (!loginRequired || isLoginInProgress)
            {
                return false;
            }

            if (LoginRequest is null)
            {
                throw new InvalidOperationException($"Login required, but {nameof(LoginRequest)} is null");
            }

            _ = await SendAsync(LoginRequest, UpdateServiceId(), false, cancellationToken);
            return true;
        }

        /// <summary>
        /// Send a request. serviceId will default to a unique value. cancellationToken will be set to a default value.
        /// </summary>
        /// <param name="requestMessage">Payload to send</param>
        /// <returns>The resulting <see cref="SaleToPOIMessage"/> wrapper. Useful for extracting the ServiceId used in the request</returns>
        /// <exception cref="MessageFormatException">An error occured converting the request to JSON</exception>
        /// <exception cref="TaskCanceledException">A timeout occured sending the request</exception>
        /// <exception cref="NetworkException">A network error occured sending the request</exception>
        public async Task<SaleToPOIMessage> SendAsync(MessagePayload requestMessage)
        {
            return await SendAsync(requestMessage, UpdateServiceId(), true, new CancellationTokenSource(DefaultTimeout).Token);
        }

        /// <summary>
        /// Send a request. cancellationToken will be set to a default value.
        /// </summary>
        /// <param name="requestMessage">Payload to send</param>
        /// <param name="serviceID">ServiceId sent in the header</param>
        /// <returns>The resulting <see cref="SaleToPOIMessage"/> wrapper. Useful for extracting the ServiceId used in the request</returns>
        /// <exception cref="MessageFormatException">An error occured converting the request to JSON</exception>
        /// <exception cref="TaskCanceledException">A timeout occured sending the request</exception>
        /// <exception cref="NetworkException">A network error occured sending the request</exception>
        public async Task<SaleToPOIMessage> SendAsync(MessagePayload requestMessage, string serviceID)
        {
            ServiceID = serviceID;
            return await SendAsync(requestMessage, serviceID, true, new CancellationTokenSource(DefaultTimeout).Token);
        }

        /// <summary>
        /// Send a request. serviceId will default to a unique value. 
        /// </summary>
        /// <param name="requestMessage">Payload to send</param>
        /// <param name="cancellationToken">Cancellation token used for the request</param>
        /// <returns>The resulting <see cref="SaleToPOIMessage"/> wrapper. Useful for extracting the ServiceId used in the request</returns>
        /// <exception cref="MessageFormatException">An error occured converting the request to JSON</exception>
        /// <exception cref="TaskCanceledException">A timeout occured sending the request</exception>
        /// <exception cref="NetworkException">A network error occured sending the request</exception>
        public async Task<SaleToPOIMessage> SendAsync(MessagePayload requestMessage, System.Threading.CancellationToken cancellationToken)
        {
            return await SendAsync(requestMessage, UpdateServiceId(), true, cancellationToken);
        }

        /// <summary>
        /// Send a request. 
        /// </summary>
        /// <param name="requestMessage">Payload to send</param>
        /// <param name="serviceID">ServiceId sent in the header</param>
        /// <param name="ensureConnectedAndLoginComplete">If true, will attempt to connect (if required) and send a login (if required) instead of throwing an exception</param>
        /// <param name="cancellationToken">Cancellation token used for the request</param>
        /// <returns>The resulting <see cref="SaleToPOIMessage"/> wrapper. Useful for extracting the ServiceId used in the request</returns>
        /// <exception cref="MessageFormatException">An error occured converting the request to JSON</exception>
        /// <exception cref="TaskCanceledException">A timeout occured sending the request</exception>
        /// <exception cref="NetworkException">A network error occured sending the request</exception>
        public async Task<SaleToPOIMessage> SendAsync(MessagePayload requestMessage, string serviceID, bool ensureConnectedAndLoginComplete, System.Threading.CancellationToken cancellationToken)
        {
            SaleToPOIMessage saleToPOIRequest;
            string s;
            try
            {
                saleToPOIRequest = MessageParser.BuildSaleToPOIMessage(serviceID, SaleID, POIID, KEK, requestMessage);
                s = MessageParser.SaleToPOIMessageToString(saleToPOIRequest);
            }
            catch (Exception e)
            {
                string message = "Error occured building request json";
                Log(LogLevel.Error, message, e);
                throw new MessageFormatException(message, e);
            }

            // Check if we need to connect and/or login
            if (ensureConnectedAndLoginComplete && (await EnsureConnectedAndLoginComplete(requestMessage is LoginRequest, cancellationToken)))
            {
                // EnsureConnectedAndLoginComplete() returned true, we need to park this 
                // request and pick it up again when our login request is returned
                parkedRequestMessage = requestMessage;
                parkedServiceID = serviceID;
                parkedCancellationToken = cancellationToken;
                return saleToPOIRequest;
            }

            Log(LogLevel.Debug, $"TX {s}");

            try
            {
                await ws.SendAsync(new ArraySegment<byte>(Encoding.UTF8.GetBytes(s)), WebSocketMessageType.Text, true, CancellationTokenSource.CreateLinkedTokenSource(cts.Token, cancellationToken).Token);
            }
            catch (TaskCanceledException tce)
            {
                string message = "Timeout occured sending the request";
                Log(LogLevel.Error, message, tce);
                await DisconnectAsync();
                throw new TimeoutException(message, false, tce.InnerException);
            }
            catch (Exception e)
            {
                string message = "A network error occured sending the request message";
                Log(LogLevel.Error, message, e);
                await DisconnectAsync();
                throw new NetworkException(message, false, e.InnerException);
            }

            return saleToPOIRequest;
        }


        /// <summary>
        /// Awaits the next response message from the host. Timeout set to <see cref="DefaultTimeout"/>
        /// </summary>
        /// <returns>A response message (if one is avaialble) or null, if none are available</returns>
        /// <exception cref="InvalidOperationException">Raised when a call is made to <see cref="RecvAsync"/> whilst 'ResponseMessage' events have been subscribed to</exception>
        /// <exception cref="TimeoutException">A timeout occured awaiting a response</exception>
        /// <exception cref="NetworkException">A network error occured awaiting a response</exception>        
        public async Task<MessagePayload> RecvAsync()
        {
            return await RecvAsync(new CancellationTokenSource(DefaultTimeout).Token);
        }


        /// <summary>
        /// Awaits the next response message from the host.
        /// </summary>
        /// <param name="cancellationToken">Override the default timeout</param>
        /// <returns>A response message (if one is avaialble) or null, if none are available</returns>
        /// <exception cref="InvalidOperationException">Raised when a call is made to <see cref="RecvAsync"/> whilst 'ResponseMessage' events have been subscribed to</exception>
        /// <exception cref="TimeoutException">A timeout occured awaiting a response</exception>
        /// <exception cref="NetworkException">A network error occured awaiting a response</exception>        
        public async Task<MessagePayload> RecvAsync(CancellationToken cancellationToken)
        {
            if (IsEventModeEnabled)
            {
                throw new InvalidOperationException($"Unable to call {nameof(RecvAsync)} when {nameof(OnLoginResponse)}, {nameof(OnLogoutResponse)}, {nameof(OnCardAcquisitionResponse)}, {nameof(OnPaymentResponse)}, {nameof(OnReconciliationResponse)}, {nameof(DisplayRequest)} or {nameof(OnTransactionStatusResponse)} are assigned");
            }

            // cts is fired when disconnect occurs (NetworkError)
            // cancellationToken will fire when timeout occurs

            try
            {
                await recvQueueSemaphore.WaitAsync(CancellationTokenSource.CreateLinkedTokenSource(new CancellationToken[] { cancellationToken, socketCloseCTS.Token }).Token);
                return recvQueue.Dequeue();
            }
            catch (OperationCanceledException)
            {
                if (socketCloseCTS.IsCancellationRequested)
                {
                    throw new NetworkException("Socket disconnected", true);
                }
                else
                {
                    throw new TimeoutException("Timeout waiting for response", true);
                }
            }
        }

        /// <summary>
        /// Waits for a specified response message type. Discards all other response types.
        /// </summary>
        /// <typeparam name="T">The type of message to await. Must be of type <see cref="MessagePayload"/></typeparam>
        /// <param name="cancellationToken"></param>
        /// <returns>A response message of type 'T', or null if an unknown response was received</returns>
        /// <exception cref="InvalidOperationException">Raised when a call is made to <see cref="RecvAsync"/> whilst 'ResponseMessage' events have been subscribed to</exception>
        /// <exception cref="TimeoutException">A timeout occured awaiting a response</exception>
        /// <exception cref="NetworkException">A network error occured awaiting a response</exception>  
        public async Task<T> RecvAsync<T>(System.Threading.CancellationToken cancellationToken) where T : MessagePayload
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                var MessagePayload = await RecvAsync(cancellationToken);

                if (MessagePayload is null)
                {
                    continue;
                }

                if (MessagePayload is T)
                {
                    return MessagePayload as T;
                }

                Log(LogLevel.Trace, $"Discarding response type {MessagePayload.GetType()}");

            }
            return null;
        }

        /// <summary>
        /// Main loop for handling recv messages on the web socket
        /// </summary>
        private async void RecvLoop()
        {
            var buffer = new byte[ReceiveBufferSize];

            try
            {
                while (ws.State == WebSocketState.Open)
                {
                    var sb = new StringBuilder();

                    WebSocketReceiveResult wsrr;
                    do
                    {
                        wsrr = await ws.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationTokenSource.CreateLinkedTokenSource(cts.Token, socketCloseCTS.Token).Token);

                        if (wsrr.MessageType == WebSocketMessageType.Close)
                        {
                            await DisconnectAsync();
                            throw new NetworkException();
                        }

                        _ = sb.Append(Encoding.UTF8.GetString(buffer, 0, wsrr.Count));
                    }
                    while (!wsrr.EndOfMessage);

                    string stringResult = sb.ToString();
                    Log(LogLevel.Debug, $"RX {stringResult}");

                    // Attempt to parse the response json
                    MessagePayload messagePayload = null;
                    try
                    {
                        messagePayload = MessageParser.ParseSaleToPOIMessage(stringResult, KEK);
                    }
                    catch (Exception e)
                    {
                        Log(LogLevel.Error, "Error occured parsing the json response", e);
                    }

                    // Try to processes the next message if we couldn't unpack
                    if (messagePayload == null)
                    {
                        continue;
                    }

                    if (messagePayload is LoginResponse)
                    {
                        LoginResponse = messagePayload as LoginResponse;
                    }

                    // Either fire an event or push to our queue for next request to RecvAsync
                    if (IsEventModeEnabled)
                    {
                        FireResponseEvent(messagePayload);
                    }
                    else
                    {
                        recvQueue.Enqueue(messagePayload);
                        _ = recvQueueSemaphore.Release();
                    }

                    // Special handling for login/logout
                    if (messagePayload is LogoutResponse)
                    {
                        if((messagePayload as LogoutResponse)?.Response?.Success == true)
                        {
                            loginRequired = true;
                        }
                    }
                    if (messagePayload is LoginResponse)
                    {
                        LoginResponse = messagePayload as LoginResponse;

                        if (loginRequired)
                        {
                            loginRequired = !LoginResponse.Response.Success;

                            // Check if we have a parked request waiting for this login response
                            if (parkedRequestMessage != null)
                            {
                                // If our login was successful, we can send the parked request that triggered this auto-login
                                if (LoginResponse.Response.Success)
                                {
                                    _ = await SendAsync(parkedRequestMessage, parkedServiceID, false, parkedCancellationToken);
                                }
                                // Otherwise, we need to respond to the initial request
                                else
                                {
                                    MessagePayload response = parkedRequestMessage.CreateDefaultResponseMessagePayload(LoginResponse.Response);

                                    if (response != null)
                                    {

                                        // Either fire an event or push to our queue for next request to RecvAsync
                                        if (IsEventModeEnabled)
                                        {
                                            FireResponseEvent(response);
                                        }
                                        else
                                        {
                                            recvQueue.Enqueue(response);
                                            _ = recvQueueSemaphore.Release();
                                        }
                                    }
                                }
                                parkedRequestMessage = null;
                                parkedServiceID = null;
                            }
                        }
                    }
                }
            }
            catch
            {
                Log(LogLevel.Debug, "Network error");
            }
            finally
            {
                await DisconnectAsync();
            }
        }

        /// <summary>
        /// Sends a request, and then waits for a specified response message type. Discards all other response types. Timeout set to <see cref="DefaultTimeout"/>
        /// </summary>
        /// <typeparam name="T">The type of message to await. Must be of type <see cref="MessagePayload"/></typeparam>
        /// <param name="requestMessage">The payload to send</param>
        /// <returns>A response message of type 'T', or null if an unknown response was received</returns>
        /// <exception cref="InvalidOperationException">Raised when a call is made to <see cref="RecvAsync"/> whilst 'ResponseMessage' events have been subscribed to</exception>
        /// <exception cref="TimeoutException">A timeout occured awaiting a response</exception>
        /// <exception cref="NetworkException">A network error occured awaiting a response</exception>        
        public async Task<T> SendRecvAsync<T>(MessagePayload requestMessage) where T : MessagePayload
        {
            return await SendRecvAsync<T>(requestMessage, new CancellationTokenSource(DefaultTimeout).Token);
        }

        /// <summary>
        /// Sends a request, and then waits for a specified response message type. Discards all other response types.
        /// </summary>
        /// <typeparam name="T">The type of message to await. Must be of type <see cref="MessagePayload"/></typeparam>
        /// <param name="requestMessage">The payload to send</param>
        /// <param name="cancellationToken">The token used to handle timeouts</param>
        /// <returns>A response message of type 'T', or null if an unknown response was received</returns>
        /// <exception cref="InvalidOperationException">Raised when a call is made to <see cref="RecvAsync"/> whilst 'ResponseMessage' events have been subscribed to</exception>
        /// <exception cref="TimeoutException">A timeout occured awaiting a response</exception>
        /// <exception cref="NetworkException">A network error occured awaiting a response</exception>        
        public async Task<T> SendRecvAsync<T>(MessagePayload requestMessage, System.Threading.CancellationToken cancellationToken) where T : MessagePayload
        {
            _ = await SendAsync(requestMessage, cancellationToken);
            return await RecvAsync<T>(cancellationToken);
        }

        #region Properties

        /// <summary>
        /// Parser for converting request/response messages to bytes
        /// </summary>
        public IMessageParser MessageParser { get; set; }

        /// <summary>
        /// ServiceID of the last message sent
        /// </summary>
        public string ServiceID { get; set; }

        /// <summary>
        /// SaleID provided by DataMesh
        /// </summary>
        public string SaleID { get; set; }

        /// <summary>
        /// POIID provided by DataMesh
        /// </summary>
        public string POIID { get; set; }

        /// <summary>
        /// KEK provided by DataMesh
        /// </summary>
        public string KEK { get; set; }

        /// <summary>
        /// The URL of the Unify service to connect to.
        /// </summary>
        public UnifyURL URL { get; set; }

        /// <summary>
        /// The custom URL string to use if <see cref="URL"/> is set to 'Custom'
        /// </summary>
        public string CustomURL { get; set; }

        /// <summary>
        /// The RootCA of the Unify service to connect to.
        /// </summary>
        public UnifyRootCA RootCA { get; set; }

        /// <summary>
        /// The custom RootCA string to use if <see cref="RootCA"/> is set to 'Custom'
        /// </summary>
        public string CustomRootCA { get; set; }

        /// <summary>
        /// The default login request to be sent by <see cref="IFusionClient"/> if a login is required
        /// </summary>
        public LoginRequest LoginRequest { get; set; }

        /// <summary>
        /// The login response returned as a part of an auto-login 
        /// </summary>
        public LoginResponse LoginResponse { get; private set; }

        /// <summary>
        /// Byte size of the receive buffer
        /// </summary>
        public int ReceiveBufferSize { get; set; } // 1Kb

        /// <summary>
        /// Level of logging which should be returned in the OnLog event. Default is <see cref="LogLevel.None"/>
        /// </summary>
        public LogLevel LogLevel { get; set; }

        /// <summary>
        /// Default amount of time we wait for responses from the switch. Default is 90 seconds.
        /// </summary>
        public TimeSpan DefaultTimeout { get; set; }

        /// <summary>
        /// Default amount of time between websocket ping/pong heartbeats. Default is 15 seconds.
        /// </summary>
        public TimeSpan DefaultHeartbeatTimeout { get; set; }


        /// <summary>
        /// Indicates if event mode has been enabled. This is set when <see cref="OnLoginResponse"/>, <see cref="OnLogoutResponse"/>, <see cref="OnCardAcquisitionResponse"/>, <see cref="OnPaymentResponse"/>, 
        /// <see cref="OnReconciliationResponse"/>, <see cref="OnTransactionStatusResponse"/>, or <see cref="OnDisplayRequest"/> events 
        /// have been subscribed to. When events mode is enabled, responses will be returned to the owner via <see cref="OnLoginResponse"/>, 
        /// <see cref="OnPaymentResponse"/>, <see cref="OnReconciliationResponse"/> , <see cref="OnTransactionStatusResponse"/>, and 
        /// <see cref="OnDisplayRequest"/> events, and all requests to <see cref="RecvAsync"/> will throw an <see cref="InvalidOperationException"/>
        /// </summary>
        public bool IsEventModeEnabled => (OnLoginResponse != null) || (OnLogoutResponse != null) || (OnCardAcquisitionResponse != null) || (OnPaymentResponse != null) || (OnReconciliationResponse != null) || (OnDisplayRequest != null) || (OnTransactionStatusResponse != null);
        #endregion

        #region Events
        /// <summary>
        /// Fired when a log event occurs which is at or above <see cref="LogLevel"/>
        /// </summary>
        public event EventHandler<LogEventArgs> OnLog;

        /// <summary>
        /// Fired when the socket connects. 
        /// </summary>
        public event EventHandler OnConnect;

        /// <summary>
        /// Fired when a socket connection was attempted, but failed
        /// </summary>
        public event EventHandler OnConnectError;

        /// <summary>
        /// Fired when the socket disconnects. 
        /// </summary>
        public event EventHandler OnDisconnect;

        /// <summary>
        /// Fired when a <see cref="LoginResponse"/> is received. Subscribing to this event will enable <see cref="IsEventModeEnabled"/>
        /// </summary>
        public event EventHandler<MessagePayloadEventArgs<LoginResponse>> OnLoginResponse;

        /// <summary>
        /// Fired when a <see cref="LogoutResponse"/> is received. Subscribing to this event will enable <see cref="IsEventModeEnabled"/>
        /// </summary>
        public event EventHandler<MessagePayloadEventArgs<LogoutResponse>> OnLogoutResponse;

        /// <summary>
        /// Fired when a <see cref="CardAcquisitionResponse"/> is received. Subscribing to this event will enable <see cref="IsEventModeEnabled"/>
        /// </summary>
        public event EventHandler<MessagePayloadEventArgs<CardAcquisitionResponse>> OnCardAcquisitionResponse;

        /// <summary>
        /// Fired when a <see cref="PaymentResponse"/> is received. Subscribing to this event will enable <see cref="IsEventModeEnabled"/>
        /// </summary>
        public event EventHandler<MessagePayloadEventArgs<PaymentResponse>> OnPaymentResponse;

        /// <summary>
        /// Fired when a <see cref="ReconciliationResponse"/> is received. Subscribing to this event will enable <see cref="IsEventModeEnabled"/>
        /// </summary>
        public event EventHandler<MessagePayloadEventArgs<ReconciliationResponse>> OnReconciliationResponse;

        /// <summary>
        /// Fired when a <see cref="DisplayRequest"/> is received. Subscribing to this event will enable <see cref="IsEventModeEnabled"/>
        /// </summary>
        public event EventHandler<MessagePayloadEventArgs<DisplayRequest>> OnDisplayRequest;


        /// <summary>
        /// Fired when a <see cref="TransactionStatusResponse"/> is received. Subscribing to this event will enable <see cref="IsEventModeEnabled"/>
        /// </summary>
        public event EventHandler<MessagePayloadEventArgs<TransactionStatusResponse>> OnTransactionStatusResponse;
        #endregion

        #region Dispose Pattern
        private bool disposedValue;
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    ws?.Dispose();
                    ws = null;
                    cts?.Dispose();
                    cts = null;
                    recvQueueSemaphore?.Dispose();
                    socketCloseCTS?.Dispose();
                    socketCloseCTS = null;

                    ServicePointManager.ServerCertificateValidationCallback -= CertificateValidation.RemoteCertificateValidationCallback;
                }

                disposedValue = true;
            }
        }

        // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        ~FusionClient()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: false);
        }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
        #endregion


        /// <summary>
        /// Helper file to fire a response event based on a message payload
        /// </summary>
        /// <param name=""></param>
        private void FireResponseEvent(MessagePayload MessagePayload)
        {
            switch (MessagePayload)
            {
                case LoginResponse r:
                    OnLoginResponse?.Invoke(this, new MessagePayloadEventArgs<LoginResponse>(r));
                    break;
                case LogoutResponse r:
                    OnLogoutResponse?.Invoke(this, new MessagePayloadEventArgs<LogoutResponse>(r));
                    break;
                case CardAcquisitionResponse r:
                    OnCardAcquisitionResponse?.Invoke(this, new MessagePayloadEventArgs<CardAcquisitionResponse>(r));
                    break;
                case PaymentResponse r:
                    OnPaymentResponse?.Invoke(this, new MessagePayloadEventArgs<PaymentResponse>(r));
                    break;
                case ReconciliationResponse r:
                    OnReconciliationResponse?.Invoke(this, new MessagePayloadEventArgs<ReconciliationResponse>(r));
                    break;
                case TransactionStatusResponse r:
                    OnTransactionStatusResponse?.Invoke(this, new MessagePayloadEventArgs<TransactionStatusResponse>(r));
                    break;
                case DisplayRequest r:
                    OnDisplayRequest?.Invoke(this, new MessagePayloadEventArgs<DisplayRequest>(r));
                    break;
                default:
                    Log(LogLevel.Error, $"Unknown response message {MessagePayload.GetMessageDescription()}");
                    break;
            }
        }

        /// <summary>
        /// Fires OnLog event based on LogLevel
        /// </summary>
        private void Log(LogLevel logLevel, string data, Exception exception = null)
        {
            if (OnLog == null || LogLevel >= logLevel)
            {
                return;
            }

            OnLog.Invoke(this, new LogEventArgs() { LogLevel = logLevel, Data = data, Exception = exception });
        }
    }
}
