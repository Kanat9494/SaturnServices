namespace SaturnServices.Models.DTOs.Requests;

public class AuthRequest
{
    [JsonPropertyName("userName")]
    public string UserName { get; set; } = null!;
    [JsonPropertyName("password")]
    public string Password { get; set; } = null!;    
}
