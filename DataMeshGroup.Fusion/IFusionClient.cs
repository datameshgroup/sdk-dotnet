using DataMeshGroup.Fusion.Model;
using System;
using System.Threading.Tasks;

namespace DataMeshGroup.Fusion
{
    public interface IFusionClient : IDisposable
    {

        /// <summary>
        /// Connects to the <see cref="URL"/>, or <see cref="CustomURL"/> if <see cref="URL"/> is <see cref="URL"/>
        /// </summary>
        Task<bool> ConnectAsync();

        /// <summary>
        /// Disconnects and releases resources
        /// </summary>
        Task DisconnectAsync();

        /// <summary>
        /// Send a request. serviceId will default to a unique value. cancellationToken will default to 60 seconds.
        /// </summary>
        /// <param name="requestMessage">Payload to send</param>
        /// <returns>The resulting <see cref="SaleToPOIMessage"/> wrapper. Useful for extracting the ServiceId used in the request</returns>
        /// <exception cref="MessageFormatException">An error occured converting the request to JSON</exception>
        /// <exception cref="TaskCanceledException">A timeout occured sending the request</exception>
        /// <exception cref="NetworkException">A network error occured sending the request</exception>
        Task<SaleToPOIMessage> SendAsync(MessagePayload requestMessage);

        /// <summary>
        /// Send a request. cancellationToken will be set to a default value.
        /// </summary>
        /// <param name="requestMessage">Payload to send</param>
        /// <param name="serviceID">ServiceId sent in the header</param>
        /// <returns>The resulting <see cref="SaleToPOIMessage"/> wrapper. Useful for extracting the ServiceId used in the request</returns>
        /// <exception cref="MessageFormatException">An error occured converting the request to JSON</exception>
        /// <exception cref="TaskCanceledException">A timeout occured sending the request</exception>
        /// <exception cref="NetworkException">A network error occured sending the request</exception>
        Task<SaleToPOIMessage> SendAsync(MessagePayload requestMessage, string serviceID);

        /// <summary>
        /// Send a request. serviceId will default to a unique value. 
        /// </summary>
        /// <param name="requestMessage">Payload to send</param>
        /// <param name="cancellationToken">Cancellation token used for the request</param>
        /// <returns>The resulting <see cref="SaleToPOIMessage"/> wrapper. Useful for extracting the ServiceId used in the request</returns>
        /// <exception cref="MessageFormatException">An error occured converting the request to JSON</exception>
        /// <exception cref="TaskCanceledException">A timeout occured sending the request</exception>
        /// <exception cref="NetworkException">A network error occured sending the request</exception>
        Task<SaleToPOIMessage> SendAsync(MessagePayload requestMessage, System.Threading.CancellationToken cancellationToken);


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
        Task<SaleToPOIMessage> SendAsync(MessagePayload requestMessage, string serviceID, bool ensureConnectedAndLoginComplete, System.Threading.CancellationToken cancellationToken);

        /// <summary>
        /// Awaits the next response message from the host. Timeout set to <see cref="DefaultTimeout"/>
        /// </summary>
        /// <returns>A response message (if one is avaialble) or null, if none are available</returns>
        /// <exception cref="InvalidOperationException">Raised when a call is made to <see cref="RecvAsync"/> whilst 'ResponseMessage' events have been subscribed to</exception>
        /// <exception cref="TimeoutException">A timeout occured awaiting a response</exception>
        /// <exception cref="NetworkException">A network error occured awaiting a response</exception>        
        Task<MessagePayload> RecvAsync();

        /// <summary>
        /// Awaits the next response message from the host.
        /// </summary>
        /// <param name="cancellationToken">Override the default timeout</param>
        /// <returns>A response message (if one is avaialble) or null, if none are available</returns>
        /// <exception cref="InvalidOperationException">Raised when a call is made to <see cref="RecvAsync"/> whilst 'ResponseMessage' events have been subscribed to</exception>
        /// <exception cref="TimeoutException">A timeout occured awaiting a response</exception>
        /// <exception cref="NetworkException">A network error occured awaiting a response</exception>        
        Task<MessagePayload> RecvAsync(System.Threading.CancellationToken cancellationToken);

