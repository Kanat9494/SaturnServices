namespace SaturnServices.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ChatsController : ControllerBase
{
    public ChatsController(WebSocketHelper webSocketHelper)
    {
        _webSocketHelper = webSocketHelper;
    }

    WebSocketHelper _webSocketHelper;

    [HttpGet("Test")]
    public IActionResult Test2()
        => Ok("Работает");

    [HttpGet("ConnectToWS")]
    public async Task ConnectToWS(ulong userId)
    {
        try
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
        catch (Exception ex)
        {

        }
    }

    [HttpGet("Pagination")]
    public IActionResult Pagination(int pageSize = 10, int pageIndex = 0)
    {
        return Ok($"Skipped count: {pageSize * pageIndex}");
    }

    [HttpPost("SendTelegramMessage")]
    public async Task<IActionResult> SendTelegramMessage([FromBody] TelegramMessage telegramMessage)
    {
        if (string.IsNullOrEmpty(telegramMessage.Message))
        {
            return BadRequest("Сообщение не может быть пустым");
        }

        await _webSocketHelper.SendTelegramMessage(telegramMessage.Message);

        return Ok("Сообщение успешно отправлено");
    }
}
