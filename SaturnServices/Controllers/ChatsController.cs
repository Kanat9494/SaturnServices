namespace SaturnServices.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ChatsController : ControllerBase
{
    public ChatsController()
    {
        _webSocketHelper = new WebSocketHelper();   
    }

    WebSocketHelper _webSocketHelper;

    [HttpGet("Test")]
    public IActionResult Test2()
        => Ok("Работает");

    [HttpGet("ConnectToWS")]
    public async Task ConnectToWS(ulong userId)
    {
        if (HttpContext.WebSockets.IsWebSocketRequest)
        {
            using var webSocket = await HttpContext.WebSockets.AcceptWebSocketAsync();
            await _webSocketHelper.Echo(webSocket, userId);
        }
        else
        {
            HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
        }
    }

    [HttpGet("Pagination")]
    public IActionResult Pagination(int pageSize = 10, int pageIndex = 0)
    {
        return Ok($"Skipped count: {pageSize * pageIndex}");
    }
}
