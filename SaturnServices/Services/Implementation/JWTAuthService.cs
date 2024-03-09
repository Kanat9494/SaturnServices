namespace SaturnServices.Services.Implementation;

public class JWTAuthService : IAuthService
{
    public JWTAuthService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    private readonly IUserRepository _userRepository;

    public async Task<UserResponse?> AuthenticateUserAsync(AuthRequest request)
    {
        var user = await _userRepository.GetUserAsync(request);

        if (user == null)
            return null;

        var token = GenerateJWT(user);

        user.AccessToken = token;

        return user;
    }

    #region JWT token
    private string GenerateJWT(UserResponse user)
    {
        var securityKey = AuthOptions.GetSymmetricSecurityKey();
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim("UserName", user.UserName),
            new Claim("UserId", user.UserId.ToString()),
        };

        var token = new JwtSecurityToken(AuthOptions.ISSUER, 
            AuthOptions.AUDIENCE,
            claims,
            expires: DateTime.Now.AddMinutes(AuthOptions.LIFE_TIME),
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
    #endregion
}
