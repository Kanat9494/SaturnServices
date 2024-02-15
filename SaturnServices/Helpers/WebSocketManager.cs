namespace SaturnServices.Helpers;

public class WebSocketManager
{
    private static readonly Dictionary<ulong, ChatClient> _clients = new Dictionary<ulong, ChatClient>();

    protected internal void AddOrUpdateClient(ChatClient client)
    {
        if (!_clients.ContainsKey(client.UserId))
            _clients.Add(client.UserId, client);
        else
            _clients[client.UserId] = client;
    }

    public static void RemoveClient(ulong userId)
    {
        _clients.Remove(userId);
    }

    public ChatClient GetClient(ulong userId)
    {
        return _clients.ContainsKey(userId) ? _clients[userId] : null;
    }
}
