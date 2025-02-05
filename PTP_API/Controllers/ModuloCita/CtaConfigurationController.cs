using BussinessLayer.Atributes;
using BussinessLayer.DTOs.ModuloCitas.CtaCitaConfiguracion;
using BussinessLayer.Interfaces.Services.ModuloCitas;
using BussinessLayer.Wrappers;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net.Mime;

namespace PTP_API.Controllers.ModuloCita
{

    [ApiController]
    [SwaggerTag("Servicio de configuración de cuentas")]
    [Route("api/v1/[controller]")]
    [Authorize]
    [EnableBitacora]
    public class CtaConfigurationController : ControllerBase
    {
        private readonly ICtaConfiguracionService _ctaConfigurationService;
        private readonly IValidator<CtaConfiguracionRequest> _validator;

        public CtaConfigurationController(ICtaConfiguracionService ctaConfigurationService, IValidator<CtaConfiguracionRequest> validator)
        {
            _ctaConfigurationService = ctaConfigurationService;
            _validator = validator;
        }

        [HttpGet]
        [ProducesResponseType(typeof(Response<IEnumerable<CtaConfiguracionResponse>>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Obtener configuraciones de cuentas", Description = "Devuelve una lista de configuraciones o una configuración específica si se proporciona un ID")]
        [DisableBitacora]
        public async Task<IActionResult> GetAllCtaConfiguration([FromQuery] int? IdConfiguration)
        {
            try
            {
                if (IdConfiguration.HasValue)
                {
                    var configuration = await _ctaConfigurationService.GetByIdResponse(IdConfiguration);
                    if (configuration == null)
                    {
                        return NotFound(Response<CtaConfiguracionResponse>.NotFound("Configuración no encontrada."));
                    }
                    return Ok(Response<CtaConfiguracionResponse>.Success(configuration, "Configuración encontrada."));
                }
                else
                {
                    var configurations = await _ctaConfigurationService.GetAllDto();

                    if (configurations == null || !configurations.Any())
                    {
                        return StatusCode(204, Response<IEnumerable<CtaConfiguracionResponse>>.NoContent("No hay configuraciones disponibles."));
                    }
                    return Ok(Response<IEnumerable<CtaConfiguracionResponse>>.Success(configurations, "Configuraciones obtenidas correctamente."));
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
        [SwaggerOperation(Summary = "Crear una nueva configuración", Description = "Endpoint para crear una configuración nueva")]
        public async Task<IActionResult> CreateCtaConfiguration([FromBody] CtaConfiguracionRequest configurationDto)
        {
            try
            {
                var validationResult = await _validator.ValidateAsync(configurationDto);

                if (!validationResult.IsValid)
                {
                    var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                    return BadRequest(Response<string>.BadRequest(errors, 400));
                }

                var response = await _ctaConfigurationService.Add(configurationDto);

                return CreatedAtAction(nameof(GetAllCtaConfiguration), Response<CtaConfiguracionResponse>.Created(response));
            }
            catch (Exception ex)
            {
                return StatusCode(500, Response<string>.ServerError(ex.Message));
            }
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Actualizar configuración", Description = "Endpoint para actualizar los datos de una configuración")]
        public async Task<IActionResult> UpdateCtaConfiguration(int id, [FromBody] CtaConfiguracionRequest configurationDto)
        {
            var validationResult = await _validator.ValidateAsync(configurationDto);

            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return BadRequest(Response<string>.BadRequest(errors, 400));
            }

            try
            {
                var existingConfiguration = await _ctaConfigurationService.GetByIdRequest(id);
                if (existingConfiguration == null)
                    return NotFound(Response<string>.NotFound("Configuración no encontrada."));

                configurationDto.IdConfiguration = id;
                await _ctaConfigurationService.Update(configurationDto, id);
                return Ok(Response<string>.Success(null, "Configuración actualizada correctamente."));
            }
            catch (Exception ex)
            {
                return StatusCode(500, Response<string>.ServerError(ex.Message));
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Eliminar configuración", Description = "Endpoint para eliminar una configuración")]
        public async Task<IActionResult> DeleteCtaConfiguration(int id)
        {
            try
            {
                var existingConfiguration = await _ctaConfigurationService.GetByIdRequest(id);
                if (existingConfiguration == null)
                    return NotFound(Response<string>.NotFound("Configuración no encontrada."));

                await _ctaConfigurationService.Delete(id);
                return Ok(Response<string>.Success(null, "Configuración eliminada correctamente."));
            }
            catch (Exception ex)
            {
                return StatusCode(500, Response<string>.ServerError(ex.Message));
            }
        }
    }

}
