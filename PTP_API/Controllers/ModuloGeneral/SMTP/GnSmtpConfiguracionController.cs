using System.Net.Mime;
using BussinessLayer.DTOs.ModuloGeneral.Smtp;
using BussinessLayer.Interfaces.Services.IModuloGeneral.SMTP;
using BussinessLayer.Wrappers;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace PTP_API.Controllers
{
    [ApiController]
    [SwaggerTag("Gestión de Configuración SMTP")]
    [Route("api/v1/[controller]")]
    [Authorize]
    public class GnSmtpConfiguracionController : ControllerBase
    {
        private readonly IGnSmtpConfiguracionService _smtpService;
        private readonly IValidator<GnSmtpConfiguracionRequest> _validator;

        public GnSmtpConfiguracionController(IGnSmtpConfiguracionService smtpService, IValidator<GnSmtpConfiguracionRequest> validator)
        {
            _smtpService = smtpService;
            _validator = validator;
        }

        [HttpGet]
        [ProducesResponseType(typeof(Response<IEnumerable<GnSmtpConfiguracionResponse>>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Obtener Configuraciones SMTP", Description = "Devuelve una lista de configuraciones SMTP o una configuración específica si se proporciona un ID")]
        public async Task<IActionResult> GetAllSmtpConfigs([FromQuery] int? id, long? empresaId)
        {
            try
            {
                if (id.HasValue)
                {
                    var config = await _smtpService.GetByIdResponse(id.Value);
                    if (config == null)
                        return NotFound(Response<GnSmtpConfiguracionResponse>.NotFound("Configuración no encontrada."));

                    return Ok(Response<GnSmtpConfiguracionResponse>.Success(config, "Configuración encontrada."));
                }
                else
                {
                    var configs = await _smtpService.GetAllDto();
                    if (configs == null || !configs.Any())
                        return StatusCode(204, Response<IEnumerable<GnSmtpConfiguracionResponse>>.NoContent("No hay configuraciones disponibles."));

                    return Ok(Response<IEnumerable<GnSmtpConfiguracionResponse>>.Success(
                        empresaId != null ? configs.Where(x => x.IdEmpresa == empresaId).ToList() : configs, "Configuraciones obtenidas correctamente."));
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
        [SwaggerOperation(Summary = "Agregar una nueva configuración SMTP", Description = "Endpoint para registrar una configuración SMTP")]
        public async Task<IActionResult> CreateSmtpConfig([FromBody] GnSmtpConfiguracionRequest configDto)
        {
            try
            {
                var validationResult = await _validator.ValidateAsync(configDto);

                if (!validationResult.IsValid)
                {
                    var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                    return BadRequest(Response<string>.BadRequest(errors, 400));
                }

                var response = await _smtpService.Add(configDto);
                return CreatedAtAction(nameof(GetAllSmtpConfigs), new { id = response.IdSmtp }, Response<GnSmtpConfiguracionResponse>.Created(response));
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
        [SwaggerOperation(Summary = "Actualizar una configuración SMTP", Description = "Endpoint para actualizar una configuración SMTP")]
        public async Task<IActionResult> UpdateSmtpConfig(int id, [FromBody] GnSmtpConfiguracionRequest configDto)
        {
            try
            {
                var validationResult = await _validator.ValidateAsync(configDto);

                if (!validationResult.IsValid)
                {
                    var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                    return BadRequest(Response<string>.BadRequest(errors, 400));
                }

                var existingConfig = await _smtpService.GetByIdRequest(id);
                if (existingConfig == null)
                    return NotFound(Response<string>.NotFound("Configuración no encontrada."));

                configDto.IdSmtp = id;
                await _smtpService.Update(configDto, id);
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
        [SwaggerOperation(Summary = "Eliminar una configuración SMTP", Description = "Endpoint para eliminar una configuración SMTP")]
        public async Task<IActionResult> DeleteSmtpConfig(int id)
        {
            try
            {
                var existingConfig = await _smtpService.GetByIdRequest(id);
                if (existingConfig == null)
                    return NotFound(Response<string>.NotFound("Configuración no encontrada."));

                await _smtpService.Delete(id);
                return Ok(Response<string>.Success(null, "Configuración eliminada correctamente."));
            }
            catch (Exception ex)
            {
                return StatusCode(500, Response<string>.ServerError(ex.Message));
            }
        }
    }
}
