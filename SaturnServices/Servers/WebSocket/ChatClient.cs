namespace SaturnServices.Servers.WebSocket;

public class ChatClient
{
    protected internal ulong UserId { get; set; }
    protected internal System.Net.WebSockets.WebSocket? ClientSocket { get; set; }
}
