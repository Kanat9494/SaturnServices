namespace SaturnServices.Helpers;

public class WebSocketHelper
{
    public WebSocketHelper()
    {
        _webSocketManager = new WebSocketManager();
    }


    private readonly WebSocketManager _webSocketManager;
    internal StringBuilder MessageBuilder { get; private set; } = new StringBuilder();
    internal async Task Echo(WebSocket webSocket, ulong userId)
    {
        try
        {
            _webSocketManager.AddOrUpdateClient(new ChatClient
            {
                UserId = userId,
                ClientSocket = webSocket,
            });
            Debug.WriteLine($"Подключился пользователь, с userId: {userId}");
            Debug.WriteLine($"Количество подключенных пользователей: {_webSocketManager.GetConnectedClientsCount()}");
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

                var client = _webSocketManager.GetClient(message?.ReceiverId ?? 0);
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
            var clientId = _webSocketManager.RemoveClientBySocket(webSocket);
            if (clientId > 0)
                Debug.WriteLine($"Пользователь с userId {clientId} прервал соединение");
            Debug.WriteLine($"Количество оставшихся подключенных пользователей: {_webSocketManager.GetConnectedClientsCount()}");

        }
    }
}
