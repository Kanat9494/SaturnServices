namespace SaturnServices.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class AuthenticationController : ControllerBase
{
    public AuthenticationController(IAuthService authService)
    {
        _authService = authService;
    }

    private readonly IAuthService _authService;

    [HttpPost("AuthenticateUser")]
    [AllowAnonymous]
    public async Task<UserResponse?> AuthenticateUser([FromBody] AuthRequest authRequest)
        => await _authService.AuthenticateUserAsync(authRequest);

    [HttpPost("SendSms")]
    [AllowAnonymous]
    public string SendSms([FromBody] SmsRequest smsRequest)
    {
        var accountSid = "AC387f683f5e51f0a76363dde54577efe6";
        var authToken = "5abfa73f7de387ed822dd5553fc3c4c4";
        TwilioClient.Init(accountSid, authToken);

        var messageOption = new CreateMessageOptions(
            new Twilio.Types.PhoneNumber(smsRequest.PhoneNumber));
        messageOption.From = new Twilio.Types.PhoneNumber("+15138084422");
        messageOption.Body = smsRequest.OTPCode.ToString();
        var message = MessageResource.Create(messageOption);
        Console.WriteLine(message);
        Console.WriteLine(message.Body);
        return message.Body;
    }
}
