namespace SaturnServices.Helpers;

public class WebSocketHelper
{
    internal static async Task Echo(WebSocket webSocket)
    {
        try
        {
            var buffer = new byte[1024 * 4];
            var receiveResult = await webSocket.ReceiveAsync(
                new ArraySegment<byte>(buffer), CancellationToken.None);


            while (!receiveResult.CloseStatus.HasValue)
            {
                var message = Encoding.UTF8.GetString(buffer, 0, receiveResult.Count);
                Debug.WriteLine(message);
                await webSocket.SendAsync(
                    new ArraySegment<byte>(buffer, 0, receiveResult.Count),
                    receiveResult.MessageType,
                    receiveResult.EndOfMessage,
                    CancellationToken.None);

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
