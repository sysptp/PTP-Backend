using BussinessLayer.DTOs.NotificationModule.MessagingConfiguration;
using BussinessLayer.Services.NotificationModule.Contracts;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class NotificationController : ControllerBase
{
    private readonly IMessageService _messageService;

    public NotificationController(IMessageService messageService)
    {
        _messageService = messageService;
    }

    [HttpPost]
    [Route("send-message")]
    public async Task<IActionResult> SendMessage([FromBody]  SendMessageDto sendMessageDto )
    {
        var response = await _messageService.SendMessage(sendMessageDto);
        return Ok(response);
    }
}
