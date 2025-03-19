using BussinessLayer.DTOs.WhatsAppModule.WhatsAppConfiguration;
using BussinessLayer.Services.WhatsAppService.Contracts;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class WhatsAppConfigurationController : ControllerBase{

    private readonly IWhatsAppConfigurationService _whatsAppConfigurationService;

    public WhatsAppConfigurationController(IWhatsAppConfigurationService whatsAppConfigurationService)
    {
        _whatsAppConfigurationService = whatsAppConfigurationService;
    }

    [HttpPost]
    public async Task<IActionResult> Create(WhatsAppConfigurationDto request)
    {
        var response = await _whatsAppConfigurationService.CreateWhatsAppConfiguration(request);
        return Ok(response);
    }

    [HttpGet]
    [Route("{businessId}")]
    public async Task<IActionResult> GetAllWhatsAppConfigurations([FromRoute]int businessId)
    {
        var response = await _whatsAppConfigurationService.GetAllWhatsAppConfigurations(businessId);

        if(response.StatusCode == StatusCodes.Status400BadRequest)
        {
            return BadRequest(response.Errors);
        }

        return response.StatusCode == StatusCodes.Status200OK ? Ok(response) : NoContent(); 
    }

    [HttpGet]
    [Route("{configurationId}/{businessId}")]
    public async Task<IActionResult> GetWhatsAppConfiguration([FromRoute]int configurationId, [FromRoute]int businessId)
    {
        var response = await _whatsAppConfigurationService.GetWhatsAppConfiguration(configurationId, businessId);

        if(response.StatusCode == StatusCodes.Status400BadRequest)
        {
            return BadRequest(response.Errors);
        }

        return response.StatusCode == StatusCodes.Status200OK ? Ok(response) : NoContent(); 
    }
    
}
