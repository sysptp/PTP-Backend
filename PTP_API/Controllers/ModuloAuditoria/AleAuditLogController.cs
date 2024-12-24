using BussinessLayer.DTOs.ModuloAuditoria;
using BussinessLayer.Interfaces.Services.ModuloAuditoria;
using BussinessLayer.Wrappers;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net.Mime;

namespace PTP_API.Controllers.ModuloAuditoria
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [SwaggerTag("Gestión de Logs de Auditoría")]
    [Authorize]
    public class AleAuditLogController : ControllerBase
    {
        private readonly IAleAuditLogService _aleAuditLogsService;
        private readonly IValidator<AleAuditLogRequest> _validator;

        public AleAuditLogController(IAleAuditLogService aleAuditLogsService, IValidator<AleAuditLogRequest> validator)
        {
            _aleAuditLogsService = aleAuditLogsService;
            _validator = validator;
        }
      
        [HttpGet]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(Response<IEnumerable<AleAuditLogResponse>>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Obtener logs de auditoría", Description = "Obtiene una lista de logs de auditoría del sistema o logs específicos si se proporcionan filtros.")]
        public async Task<IActionResult> GetByFilters(
            [FromQuery] long? idEmpresa,
            [FromQuery] string? tableName,
            [FromQuery] string? action,
            [FromQuery] string? oldValue,
            [FromQuery] string? newValue,
            [FromQuery] string? auditDate
            )
        {
            try
            {
                var logs = await _aleAuditLogsService.GetAllByFilter(tableName, action, oldValue, newValue, auditDate, idEmpresa);

                if (logs == null || !logs.Any())
                {
                    return NoContent();
                }

                return Ok(Response<IEnumerable<AleAuditLogResponse>>.Success(logs, "Logs de auditoría obtenidos correctamente."));
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
        [SwaggerOperation(Summary = "Registrar un nuevo log de auditoría", Description = "Crea un nuevo registro en los logs de auditoría del sistema.")]
        public async Task<IActionResult> Add([FromBody] AleAuditLogRequest request)
        {
            var validationResult = await _validator.ValidateAsync(request);

            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return BadRequest(Response<string>.BadRequest(errors, 400));
            }

            try
            {
                var log = await _aleAuditLogsService.Add(request);
                return StatusCode(201, Response<AleAuditLogResponse>.Created(log, "Log de auditoría creado correctamente."));
            }
            catch (Exception ex)
            {
                return StatusCode(500, Response<string>.ServerError(ex.Message));
            }
        }
    }
}
