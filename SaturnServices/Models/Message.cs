namespace SaturnServices.Models;

public class Message
{
    public ulong SenderId { get; set; }
    public ulong ReceiverId { get; set; }
    public string? Content { get; set; }
}
