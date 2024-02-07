namespace SaturnServices.Controllers;

[Route("api/[controller]")]
[ApiController]
public class WebSocketController : ControllerBase
{
    public WebSocketController()
    {

    }

    [HttpGet("ConnectWS")]
    public async Task Get()
    {
        if (HttpContext.WebSockets.IsWebSocketRequest)
        {
            ulong userId = ulong.Parse(HttpContext.Request.Query["userId"]);
            using var webSocket = await HttpContext.WebSockets.AcceptWebSocketAsync();
            await WebSocketHelper.Echo(webSocket, userId);
        }
        else
        {
            HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
        }
    }
}
