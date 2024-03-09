namespace SaturnServices.Services.Interfaces;

public interface IAuthService
{
    Task<UserResponse?> AuthenticateUserAsync(AuthRequest request);
}
