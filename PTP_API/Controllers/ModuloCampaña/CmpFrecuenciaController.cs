using AutoMapper;
using BussinessLayer.DTOs.ModuloCampaña.CmpFrecuencia;
using BussinessLayer.Interfaces.ModuloCampaña.Repository;
using BussinessLayer.Wrappers;
using DataLayer.Models.ModuloCampaña;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PTP_API.Controllers.ModuloCampaña
{
    [Route("api/[controller]")]
    [ApiController]
    public class CmpFrecuenciaController(ICmpFrecuenciaRepository repository,IMapper mapper) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                List<CmpFrecuencia> cmpFrecuencias = await repository.GetAll();
                if (cmpFrecuencias == null) return NoContent();

                List<CmpFrecuenciaDto> frecuenciaDtos = mapper.Map<List<CmpFrecuenciaDto>>(cmpFrecuencias);

                var response = Response<List<CmpFrecuenciaDto>>.Success(frecuenciaDtos);

                return Ok(response);
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
                CmpFrecuencia cmpFrecuencia = await repository.GetById(id);
                if (cmpFrecuencia == null) return NoContent();
                CmpFrecuenciaDto frecuenciaDto = mapper.Map<CmpFrecuenciaDto>(cmpFrecuencia);

                var response = Response<CmpFrecuenciaDto>.Success(frecuenciaDto);

                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
