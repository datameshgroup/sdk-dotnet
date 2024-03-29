﻿using DataMeshGroup.Fusion.Model;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace DataMeshGroup.Fusion
{
    public class DefaultWebSocketFactory : IWebSocketFactory
    {
        /// <summary>
        /// Creates and connects a ClientWebSocket that works for this platform. Uses System.Net.WebSockets.ClientWebSocket if supported or System.Net.WebSockets.Managed.ClientWebSocket if not.
        /// </summary>
        /// <param name="uri">URL to connect to</param>
        /// <param name="keepAliveInterval">How often to send ping/pong heartbeat messages</param>
        /// <param name="cancellationToken">Token used to close socket during send/recv awaits</param>
        /// <returns>A connected web socket, otherwise throws exception</returns>
        public async Task<System.Net.WebSockets.WebSocket> ConnectAsync(Uri uri, WebSocketHeaders headers, TimeSpan keepAliveInterval, CancellationToken cancellationToken)
        {
            try
            {
                var clientWebSocket = new System.Net.WebSockets.ClientWebSocket();
                clientWebSocket.Options.KeepAliveInterval = keepAliveInterval;
                // Set headers
                clientWebSocket.Options.SetRequestHeader("User-Agent", headers.UserAgent);
                if(!string.IsNullOrEmpty(headers.SaleID))
                {
                    clientWebSocket.Options.SetRequestHeader("X-SALE-ID", headers.SaleID);
                }
                if (!string.IsNullOrEmpty(headers.POIID))
                {
                    clientWebSocket.Options.SetRequestHeader("X-POI-ID", headers.SaleID);
                }
                if (!string.IsNullOrEmpty(headers.InstanceID))
                {
                    clientWebSocket.Options.SetRequestHeader("X-INSTANCE-ID", headers.SaleID);
                }
                if (!string.IsNullOrEmpty(headers.CertificationCode))
                {
                    clientWebSocket.Options.SetRequestHeader("X-CERTIFICATION-CODE", headers.SaleID);
                }

#if (!NETFRAMEWORK && !NETSTANDARD2_0)
                // .NET Core has built-in support for certificate validation, where .NET Framework 
                // relies on the static ServicePointManager.ServerCertificateValidationCallback
                clientWebSocket.Options.RemoteCertificateValidationCallback += (sender, cert, chain, error) =>
                {
                    return CertificateValidation.RemoteCertificateValidationCallback(sender, cert, chain, error);
                };
#else
                // ServicePointManager is used for .NET Framework
                System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;
                System.Net.ServicePointManager.ServerCertificateValidationCallback = CertificateValidation.RemoteCertificateValidationCallback; // unsubscribe in dispose
#endif

                await clientWebSocket.ConnectAsync(uri, cancellationToken);
                return clientWebSocket;
            }
            catch (PlatformNotSupportedException) // throw when running under .Net Framework and Windows 7 as System.Net.WebSockets.ClientWebSocket is unsupported
            {
                var clientWebSocket = new System.Net.WebSockets.Managed.ClientWebSocket();
                clientWebSocket.Options.KeepAliveInterval = keepAliveInterval;
                clientWebSocket.Options.RemoteCertificateValidationCallback += (sender, cert, chain, error) =>
                {
                    return CertificateValidation.RemoteCertificateValidationCallback(sender, cert, chain, error);
                };
                await clientWebSocket.ConnectAsync(uri, cancellationToken);
                return clientWebSocket;
            }
        }
    }
}