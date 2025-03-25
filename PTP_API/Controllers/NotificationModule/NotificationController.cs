
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class NotificationController : ControllerBase
{
    [HttpPost]
    [Route("send-email")]
    public async Task<IActionResult> SendEmail([FromBody] SendGridRequest request)
    {
        SendGridService sendGridService = new SendGridService();
        var response = await sendGridService.SendEmailAsync();
        return Ok(response);
    }
}
