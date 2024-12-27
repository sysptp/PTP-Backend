using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using Swashbuckle.AspNetCore.Annotations;
using BussinessLayer.Wrappers;
using Microsoft.AspNetCore.Authorization;
using FluentValidation;
using BussinessLayer.Atributes;
using BussinessLayer.DTOs.ModuloGeneral.Geografia.DMunicipio;
using BussinessLayer.Interfaces.Services.ModuloGeneral.Geografia;

namespace PTP_API.Controllers.ModuloGeneral.Geografia
{
    [ApiController]
    [Route("api/v1/Municipality")]
    [SwaggerTag("Gestión de Municipios")]
    [Authorize]
    [EnableBitacora]
    public class MunicipalityController : ControllerBase
    {
        private readonly IMunicipioService _municipalityService;
        private readonly IValidator<MunicipioRequest> _validator;

        public MunicipalityController(IMunicipioService municipalityService, IValidator<MunicipioRequest> validator)
        {
            _municipalityService = municipalityService;
            _validator = validator;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [SwaggerOperation(Summary = "Obtener municipios", Description = "Obtiene una lista de todos los municipios o un municipio específico si se proporciona un ID.")]
        [DisableBitacora]
        public async Task<IActionResult> Get([FromQuery] int? id,int? provinceId)
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
                    return Ok(Response<IEnumerable<MunicipioResponse>>.Success(provinceId == null ? municipalities : municipalities.Where(x => x.ProvinceId == provinceId), "Municipios obtenidos correctamente."));
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
            var validationResult = await _validator.ValidateAsync(request);

            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
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

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Actualizar municipio", Description = "Endpoint para actualizar los datos de un municipio")]
        public async Task<IActionResult> UpdateMunicipality(int id, [FromBody] MunicipioRequest request)
        {
            var validationResult = await _validator.ValidateAsync(request);

            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return BadRequest(Response<string>.BadRequest(errors, 400));
            }

            try
            {
                var existingMunicipality = await _municipalityService.GetByIdRequest(id);
                if (existingMunicipality == null)
                    return NotFound(Response<string>.NotFound("Municipio no encontrado"));

                request.Id = id;
                await _municipalityService.Update(request, id);
                return Ok(Response<string>.Success(null, "Municipio actualizado correctamente"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, Response<string>.ServerError("Ocurrió un error al actualizar el municipio. Por favor, intente nuevamente."));
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Eliminar municipio", Description = "Endpoint para eliminar un municipio")]
        public async Task<IActionResult> DeleteMunicipality(int id)
        {
            try
            {
                var existingMunicipality = await _municipalityService.GetByIdRequest(id);
                if (existingMunicipality == null)
                    return NotFound(Response<string>.NotFound("Municipio no encontrado"));

                await _municipalityService.Delete(id);
                return Ok(Response<string>.Success(null, "Municipio eliminado correctamente"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, Response<string>.ServerError("Ocurrió un error al eliminar el municipio. Por favor, intente nuevamente."));
            }
        }

    }
}
