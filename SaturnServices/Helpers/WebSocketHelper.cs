namespace SaturnServices.Helpers;

public class WebSocketHelper
{
    internal static async Task Echo(WebSocket webSocket, ulong userId)
    {
        try
        {
            WebSocketManager.AddClient(new ChatClient
            {
                UserId = userId,
                ClientSocket = webSocket,
            });
            var buffer = new byte[1024 * 4];
            var receiveResult = await webSocket.ReceiveAsync(
                new ArraySegment<byte>(buffer), CancellationToken.None);
            var jsonMessage = Encoding.UTF8.GetString(buffer, 0, receiveResult.Count);
            var messageResult = JsonSerializer.Deserialize<Message>(jsonMessage);
            
            Debug.WriteLine(jsonMessage);


            while (!receiveResult.CloseStatus.HasValue)
            {
                var jsonString = Encoding.UTF8.GetString(buffer, 0, receiveResult.Count);
                var message = JsonSerializer.Deserialize<Message>(jsonString);
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
            
        }
    }
}
