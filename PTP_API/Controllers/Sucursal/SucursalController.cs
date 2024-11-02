using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using Swashbuckle.AspNetCore.Annotations;
using BussinessLayer.DTOs.Sucursal;
using BussinessLayer.Wrappers;
using Microsoft.AspNetCore.Authorization;
using BussinessLayer.Interfaces.IEmpresa;

namespace PTP_API.Controllers.Sucursal
{
    [ApiController]
    [Route("api/v1/Sucursal")]
    [SwaggerTag("Gestión de Sucursales")]
    [Authorize]
    public class SucursalController : ControllerBase
    {
        private readonly IGnSucursalService _sucursalService;

        public SucursalController(IGnSucursalService sucursalService)
        {
            _sucursalService = sucursalService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [SwaggerOperation(Summary = "Obtener sucursales", Description = "Obtiene una lista de todas las sucursales o una sucursal específica si se proporciona un ID.")]
        public async Task<IActionResult> Get([FromQuery] long? id)
        {
            try
            {
                if (id.HasValue)
                {
                    var sucursal = await _sucursalService.GetBySucursalCode(id);
                    if (sucursal == null)
                    {
                        return NotFound(Response<GnSucursalResponse>.NotFound("Sucursal no encontrada."));
                    }
                    return Ok(Response<GnSucursalResponse>.Success(sucursal, "Sucursal encontrada."));
                }
                else
                {
                    var sucursales = await _sucursalService.GetAllDto();
                    if (sucursales == null || !sucursales.Any())
                    {
                        return StatusCode(204,Response<IEnumerable<GnSucursalResponse>>.NoContent("No hay sucursales disponibles."));
                    }
                    return Ok(Response<IEnumerable<GnSucursalResponse>>.Success(sucursales, "Sucursales obtenidas correctamente."));
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500,Response<string>.ServerError("Ocurrió un error al obtener las sucursales. Por favor, intente nuevamente."));
            }
        }

        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [SwaggerOperation(Summary = "Crear una nueva sucursal", Description = "Crea una nueva sucursal en el sistema.")]
        public async Task<IActionResult> Add([FromBody] GnSucursalRequest request)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                return BadRequest(Response<string>.BadRequest(errors, 400));
            }

            try
            {
                var sucursal = await _sucursalService.Add(request);
                return Ok(Response<GnSucursalResponse>.Created(sucursal, "Sucursal creada correctamente."));
            }
            catch (Exception ex)
            {
                return StatusCode(500,Response<string>.ServerError("Ocurrió un error al crear la sucursal. Por favor, intente nuevamente."));
            }
        }
    }
}
