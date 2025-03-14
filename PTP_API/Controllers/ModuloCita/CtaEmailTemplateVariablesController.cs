using BussinessLayer.DTOs.ModuloCitas.CtaEmailTemplateVariables;
using BussinessLayer.Interfaces.Services.ModuloCitas;
using BussinessLayer.Wrappers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace PTP_API.Controllers.ModuloCita
{
    [ApiController]
    [SwaggerTag("Gestión de Variables de Plantilla de Correo")]
    [Route("api/v1/[controller]")]
    [Authorize]
    public class CtaEmailTemplateVariablesController : ControllerBase
    {
        private readonly ICtaEmailTemplateVariablesService _service;

        public CtaEmailTemplateVariablesController(ICtaEmailTemplateVariablesService service)
        {
            _service = service;
        }

        [HttpGet]
        [ProducesResponseType(typeof(Response<IEnumerable<CtaEmailTemplateVariablesResponse>>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "ObtenerVariables de Plantillas de Correos", Description = "Devuelve una lista de variables o una variable de Plantillas de Correos  específica si se proporciona un ID")]
        public async Task<IActionResult> GetAllEmailTemplateVariables([FromQuery] long? id)
        {
            try
            {
                if (id.HasValue)
                {
                    var emailTemplateType = await _service.GetByIdResponse(id.Value);
                    if (emailTemplateType == null)
                        return NotFound(Response<CtaEmailTemplateVariablesResponse>.NotFound("Variable de plantilla de Correos no encontrada."));

                    return Ok(Response<CtaEmailTemplateVariablesResponse>.Success(emailTemplateType, "Variable de plantilla de Correos  encontrada."));
                }
                else
                {
                    var emailTemplateTypes = await _service.GetAllDto();
                    if (emailTemplateTypes == null || !emailTemplateTypes.Any())
                        return StatusCode(204, Response<IEnumerable<CtaEmailTemplateVariablesResponse>>.NoContent("No hay Variable de plantilla de Correos disponibles."));

                    return Ok(Response<IEnumerable<CtaEmailTemplateVariablesResponse>>.Success(emailTemplateTypes, "Variable de plantilla de Correos obtenidas correctamente."));
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, Response<string>.ServerError(ex.Message));
            }
        }
    }
}
