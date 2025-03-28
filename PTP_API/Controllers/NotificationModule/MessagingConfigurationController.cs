using BussinessLayer.DTOs.NotificationModule.MessagingConfiguration;
using BussinessLayer.Services.WhatsAppService.Contracts;
using BussinessLayer.Wrappers;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class MessagingConfigurationController(IMessagingConfigurationService messagingService) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Create(CreateMessagingConfigurationDto request)
    {
        Response<CreateMessagingConfigurationDto> response = await messagingService.CreateAsync(request);

        if(response.StatusCode == StatusCodes.Status400BadRequest)
        {
            return BadRequest(response.Errors);
        }
        return Ok(response);
    }

    [HttpGet]
    [Route("{businessId}")]
    public async Task<IActionResult> GetAll([FromRoute]int businessId)
    {
        Response<List<MessagingConfigurationDto>> response = await messagingService.GetAllAsync(businessId);

        if(response.StatusCode == StatusCodes.Status400BadRequest)
        {
            return BadRequest(response.Errors);
        }
        return response.StatusCode == StatusCodes.Status200OK ? Ok(response) : NoContent(); 
    }

    [HttpGet]
    [Route("{configurationId}/{businessId}")]
    public async Task<IActionResult> GetById([FromRoute]int configurationId, [FromRoute]int businessId)
    {
        Response<MessagingConfigurationDto> response = await messagingService.GetByIdAsync(configurationId, businessId);

        if(response.StatusCode == StatusCodes.Status400BadRequest)
        {
            return BadRequest(response.Errors);
        }

        return response.StatusCode == StatusCodes.Status200OK ? Ok(response) : NoContent(); 
    }
}