        /// <summary>
        /// Waits for a specified response message type. Discards all other response types.
        /// </summary>
        /// <typeparam name="T">The type of message to await. Must be of type <see cref="MessagePayload"/></typeparam>
        /// <param name="cancellationToken"></param>
        /// <returns>A response message of type 'T', or null if an unknown response was received</returns>
        /// <exception cref="InvalidOperationException">Raised when a call is made to <see cref="RecvAsync"/> whilst 'ResponseMessage' events have been subscribed to</exception>
        /// <exception cref="TimeoutException">A timeout occured awaiting a response</exception>
        /// <exception cref="NetworkException">A network error occured awaiting a response</exception>        
        Task<T> RecvAsync<T>(System.Threading.CancellationToken cancellationToken) where T : MessagePayload;

        /// <summary>
        /// Sends a request, and then waits for a specified response message type. Discards all other response types. Timeout set to <see cref="DefaultTimeout"/>
        /// </summary>
        /// <typeparam name="T">The type of message to await. Must be of type <see cref="MessagePayload"/></typeparam>
        /// <param name="requestMessage">The payload to send</param>
        /// <returns>A response message of type 'T', or null if an unknown response was received</returns>
        /// <exception cref="InvalidOperationException">Raised when a call is made to <see cref="RecvAsync"/> whilst 'ResponseMessage' events have been subscribed to</exception>
        /// <exception cref="TimeoutException">A timeout occured awaiting a response</exception>
        /// <exception cref="NetworkException">A network error occured awaiting a response</exception>        
        Task<T> SendRecvAsync<T>(MessagePayload requestMessage) where T : MessagePayload;

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
        Task<T> SendRecvAsync<T>(MessagePayload requestMessage, System.Threading.CancellationToken cancellationToken) where T : MessagePayload;


        #region Properties

        /// <summary>
        /// Parser for converting request/response messages to bytes
        /// </summary>
        IMessageParser MessageParser { get; set; }

        /// <summary>
        /// ServiceID of the last message sent
        /// </summary>
        string ServiceID { get; set; }

        /// <summary>
        /// SaleID provided by DataMesh
        /// </summary>
        string SaleID { get; set; }

        /// <summary>
        /// POIID provided by DataMesh
        /// </summary>
        string POIID { get; set; }

        /// <summary>
        /// KEK provided by DataMesh
        /// </summary>
        string KEK { get; set; }

        /// <summary>
        /// The URL of the Unify service to connect to.
        /// </summary>
        UnifyURL URL { get; set; }

        /// <summary>
        /// The custom URL string to use if <see cref="URL"/> is set to 'Custom'
        /// </summary>
        string CustomURL { get; set; }

        /// <summary>
        /// The RootCA of the Unify service to connect to.
        /// </summary>
        UnifyRootCA RootCA { get; set; }

        /// <summary>
        /// The custom RootCA string to use if <see cref="RootCA"/> is set to 'Custom'
        /// </summary>
        string CustomRootCA { get; set; }

        /// <summary>
        /// The default login request to be sent by <see cref="IFusionClient"/> if a login is required
        /// </summary>
        LoginRequest LoginRequest { get; set; }

        /// <summary>
        /// The login response returned as a part of an auto-login 
        /// </summary>
        LoginResponse LoginResponse { get; }

        /// <summary>
        /// Byte size of the receive buffer
        /// </summary>
        int ReceiveBufferSize { get; set; } // 1Kb

        /// <summary>
        /// Level of logging which should be returned in the OnLog event. Default is <see cref="LogLevel.None"/>
        /// </summary>
        LogLevel LogLevel { get; set; }

