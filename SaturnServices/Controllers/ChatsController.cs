﻿namespace SaturnServices.Controllers;

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

    [HttpGet("ConnectToWebSocket")]
    public async Task ConnectToWebSocket()
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
