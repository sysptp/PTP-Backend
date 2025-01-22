using BussinessLayer.DTOs.ModuloCampaña.CmpEmail;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PTP_API.Controllers.ModuloCampaña
{
    [Route("api/[controller]")]
    [ApiController]
    public class CmpEmailController(ICmpEmailService emailService) : ControllerBase
    {
        [HttpPost("send-email")]
        public async Task<IActionResult> SendEmail([FromForm] CmpEmailMessageDto cmpEmailMessageDto)
        {
            try
            {
                await emailService.SendEmailAsync(cmpEmailMessageDto);
                return Ok();
            }
            catch (Exception ex)
            {
                throw new ApplicationException(ex.Message, ex);
            }
        }
    }
}
