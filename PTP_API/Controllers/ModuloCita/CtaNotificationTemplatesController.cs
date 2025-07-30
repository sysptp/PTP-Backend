using System.Net.Mime;
using BussinessLayer.Atributes;
using BussinessLayer.DTOs.ModuloCitas.CtaNotificationTemplates;
using BussinessLayer.Interfaces.Services.ModuloCitas;
using BussinessLayer.Wrappers;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace PTP_API.Controllers.ModuloCita
{
    [ApiController]
    [SwaggerTag("Gestión de CtaNotificationTemplates")]
    [Route("api/v1/[controller]")]
    [Authorize]
    [EnableBitacora]
    public class CtaNotificationTemplatesController : ControllerBase
    {
        private readonly ICtaNotificationTemplatesService _ctanotificationtemplatesService;
        private readonly IValidator<CtaNotificationTemplatesRequest> _validator;

        public CtaNotificationTemplatesController(ICtaNotificationTemplatesService ctanotificationtemplatesService, IValidator<CtaNotificationTemplatesRequest> validator)
        {
            _ctanotificationtemplatesService = ctanotificationtemplatesService;
            _validator = validator;
        }

        [HttpGet]
        [ProducesResponseType(typeof(Response<IEnumerable<CtaNotificationTemplatesResponse>>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Obtener CtaNotificationTemplates", Description = "Devuelve una lista de CtaNotificationTemplates o un elemento específico si se proporciona un ID")]
        [DisableBitacora]
        public async Task<IActionResult> GetAll([FromQuery] long? id, long? companyId)
        {
            try
            {
                if (id.HasValue)
                {
                    var item = await _ctanotificationtemplatesService.GetByIdResponse(id.Value);
                    if (item == null)
                        return NotFound(Response<CtaNotificationTemplatesResponse>.NotFound("CtaNotificationTemplates no encontrado."));

                    return Ok(Response<CtaNotificationTemplatesResponse>.Success(item, "CtaNotificationTemplates encontrado."));
                }
                else
                {
                    var items = await _ctanotificationtemplatesService.GetAllDto();
                    if (items == null || !items.Any())
                        return StatusCode(204, Response<IEnumerable<CtaNotificationTemplatesResponse>>.NoContent("No hay CtaNotificationTemplates disponibles."));

                    return Ok(Response<IEnumerable<CtaNotificationTemplatesResponse>>.Success(
                        companyId != null ? items.Where(x => x.CompanyId == companyId).ToList() : items, "CtaNotificationTemplates obtenidos correctamente."));
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
        [SwaggerOperation(Summary = "Crear CtaNotificationTemplates", Description = "Endpoint para registrar un nuevo CtaNotificationTemplates")]
        public async Task<IActionResult> Create([FromBody] CtaNotificationTemplatesRequest request)
        {
            try
            {
                var validationResult = await _validator.ValidateAsync(request);

                if (!validationResult.IsValid)
                {
                    var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                    return BadRequest(Response<string>.BadRequest(errors, 400));
                }

                var response = await _ctanotificationtemplatesService.Add(request);
                return CreatedAtAction(nameof(GetAll), Response<CtaNotificationTemplatesResponse>.Created(response));
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
        [SwaggerOperation(Summary = "Actualizar CtaNotificationTemplates", Description = "Endpoint para actualizar un CtaNotificationTemplates")]
        public async Task<IActionResult> Update(long id, [FromBody] CtaNotificationTemplatesRequest request)
        {
            try
            {
                var validationResult = await _validator.ValidateAsync(request);

                if (!validationResult.IsValid)
                {
                    var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                    return BadRequest(Response<string>.BadRequest(errors, 400));
                }

                var existingItem = await _ctanotificationtemplatesService.GetByIdRequest(id);
                if (existingItem == null)
                    return NotFound(Response<string>.NotFound("CtaNotificationTemplates no encontrado."));

                request.NotificationTemplateId = id;
                await _ctanotificationtemplatesService.Update(request, id);
                return Ok(Response<string>.Success(null, "CtaNotificationTemplates actualizado correctamente."));
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
        [SwaggerOperation(Summary = "Eliminar CtaNotificationTemplates", Description = "Endpoint para eliminar un CtaNotificationTemplates")]
        public async Task<IActionResult> Delete(long id)
        {
            try
            {
                var existingItem = await _ctanotificationtemplatesService.GetByIdRequest(id);
                if (existingItem == null)
                    return NotFound(Response<string>.NotFound("CtaNotificationTemplates no encontrado."));

                await _ctanotificationtemplatesService.Delete(id);
                return Ok(Response<string>.Success(null, "CtaNotificationTemplates eliminado correctamente."));
            }
            catch (Exception ex)
            {
                return StatusCode(500, Response<string>.ServerError(ex.Message));
            }
        }
    }
}
