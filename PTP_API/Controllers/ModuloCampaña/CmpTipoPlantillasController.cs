using AutoMapper;
using BussinessLayer.DTOs.ModuloCampaña.CmpTipoPlantillas;
using BussinessLayer.FluentValidations;
using BussinessLayer.Interfaces.ModuloCampaña.Repository;
using BussinessLayer.Wrappers;
using DataLayer.Models.ModuloCampaña;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PTP_API.Controllers.ModuloCampaña
{
    [Route("api/[controller]")]
    [ApiController]
    public class CmpTipoPlantillasController(ICmpTipoPlantillaRepository repository, IMapper mapper, IValidateService<CmpTipoPlantillaCreateDto> postValidate) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CmpTipoPlantillaCreateDto plantillaCreateDto)
        {
            try
            {
                Response<CmpTipoPlantillaCreateDto> result;
                List<string> errors = postValidate.Validate(plantillaCreateDto);
                if (errors != null)
                {
                    result = Response<CmpTipoPlantillaCreateDto>.BadRequest(errors);
                    return StatusCode(StatusCodes.Status400BadRequest, result);
                }
                CmpTipoPlantilla tipoPlantilla = mapper.Map<CmpTipoPlantilla>(plantillaCreateDto);
                await repository.AddAsync(tipoPlantilla);
                result = Response<CmpTipoPlantillaCreateDto>.Created(plantillaCreateDto);
                return StatusCode(StatusCodes.Status201Created, result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpGet]
        public async Task<IActionResult> Get(int empresaId)
        {
            try
            {
                Response<List<CmpTipoPlantillaDto>> result;

                List<CmpTipoPlantilla> tipoPlantillas = await repository.GetAllAsync(empresaId);
                if (tipoPlantillas != null && tipoPlantillas.Count > 0)
                {
                    List<CmpTipoPlantillaDto> plantillaDtos = mapper.Map<List<CmpTipoPlantillaDto>>(tipoPlantillas);
                    result = Response<List<CmpTipoPlantillaDto>>.Success(plantillaDtos);
                    return StatusCode(StatusCodes.Status200OK, result);
                }
                return StatusCode(StatusCodes.Status204NoContent);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id,int empresaId)
        {
            try
            {
                Response<CmpTipoPlantillaDto> result;

                CmpTipoPlantilla tipoPlantillas = await repository.GetByIdAsync(id, empresaId);
                if (tipoPlantillas != null)
                {
                    CmpTipoPlantillaDto plantillaDtos = mapper.Map<CmpTipoPlantillaDto>(tipoPlantillas);
                    result = Response<CmpTipoPlantillaDto>.Success(plantillaDtos);
                    return StatusCode(StatusCodes.Status200OK, result);
                }
                return StatusCode(StatusCodes.Status204NoContent);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

    }
}
