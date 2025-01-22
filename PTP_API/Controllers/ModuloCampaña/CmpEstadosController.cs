using AutoMapper;
using BussinessLayer.DTOs.ModuloCampaña.CmpEstado;
using BussinessLayer.Interfaces.ModuloCampaña.Repository;
using BussinessLayer.Wrappers;
using DataLayer.Models.ModuloCampaña;
using Microsoft.AspNetCore.Mvc;

namespace PTP_API.Controllers.ModuloCampaña
{
    [Route("api/[controller]")]
    [ApiController]
    public class CmpEstadosController(ICmpEstadoRepository repository, IMapper mapper) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                List<CmpEstado> estados = await repository.GetAllAsync();
                if (estados == null && estados.Count <= 0) return NoContent();

                List<CmpEstadoDto> estadoDtos = mapper.Map<List<CmpEstadoDto>>(estados);
                return Ok(Response<List<CmpEstadoDto>>.Success(estadoDtos));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                CmpEstado estados = await repository.GetByIdAsync(id);
                if (estados == null) return NoContent();

                CmpEstadoDto estadoDtos = mapper.Map<CmpEstadoDto>(estados);
                return Ok(Response<CmpEstadoDto>.Success(estadoDtos));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
