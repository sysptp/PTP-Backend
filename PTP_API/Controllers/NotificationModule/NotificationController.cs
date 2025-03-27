
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class NotificationController : ControllerBase
{
    private readonly ITwilioService _twilioService;
    public NotificationController(ITwilioService twilioService)
    {
        _twilioService = twilioService;
    }

    [HttpPost]
    [Route("send-email")]
    public async Task<IActionResult> SendEmail([FromBody] SendGridRequest request)
    {
        SendGridService sendGridService = new SendGridService();
        var response = await sendGridService.SendEmailAsync();
        return Ok(response);
    }

    [HttpPost]
    [Route("send-sms-whatsapp")]
    public async Task<IActionResult> SendSmsWhatsapp([FromBody]  SendMessageDto sendMessageDto )
    {
        var response = await _twilioService.SendMessage(sendMessageDto);
        return Ok(response);
    }
}
