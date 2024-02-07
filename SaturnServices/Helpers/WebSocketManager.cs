namespace SaturnServices.Helpers;

public class WebSocketManager
{
    private static readonly Dictionary<ulong, ChatClient> _clients = new Dictionary<ulong, ChatClient>();

    protected internal static void AddClient(ChatClient client)
    {
        _clients.Add(client.UserId, client);
    }

    public static void RemoveClient(ulong userId)
    {
        _clients.Remove(userId);
    }

    public static ChatClient GetClient(ulong userId)
    {
        return _clients.ContainsKey(userId) ? _clients[userId] : null;
    }
}
