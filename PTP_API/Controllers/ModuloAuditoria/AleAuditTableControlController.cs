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
    [SwaggerTag("Gestión de Auditoria")]
    [Authorize]
    public class AleAuditTableControlController : ControllerBase
    {

        private readonly IAleAuditTableControlService _aleAuditTableControlService;
        private readonly IValidator<AleAuditTableControlRequest> _validator;

        public AleAuditTableControlController(IAleAuditTableControlService aleAuditTableControlService,
            IValidator<AleAuditTableControlRequest> validator)
        {
            _aleAuditTableControlService = aleAuditTableControlService;
            _validator = validator;
        }

        [HttpGet]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(Response<IEnumerable<AleAuditTableControlResponse>>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Obtener control de tablas de auditoría", Description = "Obtiene una lista del control de tablas de auditoría del sistema o una tabla específica si se proporciona un ID.")]
        public async Task<IActionResult> Get([FromQuery] long? id)
        {
            try
            {
                if (id.HasValue)
                {
                    var tableControl = await _aleAuditTableControlService.GetByIdResponse(id);
                    if (tableControl == null)
                    {
                        return NotFound(Response<AleAuditTableControlResponse>.NotFound("Control de tabla no encontrado."));
                    }
                    return Ok(Response<List<AleAuditTableControlResponse>>.Success(new List<AleAuditTableControlResponse> { tableControl }, "Control de tabla encontrado."));
                }
                else
                {
                    var tableControls = await _aleAuditTableControlService.GetAllDto();
                    if (tableControls == null || !tableControls.Any())
                    {
                        return NoContent();
                    }
                    return Ok(Response<IEnumerable<AleAuditTableControlResponse>>.Success(tableControls, "Control de tablas obtenido correctamente."));
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, Response<string>.ServerError("Ocurrió un error al obtener el control de tablas. Por favor, intente nuevamente."));
            }
        }

        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Registrar un nuevo control de tabla de auditoría", Description = "Crea un nuevo registro en el control de tablas de auditoría del sistema.")]
        public async Task<IActionResult> Add([FromBody] AleAuditTableControlRequest request)
        {
            var validationResult = await _validator.ValidateAsync(request);

            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return BadRequest(Response<string>.BadRequest(errors, 400));
            }

            try
            {
                var tableControl = await _aleAuditTableControlService.Add(request);
                return StatusCode(201, Response<AleAuditTableControlResponse>.Created(tableControl, "Control de tabla creado correctamente."));
            }
            catch (Exception ex)
            {
                return StatusCode(500, Response<string>.ServerError($"{ex.Message} el control de tabla. Por favor, intente nuevamente."));
            }
        }

    }
}
