using System.Net.Mime;
using BussinessLayer.Atributes;
using BussinessLayer.DTOs.ModuloCitas.CtaMessageTemplates;
using BussinessLayer.Interfaces.Services.ModuloCitas;
using BussinessLayer.Wrappers;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace PTP_API.Controllers.ModuloCita
{
    [ApiController]
    [SwaggerTag("Gestión de CtaMessageTemplates")]
    [Route("api/v1/[controller]")]
    [Authorize]
    [EnableBitacora]
    public class CtaMessageTemplatesController : ControllerBase
    {
        private readonly ICtaMessageTemplatesService _ctamessagetemplatesService;
        private readonly IValidator<CtaMessageTemplatesRequest> _validator;

        public CtaMessageTemplatesController(ICtaMessageTemplatesService ctamessagetemplatesService, IValidator<CtaMessageTemplatesRequest> validator)
        {
            _ctamessagetemplatesService = ctamessagetemplatesService;
            _validator = validator;
        }

        [HttpGet]
        [ProducesResponseType(typeof(Response<IEnumerable<CtaMessageTemplatesResponse>>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Obtener CtaMessageTemplates", Description = "Devuelve una lista de CtaMessageTemplates o un elemento específico si se proporciona un ID")]
        [DisableBitacora]
        public async Task<IActionResult> GetAll([FromQuery] long? id, long? companyId)
        {
            try
            {
                if (id.HasValue)
                {
                    var item = await _ctamessagetemplatesService.GetByIdResponse(id.Value);
                    if (item == null)
                        return NotFound(Response<CtaMessageTemplatesResponse>.NotFound("CtaMessageTemplates no encontrado."));

                    return Ok(Response<CtaMessageTemplatesResponse>.Success(item, "CtaMessageTemplates encontrado."));
                }
                else
                {
                    var items = await _ctamessagetemplatesService.GetAllDto();
                    if (items == null || !items.Any())
                        return StatusCode(204, Response<IEnumerable<CtaMessageTemplatesResponse>>.NoContent("No hay CtaMessageTemplates disponibles."));

                    return Ok(Response<IEnumerable<CtaMessageTemplatesResponse>>.Success(
                        companyId != null ? items.Where(x => x.CompanyId == companyId).ToList() : items, "CtaMessageTemplates obtenidos correctamente."));
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
        [SwaggerOperation(Summary = "Crear CtaMessageTemplates", Description = "Endpoint para registrar un nuevo CtaMessageTemplates")]
        public async Task<IActionResult> Create([FromBody] CtaMessageTemplatesRequest request)
        {
            try
            {
                var validationResult = await _validator.ValidateAsync(request);

                if (!validationResult.IsValid)
                {
                    var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                    return BadRequest(Response<string>.BadRequest(errors, 400));
                }

                var response = await _ctamessagetemplatesService.Add(request);
                return CreatedAtAction(nameof(GetAll), Response<CtaMessageTemplatesResponse>.Created(response));
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
        [SwaggerOperation(Summary = "Actualizar CtaMessageTemplates", Description = "Endpoint para actualizar un CtaMessageTemplates")]
        public async Task<IActionResult> Update(long id, [FromBody] CtaMessageTemplatesRequest request)
        {
            try
            {
                var validationResult = await _validator.ValidateAsync(request);

                if (!validationResult.IsValid)
                {
                    var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                    return BadRequest(Response<string>.BadRequest(errors, 400));
                }

                var existingItem = await _ctamessagetemplatesService.GetByIdRequest(id);
                if (existingItem == null)
                    return NotFound(Response<string>.NotFound("CtaMessageTemplates no encontrado."));

                request.Id = id;
                await _ctamessagetemplatesService.Update(request, id);
                return Ok(Response<string>.Success(null, "CtaMessageTemplates actualizado correctamente."));
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
        [SwaggerOperation(Summary = "Eliminar CtaMessageTemplates", Description = "Endpoint para eliminar un CtaMessageTemplates")]
        public async Task<IActionResult> Delete(long id)
        {
            try
            {
                var existingItem = await _ctamessagetemplatesService.GetByIdRequest(id);
                if (existingItem == null)
                    return NotFound(Response<string>.NotFound("CtaMessageTemplates no encontrado."));

                await _ctamessagetemplatesService.Delete(id);
                return Ok(Response<string>.Success(null, "CtaMessageTemplates eliminado correctamente."));
            }
            catch (Exception ex)
            {
                return StatusCode(500, Response<string>.ServerError(ex.Message));
            }
        }
    }
}
