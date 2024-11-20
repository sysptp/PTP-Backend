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
    [SwaggerTag("Gestión de Logs")]
    [Authorize]
    public class LogsController : ControllerBase
    {
        private readonly IAleLogsService _AleLogsService;
        private readonly IValidator<AleLogsRequest> _validator;

        public LogsController(IAleLogsService AleLogsService, IValidator<AleLogsRequest> validator)
        {
            _AleLogsService = AleLogsService;
            _validator = validator;
        }
        [HttpGet]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(Response<IEnumerable<AleLogsReponse>>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Obtener Auditoria del Login", Description = "Obtiene una lista de todas las Auditoria del Login o una Auditoria del Login específica si se proporciona un ID.")]
        public async Task<IActionResult> Get([FromQuery] int? id)
        {
            try
            {
                if (id.HasValue)
                {
                    var AleLogs = await _AleLogsService.GetByIdResponse(id);
                    if (AleLogs == null)
                    {
                        return NotFound(Response<AleLogsReponse>.NotFound("Auditoria del Login no encontrada."));
                    }
                    return Ok(Response<List<AleLogsReponse>>.Success(new List<AleLogsReponse> { AleLogs }, "Auditoria del Login encontrada."));
                }
                else
                {
                    var AleLogss = await _AleLogsService.GetAllDto();
                    if (AleLogss == null || AleLogss.Count == 0)
                    {
                        return NoContent();
                    }
                    return Ok(Response<IEnumerable<AleLogsReponse>>.Success(AleLogss, "Auditoria del Login obtenidas correctamente."));
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, Response<string>.ServerError("Ocurrió un error al obtener las Auditoria del Logins. Por favor, intente nuevamente."));
            }
        }

        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Crear una nueva Auditoria del Login", Description = "Crea una nueva Auditoria del Login en el sistema.")]
        public async Task<IActionResult> Add([FromBody] AleLogsRequest request)
        {
            var validationResult = await _validator.ValidateAsync(request);

            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return BadRequest(Response<string>.BadRequest(errors, 400));
            }

            try
            {
                var AleLogs = await _AleLogsService.Add(request);
                return StatusCode(201, Response<AleLogsReponse>.Created(AleLogs, "Auditoria del Login creada correctamente."));
            }
            catch (Exception ex)
            {
                return StatusCode(500, Response<string>.ServerError("Ocurrió un error al crear la Auditoria del Login. Por favor, intente nuevamente."));
            }
        }

        [HttpPut("{id}")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Actualizar una Auditoria del Login", Description = "Actualiza la información de una Auditoria del Login existente.")]
        public async Task<IActionResult> Update(int id, [FromBody] AleLogsRequest saveDto)
        {
            var validationResult = await _validator.ValidateAsync(saveDto);

            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return BadRequest(Response<string>.BadRequest(errors, 400));
            }

            try
            {
                var existingEmpresa = await _AleLogsService.GetByIdResponse(id);
                if (existingEmpresa == null)
                {
                    return NotFound(Response<AleLogsReponse>.NotFound("Auditoria del Login no encontrada."));
                }
                saveDto.IdLogs = id;
                await _AleLogsService.Update(saveDto, id);
                return Ok(Response<string>.Success(null, "Auditoria del Login actualizada correctamente"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, Response<string>.ServerError("Ocurrió un error al actualizar la Auditoria del Login. Por favor, intente nuevamente."));
            }
        }
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Eliminar una Auditoria del Login", Description = "Elimina una Auditoria del Login de manera lógica.")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var AleLogs = await _AleLogsService.GetByIdResponse(id);
                if (AleLogs == null)
                {
                    return NotFound(Response<string>.NotFound("Auditoria del Login no encontrada."));
                }

                await _AleLogsService.Delete(id);
                return Ok(Response<string>.Success(null, "Auditoria del Login eliminada correctamente"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, Response<string>.ServerError("Ocurrió un error al eliminar la Auditoria del Login. Por favor, intente nuevamente."));
            }
        }
    }
}
