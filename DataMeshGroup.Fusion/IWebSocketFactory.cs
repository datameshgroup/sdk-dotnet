using System;
using System.Net.WebSockets;
using System.Threading;
using System.Threading.Tasks;

namespace DataMeshGroup.Fusion
{
    public interface IWebSocketFactory
    {
        /// <summary>
        /// Creates and connects a ClientWebSocket that works for this platform.
        /// </summary>
        /// <param name="uri">URL to connect to</param>
        /// <param name="keepAliveInterval">How often to send ping/pong heartbeat messages</param>
        /// <param name="cancellationToken">Token used to close socket during send/recv awaits</param>        
        /// <returns>A connected web socket, otherwise throws exception</returns>
        Task<WebSocket> ConnectAsync(Uri uri, TimeSpan keepAliveInterval, CancellationToken cancellationToken);
    }
}
