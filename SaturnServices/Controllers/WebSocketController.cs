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
            using var webSocket = await HttpContext.WebSockets.AcceptWebSocketAsync();
            await WebSocketHelper.Echo(webSocket);
        }
        else
        {
            HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
        }
    }
}
