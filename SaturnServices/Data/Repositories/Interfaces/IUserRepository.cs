namespace SaturnServices.Data.Repositories.Interfaces;

public interface IUserRepository
{
    Task<UserResponse?> GetUserAsync(AuthRequest request);
}
