using System.Net.WebSockets;
using System.Text;

namespace BlogApp.WebSocket
{
    public class WebSocketMiddleware
    {
        private readonly RequestDelegate _next;

        public WebSocketMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (context.WebSockets.IsWebSocketRequest)
            {
                System.Net.WebSockets.WebSocket webSocket = await context.WebSockets.AcceptWebSocketAsync();
                await HandleWebSocketConnectionAsync(webSocket);
            }
            else
            {
                await _next(context);
            }
        }

        private async Task HandleWebSocketConnectionAsync(System.Net.WebSockets.WebSocket webSocket)
        {
            var buffer = new byte[1024 * 4];
            WebSocketReceiveResult result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);

            while (!result.CloseStatus.HasValue)
            {
                var serverMsg = Encoding.UTF8.GetBytes($"Server: Olá! Agora é :  {DateTime.Now}");
                await webSocket.SendAsync(new ArraySegment<byte>(serverMsg, 0, serverMsg.Length), result.MessageType, result.EndOfMessage, CancellationToken.None);

                result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
            }

            await webSocket.CloseAsync(result.CloseStatus.Value, result.CloseStatusDescription, CancellationToken.None);
        }
    }
}