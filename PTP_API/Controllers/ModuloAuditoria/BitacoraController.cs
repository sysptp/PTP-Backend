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
    [SwaggerTag("Gestión de Bitacora")]
    [Authorize]
    public class BitacoraController : ControllerBase
    {
        private readonly IAleBitacoraService _aleBitacoraService;
        private readonly IValidator<AleBitacoraRequest> _validator;

        public BitacoraController(IAleBitacoraService aleBitacoraService, IValidator<AleBitacoraRequest> validator)
        {
            _aleBitacoraService = aleBitacoraService;
            _validator = validator;
        }
        [HttpGet]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(Response<IEnumerable<AleBitacoraReponse>>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Obtener Bitacora Sistema", Description = "Obtiene una lista de todas las Bitacora Sistema o una categoria específica si se proporciona un ID.")]
        public async Task<IActionResult> Get([FromQuery] long? id)
        {
            try
            {
                if (id.HasValue)
                {
                    var aleBitacora = await _aleBitacoraService.GetByIdResponse(id);
                    if (aleBitacora == null)
                    {
                        return NotFound(Response<AleBitacoraReponse>.NotFound("Bitacora no encontrada."));
                    }
                    return Ok(Response<List<AleBitacoraReponse>>.Success(new List<AleBitacoraReponse> { aleBitacora }, "Bitacora Sistema encontrada."));
                }
                else
                {
                    var aleBitacoras = await _aleBitacoraService.GetAllDto();
                    if (aleBitacoras == null || aleBitacoras.Count == 0)
                    {
                        return NoContent();
                    }
                    return Ok(Response<IEnumerable<AleBitacoraReponse>>.Success(aleBitacoras, "Bitacora Sistema obtenidas correctamente."));
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, Response<string>.ServerError("Ocurrió un error al obtener las Bitacora. Por favor, intente nuevamente."));
            }
        }

        [HttpGet("GetByFilters")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(Response<IEnumerable<AleBitacoraReponse>>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Obtener Bitacora Sistema con Filtros", Description = "Obtiene una lista de Bitacora Sistema según los filtros proporcionados.")]
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
                var filteredBitacoras = await _aleBitacoraService.GetAllByFilters(
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

                if (filteredBitacoras == null || !filteredBitacoras.Any())
                {
                    return NoContent();
                }

                return Ok(Response<IEnumerable<AleBitacoraReponse>>.Success(filteredBitacoras, "Bitacoras filtradas obtenidas correctamente."));
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
        [SwaggerOperation(Summary = "Crear una nueva Bitacora Sistema", Description = "Crea una nueva Bitacora Sistema en el sistema.")]
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
                var bitacora = await _aleBitacoraService.Add(request);
                return StatusCode(201, Response<AleBitacoraReponse>.Created(bitacora, "Bitacora Sistema creada correctamente."));
            }
            catch (Exception ex)
            {
                return StatusCode(500, Response<string>.ServerError("ex.Message la Bitacora Sistema. Por favor, intente nuevamente."));
            }
        }

    }
}
