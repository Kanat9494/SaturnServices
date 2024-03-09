namespace SaturnServices.Helpers;

public class AuthOptions
{
    public const string ISSUER = "letoinc.kg.swift-courier";
    public const string AUDIENCE = "letoinc.kg.clients";
    const string KEY = "LOIUE45634oiulkj78234qwrpiuMNKJGH2634987vzxcv23984564360982341lkasfmzcxv";
    public const int LIFE_TIME = 10;
    public static SymmetricSecurityKey GetSymmetricSecurityKey()
    {
        return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(KEY));
    }
}
