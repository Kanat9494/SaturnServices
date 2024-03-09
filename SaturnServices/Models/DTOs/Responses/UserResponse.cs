namespace SaturnServices.Models.DTOs.Responses;

public class UserResponse
{
    public UserResponse(User user)
    {
        UserId = user.UserId;
        UserName = user.UserName;   
    }
    public int UserId { get; set; }
    public string UserName { get; set; }
    public string AccessToken { get; set; }
}
