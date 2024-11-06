using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using Swashbuckle.AspNetCore.Annotations;
using BussinessLayer.Wrappers;
using Microsoft.AspNetCore.Authorization;
using BussinessLayer.Interfaces.IGeografia;
using BussinessLayer.DTOs.Configuracion.Geografia.DMunicipio;

namespace PTP_API.Controllers.ModuloGeneral.Geografia
{
    [ApiController]
    [Route("api/v1/Municipality")]
    [SwaggerTag("Gestión de Municipios")]
    [Authorize]
    public class MunicipalityController : ControllerBase
    {
        private readonly IMunicipioService _municipalityService;

        public MunicipalityController(IMunicipioService municipalityService)
        {
            _municipalityService = municipalityService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [SwaggerOperation(Summary = "Obtener municipios", Description = "Obtiene una lista de todos los municipios o un municipio específico si se proporciona un ID.")]
        public async Task<IActionResult> Get([FromQuery] int? id)
        {
            try
            {
                if (id.HasValue)
                {
                    var municipality = await _municipalityService.GetByIdResponse((int)id);
                    if (municipality == null)
                    {
                        return NotFound(Response<MunicipioResponse>.NotFound("Municipio no encontrado."));
                    }
                    return Ok(Response<MunicipioResponse>.Success(municipality, "Municipio encontrado."));
                }
                else
                {
                    var municipalities = await _municipalityService.GetAllDto();
                    if (municipalities == null || !municipalities.Any())
                    {
                        return StatusCode(204, Response<IEnumerable<MunicipioResponse>>.NoContent("No hay municipios disponibles."));
                    }
                    return Ok(Response<IEnumerable<MunicipioResponse>>.Success(municipalities, "Municipios obtenidos correctamente."));
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, Response<string>.ServerError("Ocurrió un error al obtener los municipios. Por favor, intente nuevamente."));
            }
        }

        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [SwaggerOperation(Summary = "Crear un nuevo municipio", Description = "Crea un nuevo municipio en el sistema.")]
        public async Task<IActionResult> Add([FromBody] MunicipioRequest request)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                return BadRequest(Response<string>.BadRequest(errors, 400));
            }

            try
            {
                var municipality = await _municipalityService.Add(request);
                return Ok(Response<MunicipioResponse>.Created(municipality, "Municipio creado correctamente."));
            }
            catch (Exception ex)
            {
                return StatusCode(500, Response<string>.ServerError("Ocurrió un error al crear el municipio. Por favor, intente nuevamente."));
            }
        }
    }
}
