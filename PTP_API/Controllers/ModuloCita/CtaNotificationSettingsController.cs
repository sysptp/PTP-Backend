using System.Net.Mime;
using BussinessLayer.DTOs.ModuloCitas.CtaNotificationSettings;
using BussinessLayer.Interfaces.Services.ModuloCitas;
using BussinessLayer.Wrappers;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace PTP_API.Controllers.ModuloCita
{
    [ApiController]
    [SwaggerTag("Gestión de Configuraciones de Notificaciones")]
    [Route("api/v1/[controller]")]
    [Authorize]
    public class CtaNotificationSettingsController : ControllerBase
    {
        private readonly ICtaNotificationSettingsService _notificationSettingsService;
        private readonly IValidator<CtaNotificationSettingsRequest> _validator;

        public CtaNotificationSettingsController(ICtaNotificationSettingsService notificationSettingsService, IValidator<CtaNotificationSettingsRequest> validator)
        {
            _notificationSettingsService = notificationSettingsService;
            _validator = validator;
        }

        [HttpGet]
        [ProducesResponseType(typeof(Response<IEnumerable<CtaNotificationSettingsResponse>>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Obtener Configuraciones de Notificaciones", Description = "Devuelve una lista de configuraciones o una configuración específica si se proporciona un ID")]
        public async Task<IActionResult> GetAllNotificationSettings([FromQuery] long? id, long? companyId)
        {
            try
            {
                if (id.HasValue)
                {
                    var setting = await _notificationSettingsService.GetByIdResponse(id.Value);
                    if (setting == null)
                        return NotFound(Response<CtaNotificationSettingsResponse>.NotFound("Configuración no encontrada."));

                    return Ok(Response<CtaNotificationSettingsResponse>.Success(setting, "Configuración encontrada."));
                }
                else
                {
                    var settings = await _notificationSettingsService.GetAllDto();
                    if (settings == null || !settings.Any())
                        return StatusCode(204, Response<IEnumerable<CtaNotificationSettingsResponse>>.NoContent("No hay configuraciones disponibles."));

                    return Ok(Response<IEnumerable<CtaNotificationSettingsResponse>>.Success(
                        companyId != null ? settings.Where(x => x.CompanyId == companyId).ToList() : settings, "Configuraciones obtenidas correctamente."));
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
        [SwaggerOperation(Summary = "Agregar una nueva configuración de notificaciones", Description = "Endpoint para registrar una configuración de notificaciones")]
        public async Task<IActionResult> CreateNotificationSetting([FromBody] CtaNotificationSettingsRequest notificationSettingDto)
        {
            try
            {
                var validationResult = await _validator.ValidateAsync(notificationSettingDto);

                if (!validationResult.IsValid)
                {
                    var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                    return BadRequest(Response<string>.BadRequest(errors, 400));
                }

                var response = await _notificationSettingsService.Add(notificationSettingDto);
                return CreatedAtAction(nameof(GetAllNotificationSettings), Response<CtaNotificationSettingsResponse>.Created(response));
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
        [SwaggerOperation(Summary = "Actualizar una configuración de notificaciones", Description = "Endpoint para actualizar una configuración de notificaciones")]
        public async Task<IActionResult> UpdateNotificationSetting(long id, [FromBody] CtaNotificationSettingsRequest notificationSettingDto)
        {
            try
            {
                var validationResult = await _validator.ValidateAsync(notificationSettingDto);

                if (!validationResult.IsValid)
                {
                    var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                    return BadRequest(Response<string>.BadRequest(errors, 400));
                }

                var existingSetting = await _notificationSettingsService.GetByIdRequest(id);
                if (existingSetting == null)
                    return NotFound(Response<string>.NotFound("Configuración no encontrada."));

                notificationSettingDto.Id = id;
                await _notificationSettingsService.Update(notificationSettingDto, id);
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
        [SwaggerOperation(Summary = "Eliminar una configuración de notificaciones", Description = "Endpoint para eliminar una configuración de notificaciones")]
        public async Task<IActionResult> DeleteNotificationSetting(long id)
        {
            try
            {
                var existingSetting = await _notificationSettingsService.GetByIdRequest(id);
                if (existingSetting == null)
                    return NotFound(Response<string>.NotFound("Configuración no encontrada."));

                await _notificationSettingsService.Delete(id);
                return Ok(Response<string>.Success(null, "Configuración eliminada correctamente."));
            }
            catch (Exception ex)
            {
                return StatusCode(500, Response<string>.ServerError(ex.Message));
            }
        }
    }
}