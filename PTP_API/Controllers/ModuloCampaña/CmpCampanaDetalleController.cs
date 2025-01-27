using BussinessLayer.Interfaces.ModuloCampaña.Services;
using Microsoft.AspNetCore.Mvc;

namespace PTP_API.Controllers.ModuloCampaña;

[Route("api/[controller]")]
[ApiController]
public class CmpCampanaDetalleController(ICmpCampanaDetalleService service) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll(int empresaId)
    {
        return Ok(await service.GetAll(empresaId));
    }
}