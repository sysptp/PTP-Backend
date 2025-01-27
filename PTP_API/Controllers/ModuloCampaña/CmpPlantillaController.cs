using BussinessLayer.DTOs.ModuloCampaña.CmpPlantillas;
using BussinessLayer.Interfaces.ModuloCampaña.Services;
using BussinessLayer.Wrappers;
using Microsoft.AspNetCore.Mvc;

namespace PTP_API.Controllers.ModuloCampaña
{
    [Route("api/[controller]")]
    [ApiController]
    public class CmpPlantillaController(ICmpPlantillaService service) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Created(CmpPlantillaCreateDto dto)
        {
            Response<CmpPlantillaCreateDto> response = await service.CreateAsync(dto);
            return response.Succeeded ? Created("Recurso creado con exito", response) : BadRequest(response);
        }
        [HttpGet]
        public async Task<IActionResult> GetAll(int empresaId)
        {
            Response<List<CmpPlantillaDto>> response = await service.GetAllAsync(empresaId);
            return response.Succeeded ? Ok(response) : BadRequest(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id, int empresaId)
        {
            Response<CmpPlantillaDto> response = await service.GetByIdAsync(id, empresaId);
            return response.Succeeded ? Ok(response) : BadRequest(response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(CmpPlantillaUpdateDto dto)
        {
            Response<CmpPlantillaUpdateDto> response = await service.UpdateAsync(dto);
            return response.Succeeded ? Ok(response) : BadRequest(response);

        }
    }
}
