using BussinessLayer.DTOs.ModuloCitas.CtaNotificationSettings;
using Microsoft.AspNetCore.Authorization;
using BussinessLayer.Interfaces.Services.ModuloCitas;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using BussinessLayer.Wrappers;
using BussinessLayer.DTOs.ModuloCitas.CtaEmailTemplateTypes;

namespace PTP_API.Controllers.ModuloCita
{
    [ApiController]
    [SwaggerTag("Gestión de Configuraciones de Notificaciones")]
    [Route("api/v1/[controller]")]
    [Authorize]
    public class CtaEmailTemplateTypesController : ControllerBase
    {
        private readonly ICtaEmailTemplateTypesService _emailTemplateTypesService;

        public CtaEmailTemplateTypesController(ICtaEmailTemplateTypesService emailTemplateTypesService)
        {
            _emailTemplateTypesService = emailTemplateTypesService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(Response<IEnumerable<CtaEmailTemplateTypesResponse>>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Obtener Configuraciones de Notificaciones", Description = "Devuelve una lista de configuraciones o una configuración específica si se proporciona un ID")]
        public async Task<IActionResult> GetAllEmailTemplateTypes([FromQuery] long? id, long? companyId)
        {
            try
            {
                if (id.HasValue)
                {
                    var emailTemplateType = await _emailTemplateTypesService.GetByIdResponse(id.Value);
                    if (emailTemplateType == null)
                        return NotFound(Response<CtaEmailTemplateTypesResponse>.NotFound("Configuración no encontrada."));

                    return Ok(Response<CtaEmailTemplateTypesResponse>.Success(emailTemplateType, "Configuración encontrada."));
                }
                else
                {
                    var emailTemplateTypes = await _emailTemplateTypesService.GetAllDto();
                    if (emailTemplateTypes == null || !emailTemplateTypes.Any())
                        return StatusCode(204, Response<IEnumerable<CtaEmailTemplateTypesResponse>>.NoContent("No hay configuraciones disponibles."));

                    return Ok(Response<IEnumerable<CtaEmailTemplateTypesResponse>>.Success(emailTemplateTypes, "Configuraciones obtenidas correctamente."));
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, Response<string>.ServerError(ex.Message));
            }
        }
    }
}