        /// <summary>
        /// Default amount of time we wait for responses from the switch. Default is 90 seconds.
        /// </summary>
        TimeSpan DefaultTimeout { get; set; }

        /// <summary>
        /// Default amount of time between websocket ping/pong heartbeats. Default is 15 seconds.
        /// </summary>
        TimeSpan DefaultHeartbeatTimeout { get; set; }

        /// <summary>
        /// Indicates if event mode has been enabled. This is set when <see cref="OnLoginResponse"/> or 
        /// <see cref="OnPaymentResponse"/> events have been subscribed to. When events mode is enabled, 
        /// responses will be returned to the owner via <see cref="OnLoginResponse"/> and 
        /// <see cref="OnPaymentResponse"/> events, and all requests to <see cref="RecvAsync"/> will 
        /// throw an <see cref="InvalidOperationException"/>
        /// </summary>
        bool IsEventModeEnabled { get; }
        #endregion

        #region Events
        /// <summary>
        /// Fired when a log event occurs which is at or above <see cref="LogLevel"/>
        /// </summary>
        event EventHandler<LogEventArgs> OnLog;

        /// <summary>
        /// Fired when the socket connects. 
        /// </summary>
        event EventHandler OnConnect;

        /// <summary>
        /// Fired when a socket connection was attempted, but failed
        /// </summary>
        event EventHandler OnConnectError;

        /// <summary>
        /// Fired when the socket disconnects. 
        /// </summary>
        event EventHandler OnDisconnect;

        /// <summary>
        /// Fired when a <see cref="LoginResponse"/> is received. Subscribing to this event will enable <see cref="IsEventModeEnabled"/>
        /// </summary>
        event EventHandler<MessagePayloadEventArgs<LoginResponse>> OnLoginResponse;

        /// <summary>
        /// Fired when a <see cref="LogoutResponse"/> is received. Subscribing to this event will enable <see cref="IsEventModeEnabled"/>
        /// </summary>
        event EventHandler<MessagePayloadEventArgs<LogoutResponse>> OnLogoutResponse;

        /// <summary>
        /// Fired when a <see cref="CardAcquisitionResponse"/> is received. Subscribing to this event will enable <see cref="IsEventModeEnabled"/>
        /// </summary>
        event EventHandler<MessagePayloadEventArgs<CardAcquisitionResponse>> OnCardAcquisitionResponse;

        /// <summary>
        /// Fired when a <see cref="PaymentResponse"/> is received. Subscribing to this event will enable <see cref="IsEventModeEnabled"/>
        /// </summary>
        event EventHandler<MessagePayloadEventArgs<PaymentResponse>> OnPaymentResponse;

        /// <summary>
        /// Fired when a <see cref="ReconciliationResponse"/> is received. Subscribing to this event will enable <see cref="IsEventModeEnabled"/>
        /// </summary>
        event EventHandler<MessagePayloadEventArgs<ReconciliationResponse>> OnReconciliationResponse;

        /// <summary>
        /// Fired when a <see cref="DisplayRequest"/> is received. Subscribing to this event will enable <see cref="IsEventModeEnabled"/>
        /// </summary>
        event EventHandler<MessagePayloadEventArgs<DisplayRequest>> OnDisplayRequest;

        /// <summary>
        /// Fired when a <see cref="TransactionStatusResponse"/> is received. Subscribing to this event will enable <see cref="IsEventModeEnabled"/>
        /// </summary>
        event EventHandler<MessagePayloadEventArgs<TransactionStatusResponse>> OnTransactionStatusResponse;

        /// <summary>
        /// Fired when a <see cref="EventNotification"/> is received. Subscribing to this event will enable <see cref="IsEventModeEnabled"/>
        /// </summary>
        event EventHandler<MessagePayloadEventArgs<EventNotification>> OnEventNotification;

        #endregion
    }
}
