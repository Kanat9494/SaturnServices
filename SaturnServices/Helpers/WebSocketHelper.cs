namespace SaturnServices.Helpers;

public class WebSocketHelper
{
    internal StringBuilder MessageBuilder { get; private set; } = new StringBuilder();
    internal async Task Echo(WebSocket webSocket, ulong userId)
    {
        try
        {
            WebSocketManager.AddOrUpdateClient(new ChatClient
            {
                UserId = userId,
                ClientSocket = webSocket,
            });
            var buffer = new byte[1024 * 4];
            var receiveResult = await webSocket.ReceiveAsync(
                new ArraySegment<byte>(buffer), CancellationToken.None);


            while (!receiveResult.CloseStatus.HasValue)
            {
                MessageBuilder.Clear();

                MessageBuilder.Append(Encoding.UTF8.GetString(buffer, 0, receiveResult.Count));
                var message = JsonSerializer.Deserialize<Message>(MessageBuilder.ToString());
                

                while (receiveResult.EndOfMessage == false)
                {
                    Array.Resize(ref buffer, buffer.Length * 2);
                    receiveResult = await webSocket.ReceiveAsync(
                        new ArraySegment<byte>(buffer), CancellationToken.None);
                }

                var client = WebSocketManager.GetClient(message?.ReceiverId ?? 0);
                if (client != null)
                {
                    await client!.ClientSocket!.SendAsync(new ArraySegment<byte>(buffer, 0, receiveResult.Count),
                    receiveResult.MessageType,
                    receiveResult.EndOfMessage,
                    CancellationToken.None);
                }

                receiveResult = await webSocket.ReceiveAsync(
                    new ArraySegment<byte>(buffer), CancellationToken.None);
            }

            await webSocket.CloseAsync(
                receiveResult.CloseStatus.Value,
                receiveResult.CloseStatusDescription,
                CancellationToken.None);
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            await webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Unexpected error", CancellationToken.None);
        }
    }
}
