using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using Swashbuckle.AspNetCore.Annotations;
using BussinessLayer.Wrappers;
using Microsoft.AspNetCore.Authorization;
using FluentValidation;
using BussinessLayer.DTOs.ModuloAuditoria;
using BussinessLayer.Interfaces.Services.ModuloAuditoria;

namespace PTP_API.Controllers.ModuloAuditoria
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [SwaggerTag("Gestión de Auditoria")]
    [Authorize]
    public class BitacoraController : ControllerBase
    {
        private readonly IAleBitacoraService _aleAuditoriaService;
        private readonly IValidator<AleBitacoraRequest> _validator;

        public BitacoraController(IAleBitacoraService aleAuditoriaService, IValidator<AleBitacoraRequest> validator)
        {
            _aleAuditoriaService = aleAuditoriaService;
            _validator = validator;
        }
        [HttpGet]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(Response<IEnumerable<AleBitacoraReponse>>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Obtener Auditoria Sistema", Description = "Obtiene una lista de todas las Auditoria Sistema o una categoria específica si se proporciona un ID.")]
        public async Task<IActionResult> Get([FromQuery] long? id)
        {
            try
            {
                if (id.HasValue)
                {
                    var aleAuditoria = await _aleAuditoriaService.GetByIdResponse(id);
                    if (aleAuditoria == null)
                    {
                        return NotFound(Response<AleBitacoraReponse>.NotFound("Auditoria no encontrada."));
                    }
                    return Ok(Response<List<AleBitacoraReponse>>.Success(new List<AleBitacoraReponse> { aleAuditoria }, "Auditoria Sistema encontrada."));
                }
                else
                {
                    var aleAuditorias = await _aleAuditoriaService.GetAllDto();
                    if (aleAuditorias == null || aleAuditorias.Count == 0)
                    {
                        return NoContent();
                    }
                    return Ok(Response<IEnumerable<AleBitacoraReponse>>.Success(aleAuditorias, "Auditoria Sistema obtenidas correctamente."));
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, Response<string>.ServerError("Ocurrió un error al obtener las Auditoria. Por favor, intente nuevamente."));
            }
        }

        [HttpGet("GetByFilters")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(Response<IEnumerable<AleBitacoraReponse>>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Obtener Auditoria Sistema con Filtros", Description = "Obtiene una lista de Auditoria Sistema según los filtros proporcionados.")]
        public async Task<IActionResult> GetByFilters(
    [FromQuery] string? modulo,
    [FromQuery] string? accion,
    [FromQuery] int? ano,
    [FromQuery] int? mes,
    [FromQuery] int? dia,
    [FromQuery] int? hora,
    [FromQuery] string? requestLike,
    [FromQuery] string? responseLike,
    [FromQuery] string? rolUsuario,
    [FromQuery] long? idEmpresa,
    [FromQuery] long? idSucursal)
        {
            try
            {
                var filteredAuditorias = await _aleAuditoriaService.GetAllByFilters(
                    modulo ?? string.Empty,
                    accion ?? string.Empty,
                    ano ?? 0,
                    mes ?? 0,
                    dia ?? 0,
                    hora ?? 0,
                    requestLike ?? string.Empty,
                    responseLike ?? string.Empty,
                    rolUsuario ?? string.Empty,
                    idEmpresa ?? 0,
                    idSucursal ?? 0);

                if (filteredAuditorias == null || !filteredAuditorias.Any())
                {
                    return NoContent();
                }

                return Ok(Response<IEnumerable<AleBitacoraReponse>>.Success(filteredAuditorias, "Auditorias filtradas obtenidas correctamente."));
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
        [SwaggerOperation(Summary = "Crear una nueva Auditoria Sistema", Description = "Crea una nueva Auditoria Sistema en el sistema.")]
        public async Task<IActionResult> Add([FromBody] AleBitacoraRequest request)
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
                return StatusCode(201, Response<AleBitacoraReponse>.Created(aleAuditoria, "Auditoria Sistema creada correctamente."));
            }
            catch (Exception ex)
            {
                return StatusCode(500, Response<string>.ServerError("Ocurrió un error al crear la Auditoria Sistema. Por favor, intente nuevamente."));
            }
        }

    }
}
