using BussinessLayer.DTOs.ModuloCampaña.CmpCampana;
using BussinessLayer.Interfaces.ModuloCampaña.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PTP_API.Controllers.ModuloCampaña
{
    [Route("api/[controller]")]
    [ApiController]
    public class CmpCampanaController(ICmpCampanaService service) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Create(CmpCampanaCreateDto dto)
        {
            await service.CreateAsync(dto);
            return Ok(dto);
        }
    }
}
