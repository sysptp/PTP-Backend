using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using Swashbuckle.AspNetCore.Annotations;
using BussinessLayer.Wrappers;
using Microsoft.AspNetCore.Authorization;
using FluentValidation;
using BussinessLayer.Atributes;
using BussinessLayer.Interfaces.ModuloGeneral.Geografia;
using BussinessLayer.DTOs.ModuloGeneral.Geografia.DRegion;

namespace PTP_API.Controllers.ModuloGeneral.Geografia
{
    [ApiController]
    [Route("api/v1/Region")]
    [SwaggerTag("Gestión de Regiones")]
    [Authorize]
    [EnableAuditing]
    public class RegionController : ControllerBase
    {
        private readonly IRegionService _regionService;
        private readonly IValidator<RegionRequest> _validator;

        public RegionController(IRegionService regionService, IValidator<RegionRequest> validator)
        {
            _regionService = regionService;
            _validator = validator;
        }

        [HttpGet]
        [EnableAuditing]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [SwaggerOperation(Summary = "Obtener regiones", Description = "Obtiene una lista de todas las regiones o una región específica si se proporciona un ID.")]
        [DisableAuditing]
        public async Task<IActionResult> Get([FromQuery] int? id, int? countryId)
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
                   return Ok(Response<IEnumerable<RegionResponse>>.Success(countryId == null ? regions : regions.Where(x => x.CountryId == countryId), "Regiones obtenidas correctamente."));
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
            var validationResult = await _validator.ValidateAsync(request);

            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
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

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Actualizar región", Description = "Endpoint para actualizar los datos de una región")]
        public async Task<IActionResult> UpdateRegion(int id, [FromBody] RegionRequest request)
        {
            var validationResult = await _validator.ValidateAsync(request);

            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return BadRequest(Response<string>.BadRequest(errors, 400));
            }

            try
            {
                var existingRegion = await _regionService.GetByIdRequest(id);
                if (existingRegion == null)
                    return NotFound(Response<string>.NotFound("Región no encontrada"));

                request.Id = id;
                await _regionService.Update(request, id);
                return Ok(Response<string>.Success(null, "Región actualizada correctamente"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, Response<string>.ServerError("Ocurrió un error al actualizar la región. Por favor, intente nuevamente."));
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Eliminar región", Description = "Endpoint para eliminar una región")]
        public async Task<IActionResult> DeleteRegion(int id)
        {
            try
            {
                var existingRegion = await _regionService.GetByIdRequest(id);
                if (existingRegion == null)
                    return NotFound(Response<string>.NotFound("Región no encontrada"));

                await _regionService.Delete(id);
                return Ok(Response<string>.Success(null, "Región eliminada correctamente"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, Response<string>.ServerError("Ocurrió un error al eliminar la región. Por favor, intente nuevamente."));
            }
        }

    }
}
