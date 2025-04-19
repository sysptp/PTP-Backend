using System.Net.Mime;
using BussinessLayer.DTOs.ModuloCitas.CtaSmsTemplates;
using BussinessLayer.Interfaces.Services.ModuloCitas;
using BussinessLayer.Wrappers;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace PTP_API.Controllers.ModuloCita
{
    [ApiController]
    [SwaggerTag("Gestión de CtaSmsTemplates")]
    [Route("api/v1/[controller]")]
    [Authorize]
    public class CtaSmsTemplatesController : ControllerBase
    {
        private readonly ICtaSmsTemplatesService _ctasmstemplatesService;
        private readonly IValidator<CtaSmsTemplatesRequest> _validator;

        public CtaSmsTemplatesController(ICtaSmsTemplatesService ctasmstemplatesService, IValidator<CtaSmsTemplatesRequest> validator)
        {
            _ctasmstemplatesService = ctasmstemplatesService;
            _validator = validator;
        }

        [HttpGet]
        [ProducesResponseType(typeof(Response<IEnumerable<CtaSmsTemplatesResponse>>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Obtener CtaSmsTemplates", Description = "Devuelve una lista de CtaSmsTemplates o un elemento específico si se proporciona un ID")]
        public async Task<IActionResult> GetAll([FromQuery] long? id, long? companyId)
        {
            try
            {
                if (id.HasValue)
                {
                    var item = await _ctasmstemplatesService.GetByIdResponse(id.Value);
                    if (item == null)
                        return NotFound(Response<CtaSmsTemplatesResponse>.NotFound("CtaSmsTemplates no encontrado."));

                    return Ok(Response<CtaSmsTemplatesResponse>.Success(item, "CtaSmsTemplates encontrado."));
                }
                else
                {
                    var items = await _ctasmstemplatesService.GetAllDto();
                    if (items == null || !items.Any())
                        return StatusCode(204, Response<IEnumerable<CtaSmsTemplatesResponse>>.NoContent("No hay CtaSmsTemplates disponibles."));

                    return Ok(Response<IEnumerable<CtaSmsTemplatesResponse>>.Success(
                        companyId != null ? items.Where(x => x.CompanyId == companyId).ToList() : items, "CtaSmsTemplates obtenidos correctamente."));
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
        [SwaggerOperation(Summary = "Crear CtaSmsTemplates", Description = "Endpoint para registrar un nuevo CtaSmsTemplates")]
        public async Task<IActionResult> Create([FromBody] CtaSmsTemplatesRequest request)
        {
            try
            {
                var validationResult = await _validator.ValidateAsync(request);

                if (!validationResult.IsValid)
                {
                    var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                    return BadRequest(Response<string>.BadRequest(errors, 400));
                }

                var response = await _ctasmstemplatesService.Add(request);
                return CreatedAtAction(nameof(GetAll), Response<CtaSmsTemplatesResponse>.Created(response));
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
        [SwaggerOperation(Summary = "Actualizar CtaSmsTemplates", Description = "Endpoint para actualizar un CtaSmsTemplates")]
        public async Task<IActionResult> Update(long id, [FromBody] CtaSmsTemplatesRequest request)
        {
            try
            {
                var validationResult = await _validator.ValidateAsync(request);

                if (!validationResult.IsValid)
                {
                    var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                    return BadRequest(Response<string>.BadRequest(errors, 400));
                }

                var existingItem = await _ctasmstemplatesService.GetByIdRequest(id);
                if (existingItem == null)
                    return NotFound(Response<string>.NotFound("CtaSmsTemplates no encontrado."));

                request.Id = id;
                await _ctasmstemplatesService.Update(request, id);
                return Ok(Response<string>.Success(null, "CtaSmsTemplates actualizado correctamente."));
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
        [SwaggerOperation(Summary = "Eliminar CtaSmsTemplates", Description = "Endpoint para eliminar un CtaSmsTemplates")]
        public async Task<IActionResult> Delete(long id)
        {
            try
            {
                var existingItem = await _ctasmstemplatesService.GetByIdRequest(id);
                if (existingItem == null)
                    return NotFound(Response<string>.NotFound("CtaSmsTemplates no encontrado."));

                await _ctasmstemplatesService.Delete(id);
                return Ok(Response<string>.Success(null, "CtaSmsTemplates eliminado correctamente."));
            }
            catch (Exception ex)
            {
                return StatusCode(500, Response<string>.ServerError(ex.Message));
            }
        }
    }
}
