using System.Net.Mime;
using BussinessLayer.DTOs.ModuloCitas.CtaWhatsAppTemplates;
using BussinessLayer.Interfaces.Services.ModuloCitas;
using BussinessLayer.Wrappers;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace PTP_API.Controllers.ModuloCita
{
    [ApiController]
    [SwaggerTag("Gestión de CtaWhatsAppTemplates")]
    [Route("api/v1/[controller]")]
    [Authorize]
    public class CtaWhatsAppTemplatesController : ControllerBase
    {
        private readonly ICtaWhatsAppTemplatesService _ctawhatsapptemplatesService;
        private readonly IValidator<CtaWhatsAppTemplatesRequest> _validator;

        public CtaWhatsAppTemplatesController(ICtaWhatsAppTemplatesService ctawhatsapptemplatesService, IValidator<CtaWhatsAppTemplatesRequest> validator)
        {
            _ctawhatsapptemplatesService = ctawhatsapptemplatesService;
            _validator = validator;
        }

        [HttpGet]
        [ProducesResponseType(typeof(Response<IEnumerable<CtaWhatsAppTemplatesResponse>>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Obtener CtaWhatsAppTemplates", Description = "Devuelve una lista de CtaWhatsAppTemplates o un elemento específico si se proporciona un ID")]
        public async Task<IActionResult> GetAll([FromQuery] long? id, long? companyId)
        {
            try
            {
                if (id.HasValue)
                {
                    var item = await _ctawhatsapptemplatesService.GetByIdResponse(id.Value);
                    if (item == null)
                        return NotFound(Response<CtaWhatsAppTemplatesResponse>.NotFound("CtaWhatsAppTemplates no encontrado."));

                    return Ok(Response<CtaWhatsAppTemplatesResponse>.Success(item, "CtaWhatsAppTemplates encontrado."));
                }
                else
                {
                    var items = await _ctawhatsapptemplatesService.GetAllDto();
                    if (items == null || !items.Any())
                        return StatusCode(204, Response<IEnumerable<CtaWhatsAppTemplatesResponse>>.NoContent("No hay CtaWhatsAppTemplates disponibles."));

                    return Ok(Response<IEnumerable<CtaWhatsAppTemplatesResponse>>.Success(
                        companyId != null ? items.Where(x => x.CompanyId == companyId).ToList() : items, "CtaWhatsAppTemplates obtenidos correctamente."));
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
        [SwaggerOperation(Summary = "Crear CtaWhatsAppTemplates", Description = "Endpoint para registrar un nuevo CtaWhatsAppTemplates")]
        public async Task<IActionResult> Create([FromBody] CtaWhatsAppTemplatesRequest request)
        {
            try
            {
                var validationResult = await _validator.ValidateAsync(request);

                if (!validationResult.IsValid)
                {
                    var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                    return BadRequest(Response<string>.BadRequest(errors, 400));
                }

                var response = await _ctawhatsapptemplatesService.Add(request);
                return CreatedAtAction(nameof(GetAll), Response<CtaWhatsAppTemplatesResponse>.Created(response));
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
        [SwaggerOperation(Summary = "Actualizar CtaWhatsAppTemplates", Description = "Endpoint para actualizar un CtaWhatsAppTemplates")]
        public async Task<IActionResult> Update(long id, [FromBody] CtaWhatsAppTemplatesRequest request)
        {
            try
            {
                var validationResult = await _validator.ValidateAsync(request);

                if (!validationResult.IsValid)
                {
                    var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                    return BadRequest(Response<string>.BadRequest(errors, 400));
                }

                var existingItem = await _ctawhatsapptemplatesService.GetByIdRequest(id);
                if (existingItem == null)
                    return NotFound(Response<string>.NotFound("CtaWhatsAppTemplates no encontrado."));

                request.Id = id;
                await _ctawhatsapptemplatesService.Update(request, id);
                return Ok(Response<string>.Success(null, "CtaWhatsAppTemplates actualizado correctamente."));
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
        [SwaggerOperation(Summary = "Eliminar CtaWhatsAppTemplates", Description = "Endpoint para eliminar un CtaWhatsAppTemplates")]
        public async Task<IActionResult> Delete(long id)
        {
            try
            {
                var existingItem = await _ctawhatsapptemplatesService.GetByIdRequest(id);
                if (existingItem == null)
                    return NotFound(Response<string>.NotFound("CtaWhatsAppTemplates no encontrado."));

                await _ctawhatsapptemplatesService.Delete(id);
                return Ok(Response<string>.Success(null, "CtaWhatsAppTemplates eliminado correctamente."));
            }
            catch (Exception ex)
            {
                return StatusCode(500, Response<string>.ServerError(ex.Message));
            }
        }
    }
}
