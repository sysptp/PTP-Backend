using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using Swashbuckle.AspNetCore.Annotations;
using BussinessLayer.Interfaces.IAuditoria;
using BussinessLayer.Wrappers;
using Microsoft.AspNetCore.Authorization;
using FluentValidation;
using BussinessLayer.DTOs.Auditoria;

namespace PTP_API.Controllers.Auditoria
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [SwaggerTag("Gestión de Auditoria")]
    [Authorize]
    public class AuditoriaController : ControllerBase
    {
        private readonly IAleAuditoriaService _aleAuditoriaService;
        private readonly IValidator<AleAuditoriaRequest> _validator;

        public AuditoriaController(IAleAuditoriaService aleAuditoriaService, IValidator<AleAuditoriaRequest> validator)
        {
            _aleAuditoriaService = aleAuditoriaService;
            _validator = validator;
        }
        [HttpGet]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(Response<IEnumerable<AleAuditoriaReponse>>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Obtener Auditoria Sistema", Description = "Obtiene una lista de todas las Auditoria Sistema o una categoria específica si se proporciona un ID.")]
        public async Task<IActionResult> Get([FromQuery] int? id)
        {
            try
            {
                if (id.HasValue)
                {
                    var aleAuditoria = await _aleAuditoriaService.GetByIdResponse(id);
                    if (aleAuditoria == null)
                    {
                        return NotFound(Response<AleAuditoriaReponse>.NotFound("Categoria no encontrada."));
                    }
                    return Ok(Response<List<AleAuditoriaReponse>>.Success(new List<AleAuditoriaReponse> { aleAuditoria }, "Auditoria Sistema encontrada."));
                }
                else
                {
                    var aleAuditorias = await _aleAuditoriaService.GetAllDto();
                    if (aleAuditorias == null || aleAuditorias.Count == 0)
                    {
                        return NoContent();
                    }
                    return Ok(Response<IEnumerable<AleAuditoriaReponse>>.Success(aleAuditorias, "Auditoria Sistema obtenidas correctamente."));
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, Response<string>.ServerError("Ocurrió un error al obtener las categorias de tickets. Por favor, intente nuevamente."));
            }
        }

        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Crear una nueva Auditoria Sistema", Description = "Crea una nueva Auditoria Sistema en el sistema.")]
        public async Task<IActionResult> Add([FromBody] AleAuditoriaRequest request)
        {
            var validationResult = await _validator.ValidateAsync(request);

            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return BadRequest(Response<string>.BadRequest(errors, 400));
            }

            try
            {
                var aleAuditoria = await _aleAuditoriaService.Add(request);
                return StatusCode(201, Response<AleAuditoriaReponse>.Created(aleAuditoria, "Auditoria Sistema creada correctamente."));
            }
            catch (Exception ex)
            {
                return StatusCode(500, Response<string>.ServerError("Ocurrió un error al crear la Auditoria Sistema. Por favor, intente nuevamente."));
            }
        }

        [HttpPut("{id}")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Actualizar una Categoria Ticket", Description = "Actualiza la información de una Auditoria Sistema existente.")]
        public async Task<IActionResult> Update(int id, [FromBody] AleAuditoriaRequest saveDto)
        {
            var validationResult = await _validator.ValidateAsync(saveDto);

            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return BadRequest(Response<string>.BadRequest(errors, 400));
            }

            try
            {
                var existingEmpresa = await _aleAuditoriaService.GetByIdResponse(id);
                if (existingEmpresa == null)
                {
                    return NotFound(Response<AleAuditoriaReponse>.NotFound("Auditoria Sistema no encontrada."));
                }
                saveDto.IdAuditoria = id;
                await _aleAuditoriaService.Update(saveDto, id);
                return Ok(Response<string>.Success(null, "Categoria actualizada correctamente"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, Response<string>.ServerError("Ocurrió un error al actualizar la Categoria. Por favor, intente nuevamente."));
            }
        }
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Eliminar una Auditoria Sistema", Description = "Elimina una Categoria de manera lógica.")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var aleAuditoria = await _aleAuditoriaService.GetByIdResponse(id);
                if (aleAuditoria == null)
                {
                    return NotFound(Response<string>.NotFound("Auditoria Sistema no encontrada."));
                }

                await _aleAuditoriaService.Delete(id);
                return Ok(Response<string>.Success(null, "Auditoria Sistema eliminada correctamente"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, Response<string>.ServerError("Ocurrió un error al eliminar la Auditoria Sistema. Por favor, intente nuevamente."));
            }
        }
    }
}
