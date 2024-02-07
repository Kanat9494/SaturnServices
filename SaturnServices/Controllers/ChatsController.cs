namespace SaturnServices.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ChatsController : ControllerBase
{
    public ChatsController()
    {

    }

    [HttpGet("Test")]
    public IActionResult Test2()
        => Ok("Работает");

    [HttpGet("ConnectToWS")]
    public async Task ConnectToWS(ulong userId)
    {
        if (HttpContext.WebSockets.IsWebSocketRequest)
        {
            using var webSocket = await HttpContext.WebSockets.AcceptWebSocketAsync();
            await WebSocketHelper.Echo(webSocket, userId);
        }
        else
        {
            HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
        }
    }
}
