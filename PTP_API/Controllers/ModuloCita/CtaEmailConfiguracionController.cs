using BussinessLayer.Atributes;
using BussinessLayer.DTOs.ModuloCitas.CtaEmailConfiguracion;
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
    [SwaggerTag("Gestión de configuraciones de correo electrónico")]
    [Route("api/v1/[controller]")]
    [Authorize]
    [EnableBitacora]
    public class CtaEmailConfiguracionController : ControllerBase
    {
        private readonly ICtaEmailConfiguracionService _emailConfigService;
        private readonly IValidator<CtaEmailConfiguracionRequest> _validator;

        public CtaEmailConfiguracionController(ICtaEmailConfiguracionService emailConfigService, IValidator<CtaEmailConfiguracionRequest> validator)
        {
            _emailConfigService = emailConfigService;
            _validator = validator;
        }

        [HttpGet]
        [ProducesResponseType(typeof(Response<IEnumerable<CtaEmailConfiguracionResponse>>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Obtener configuraciones de correo electrónico", Description = "Devuelve una lista de configuraciones de correo electrónico o una configuración específica si se proporciona un ID")]
        public async Task<IActionResult> GetAllConfigurations([FromQuery] int? IdEmailConfiguration, long? companyId)
        {
            try
            {
                if (IdEmailConfiguration.HasValue)
                {
                    var config = await _emailConfigService.GetByIdResponse(IdEmailConfiguration.Value);
                    if (config == null)
                        return NotFound(Response<CtaEmailConfiguracionResponse>.NotFound("Configuración no encontrada."));

                    return Ok(Response<CtaEmailConfiguracionResponse>.Success(config, "Configuración encontrada."));
                }
                else
                {
                    var configurations = await _emailConfigService.GetAllDto();
                    if (configurations == null || !configurations.Any())
                        return StatusCode(204, Response<IEnumerable<CtaEmailConfiguracionResponse>>.NoContent("No hay configuraciones disponibles."));

                    return Ok(Response<IEnumerable<CtaEmailConfiguracionResponse>>.Success(
                        companyId != null ? configurations.Where(x => x.CompanyId == companyId).ToList() : configurations, "Configuraciones obtenidas correctamente."));
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
        [SwaggerOperation(Summary = "Crear una nueva configuración de correo", Description = "Endpoint para registrar una configuración de correo electrónico")]
        public async Task<IActionResult> CreateConfiguration([FromBody] CtaEmailConfiguracionRequest emailConfigDto)
        {
            try
            {
                var validationResult = await _validator.ValidateAsync(emailConfigDto);

                if (!validationResult.IsValid)
                {
                    var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                    return BadRequest(Response<string>.BadRequest(errors, 400));
                }

                var response = await _emailConfigService.Add(emailConfigDto);
                return CreatedAtAction(nameof(GetAllConfigurations), response);
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
        [SwaggerOperation(Summary = "Actualizar una configuración de correo", Description = "Endpoint para actualizar una configuración de correo electrónico")]
        public async Task<IActionResult> UpdateConfiguration(int id, [FromBody] CtaEmailConfiguracionRequest emailConfigDto)
        {
            try
            {
                var validationResult = await _validator.ValidateAsync(emailConfigDto);

                if (!validationResult.IsValid)
                {
                    var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                    return BadRequest(Response<string>.BadRequest(errors, 400));
                }

                var existingConfig = await _emailConfigService.GetByIdRequest(id);
                if (existingConfig == null)
                    return NotFound(Response<string>.NotFound("Configuración no encontrada."));

                emailConfigDto.IdEmailConfiguration = id;
                await _emailConfigService.Update(emailConfigDto, id);
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
        [SwaggerOperation(Summary = "Eliminar una configuración de correo", Description = "Endpoint para eliminar una configuración de correo electrónico")]
        public async Task<IActionResult> DeleteConfiguration(int id)
        {
            try
            {
                var existingConfig = await _emailConfigService.GetByIdRequest(id);
                if (existingConfig == null)
                    return NotFound(Response<string>.NotFound("Configuración no encontrada."));

                await _emailConfigService.Delete(id);
                return Ok(Response<string>.Success(null, "Configuración eliminada correctamente."));
            }
            catch (Exception ex)
            {
                return StatusCode(500, Response<string>.ServerError(ex.Message));
            }
        }
    }

}
