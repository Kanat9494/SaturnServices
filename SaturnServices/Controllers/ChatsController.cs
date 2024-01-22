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
}
