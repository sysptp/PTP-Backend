using System.Net.Mime;
using BussinessLayer.Atributes;
using BussinessLayer.DTOs.ModuloCitas.CtaEmailTemplates;
using BussinessLayer.Interfaces.Services.ModuloCitas;
using BussinessLayer.Wrappers;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace PTP_API.Controllers.ModuloCita
{
    [ApiController]
    [SwaggerTag("Gestión de Plantillas de Correo")]
    [Route("api/v1/[controller]")]
    [Authorize]
    [EnableBitacora]
    public class CtaEmailTemplatesController : ControllerBase
    {
        private readonly ICtaEmailTemplatesService _emailTemplatesService;
        private readonly IValidator<CtaEmailTemplatesRequest> _validator;

        public CtaEmailTemplatesController(ICtaEmailTemplatesService emailTemplatesService, IValidator<CtaEmailTemplatesRequest> validator)
        {
            _emailTemplatesService = emailTemplatesService;
            _validator = validator;
        }

        [HttpGet]
        [ProducesResponseType(typeof(Response<IEnumerable<CtaEmailTemplatesResponse>>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Obtener Plantillas de Correo", Description = "Devuelve una lista de plantillas o una plantilla específica si se proporciona un ID")]
        [DisableBitacora]
        public async Task<IActionResult> GetAllEmailTemplates([FromQuery] long? id, long? companyId)
        {
            try
            {
                if (id.HasValue)
                {
                    var template = await _emailTemplatesService.GetByIdResponse(id.Value);
                    if (template == null)
                        return NotFound(Response<CtaEmailTemplatesResponse>.NotFound("Plantilla no encontrada."));

                    return Ok(Response<CtaEmailTemplatesResponse>.Success(template, "Plantilla encontrada."));
                }
                else
                {
                    var templates = await _emailTemplatesService.GetAllDto();
                    if (templates == null || !templates.Any())
                        return StatusCode(204, Response<IEnumerable<CtaEmailTemplatesResponse>>.NoContent("No hay plantillas disponibles."));

                    return Ok(Response<IEnumerable<CtaEmailTemplatesResponse>>.Success(
                        companyId != null ? templates.Where(x => x.CompanyId == companyId).ToList() : templates, "Plantillas obtenidas correctamente."));
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
        [SwaggerOperation(Summary = "Agregar una nueva plantilla de correo", Description = "Endpoint para registrar una plantilla de correo")]
        public async Task<IActionResult> CreateEmailTemplate([FromBody] CtaEmailTemplatesRequest emailTemplateDto)
        {
            try
            {
                var validationResult = await _validator.ValidateAsync(emailTemplateDto);

                if (!validationResult.IsValid)
                {
                    var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                    return BadRequest(Response<string>.BadRequest(errors, 400));
                }

                var response = await _emailTemplatesService.Add(emailTemplateDto);
                return CreatedAtAction(nameof(GetAllEmailTemplates), Response<CtaEmailTemplatesResponse>.Created(response));
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
        [SwaggerOperation(Summary = "Actualizar una plantilla de correo", Description = "Endpoint para actualizar una plantilla de correo")]
        public async Task<IActionResult> UpdateEmailTemplate(long id, [FromBody] CtaEmailTemplatesRequest emailTemplateDto)
        {
            try
            {
                var validationResult = await _validator.ValidateAsync(emailTemplateDto);

                if (!validationResult.IsValid)
                {
                    var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                    return BadRequest(Response<string>.BadRequest(errors, 400));
                }

                var existingTemplate = await _emailTemplatesService.GetByIdRequest(id);
                if (existingTemplate == null)
                    return NotFound(Response<string>.NotFound("Plantilla no encontrada."));

                emailTemplateDto.Id = id;
                await _emailTemplatesService.Update(emailTemplateDto, id);
                return Ok(Response<string>.Success(null, "Plantilla actualizada correctamente."));
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
        [SwaggerOperation(Summary = "Eliminar una plantilla de correo", Description = "Endpoint para eliminar una plantilla de correo")]
        public async Task<IActionResult> DeleteEmailTemplate(long id)
        {
            try
            {
                var existingTemplate = await _emailTemplatesService.GetByIdRequest(id);
                if (existingTemplate == null)
                    return NotFound(Response<string>.NotFound("Plantilla no encontrada."));

                await _emailTemplatesService.Delete(id);
                return Ok(Response<string>.Success(null, "Plantilla eliminada correctamente."));
            }
            catch (Exception ex)
            {
                return StatusCode(500, Response<string>.ServerError(ex.Message));
            }
        }
    }
}