using System.Net.Mime;
using BussinessLayer.DTOs.ModuloGeneral.Seguridad.GnSecurityParameters;
using BussinessLayer.Interfaces.Services.ModuloGeneral.Seguridad;
using BussinessLayer.Wrappers;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace PTP_API.Controllers.ModuloGeneral.Seguridad
{
    [ApiController]
    [SwaggerTag("Gestión de GnSecurityParameters")]
    [Route("api/v1/[controller]")]
    [Authorize]
    public class GnSecurityParametersController : ControllerBase
    {
        private readonly IGnSecurityParametersService _gnsecurityparametersService;
        private readonly IValidator<GnSecurityParametersRequest> _validator;

        public GnSecurityParametersController(IGnSecurityParametersService gnsecurityparametersService, IValidator<GnSecurityParametersRequest> validator)
        {
            _gnsecurityparametersService = gnsecurityparametersService;
            _validator = validator;
        }

        [HttpGet]
        [ProducesResponseType(typeof(Response<IEnumerable<GnSecurityParametersResponse>>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Obtener GnSecurityParameters", Description = "Devuelve una lista de GnSecurityParameters o un elemento específico si se proporciona un ID")]
        public async Task<IActionResult> GetAll([FromQuery] long? companyId)
        {
            try
            {
                if (companyId.HasValue)
                {
                    var item = await _gnsecurityparametersService.GetByIdResponse(companyId.Value);
                    if (item == null)
                        return NotFound(Response<GnSecurityParametersResponse>.NotFound("GnSecurityParameters no encontrado."));

                    return Ok(Response<GnSecurityParametersResponse>.Success(item, "GnSecurityParameters encontrado."));
                }
                else
                {
                    var items = await _gnsecurityparametersService.GetAllDto();
                    if (items == null || !items.Any())
                        return StatusCode(204, Response<IEnumerable<GnSecurityParametersResponse>>.NoContent("No hay GnSecurityParameters disponibles."));

                    return Ok(Response<IEnumerable<GnSecurityParametersResponse>>.Success(items, "GnSecurityParameters obtenidos correctamente."));
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, Response<string>.ServerError(ex.Message));
            }
        }

        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Crear GnSecurityParameters", Description = "Endpoint para registrar un nuevo GnSecurityParameters")]
        public async Task<IActionResult> Create([FromBody] GnSecurityParametersRequest request)
        {
            try
            {
                var validationResult = await _validator.ValidateAsync(request);

                if (!validationResult.IsValid)
                {
                    var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                    return BadRequest(Response<string>.BadRequest(errors, 400));
                }

                var response = await _gnsecurityparametersService.Add(request);
                return CreatedAtAction(nameof(GetAll), Response<GnSecurityParametersResponse>.Created(response));
            }
            catch (Exception ex)
            {
                return StatusCode(500, Response<string>.ServerError(ex.Message));
            }
        }

        [HttpPut("{companyId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Actualizar GnSecurityParameters", Description = "Endpoint para actualizar un GnSecurityParameters")]
        public async Task<IActionResult> Update(long companyId, [FromBody] GnSecurityParametersRequest request)
        {
            try
            {
                var validationResult = await _validator.ValidateAsync(request);

                if (!validationResult.IsValid)
                {
                    var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                    return BadRequest(Response<string>.BadRequest(errors, 400));
                }

                var existingItem = await _gnsecurityparametersService.GetByIdRequest(companyId);
                if (existingItem == null)
                    return NotFound(Response<string>.NotFound("GnSecurityParameters no encontrado."));

                request.CompanyId = companyId;
                await _gnsecurityparametersService.Update(request, companyId);
                return Ok(Response<string>.Success(null, "GnSecurityParameters actualizado correctamente."));
            }
            catch (Exception ex)
            {
                return StatusCode(500, Response<string>.ServerError(ex.Message));
            }
        }

        [HttpDelete("{companyId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Eliminar GnSecurityParameters", Description = "Endpoint para eliminar un GnSecurityParameters")]
        public async Task<IActionResult> Delete(long companyId)
        {
            try
            {
                var existingItem = await _gnsecurityparametersService.GetByIdRequest(companyId);
                if (existingItem == null)
                    return NotFound(Response<string>.NotFound("GnSecurityParameters no encontrado."));

                await _gnsecurityparametersService.Delete(companyId);
                return Ok(Response<string>.Success(null, "GnSecurityParameters eliminado correctamente."));
            }
            catch (Exception ex)
            {
                return StatusCode(500, Response<string>.ServerError(ex.Message));
            }
        }
    }
}
