using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using Swashbuckle.AspNetCore.Annotations;
using BussinessLayer.DTOs.Geografia.DRegion;
using BussinessLayer.Wrappers;
using Microsoft.AspNetCore.Authorization;
using BussinessLayer.Interfaces.IGeografia;

namespace PTP_API.Controllers.Geografia
{
    [ApiController]
    [Route("api/v1/Region")]
    [SwaggerTag("Gestión de Regiones")]
    [Authorize]
    public class RegionController : ControllerBase
    {
        private readonly IRegionService _regionService;

        public RegionController(IRegionService regionService)
        {
            _regionService = regionService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [SwaggerOperation(Summary = "Obtener regiones", Description = "Obtiene una lista de todas las regiones o una región específica si se proporciona un ID.")]
        public async Task<IActionResult> Get([FromQuery] int? id)
        {
            try
            {
                if (id.HasValue)
                {
                    var region = await _regionService.GetByIdResponse((int)id);
                    if (region == null)
                    {
                        return NotFound(Response<RegionResponse>.NotFound("Región no encontrada."));
                    }
                    return Ok(Response<RegionResponse>.Success(region, "Región encontrada."));
                }
                else
                {
                    var regions = await _regionService.GetAllDto();
                    if (regions == null || !regions.Any())
                    {
                        return StatusCode(204, Response<IEnumerable<RegionResponse>>.NoContent("No hay regiones disponibles."));
                    }
                    return Ok(Response<IEnumerable<RegionResponse>>.Success(regions, "Regiones obtenidas correctamente."));
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, Response<string>.ServerError("Ocurrió un error al obtener las regiones. Por favor, intente nuevamente."));
            }
        }

        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [SwaggerOperation(Summary = "Crear una nueva región", Description = "Crea una nueva región en el sistema.")]
        public async Task<IActionResult> Add([FromBody] RegionRequest request)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                return BadRequest(Response<string>.BadRequest(errors, 400));
            }

            try
            {
                var region = await _regionService.Add(request);
                return Ok(Response<RegionResponse>.Created(region, "Región creada correctamente."));
            }
            catch (Exception ex)
            {
                return StatusCode(500, Response<string>.ServerError("Ocurrió un error al crear la región. Por favor, intente nuevamente."));
            }
        }
    }
}
