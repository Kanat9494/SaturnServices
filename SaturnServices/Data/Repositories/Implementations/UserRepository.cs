namespace SaturnServices.Data.Repositories.Implementations;

public class UserRepository : IUserRepository
{
    public UserRepository(SaturnDBContext dbContext)
    {
        _dbContext = dbContext;
    }

    private readonly SaturnDBContext _dbContext;
    public async Task<UserResponse?> GetUserAsync(AuthRequest request)
    {
        var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.UserName == request.UserName &&
            u.Password == request.Password);
        if (user == null)
            return null;

        return new UserResponse(user!);
    }
}
