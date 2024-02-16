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

    protected internal ulong RemoveClientBySocket(WebSocket socket)
    {
        var client = _clients.FirstOrDefault(c => c.Value.ClientSocket == socket);
        if (_clients.ContainsKey(client.Value.UserId))
        {
            _clients.Remove(client.Key);
            return client.Value.UserId;
        }


        return 0;
    }

    protected internal ChatClient GetClient(ulong userId)
    {
        return _clients.ContainsKey(userId) ? _clients[userId] : null;
    }

    protected internal int GetConnectedClientsCount()
    {
        return _clients.Count;
    }
}
