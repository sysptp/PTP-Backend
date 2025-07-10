using BussinessLayer.Atributes;
using BussinessLayer.DTOs.ModuloCitas.BookingPortal;
using BussinessLayer.Interfaces.Services.ModuloCitas;
using BussinessLayer.Wrappers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net.Mime;

namespace PTP_API.Controllers.ModuloCita
{
    [ApiController]
    [SwaggerTag("Gestión de Áreas del Portal de Reservas")]
    [Route("api/v1/[controller]")]
    [Authorize]
    [EnableBitacora]
    public class CtaBookingPortalAreasController : ControllerBase
    {
        private readonly ICtaBookingPortalAreasService _portalAreasService;

        public CtaBookingPortalAreasController(ICtaBookingPortalAreasService portalAreasService)
        {
            _portalAreasService = portalAreasService;
        }

        [HttpGet("portal/{portalId}")]
        [ProducesResponseType(typeof(Response<List<BookingPortalAreaResponse>>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Obtener áreas por portal", Description = "Obtiene todas las áreas asignadas a un portal específico")]
        [DisableBitacora]
        public async Task<IActionResult> GetAreasByPortal(int portalId)
        {
            try
            {
                var areas = await _portalAreasService.GetAreasByPortalIdAsync(portalId);
                if (!areas.Any())
                    return StatusCode(204, Response<string>.NoContent("No hay áreas asignadas a este portal"));

                return Ok(Response<List<BookingPortalAreaResponse>>.Success(areas, "Áreas obtenidas exitosamente"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, Response<string>.ServerError(ex.Message));
            }
        }

        [HttpGet("area/{areaId}")]
        [ProducesResponseType(typeof(Response<List<BookingPortalAreaResponse>>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Obtener portales por área", Description = "Obtiene todos los portales que tienen asignada un área específica")]
        [DisableBitacora]
        public async Task<IActionResult> GetPortalsByArea(int areaId)
        {
            try
            {
                var portals = await _portalAreasService.GetPortalsByAreaIdAsync(areaId);
                if (!portals.Any())
                    return StatusCode(204, Response<string>.NoContent("Esta área no está asignada a ningún portal"));

                return Ok(Response<List<BookingPortalAreaResponse>>.Success(portals, "Portales obtenidos exitosamente"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, Response<string>.ServerError(ex.Message));
            }
        }

        [HttpGet("portal/{portalId}/default")]
        [ProducesResponseType(typeof(Response<BookingPortalAreaResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Obtener área por defecto", Description = "Obtiene el área por defecto de un portal")]
        [DisableBitacora]
        public async Task<IActionResult> GetDefaultArea(int portalId)
        {
            try
            {
                var defaultArea = await _portalAreasService.GetDefaultAreaByPortalIdAsync(portalId);
                if (defaultArea == null)
                    return NotFound(Response<string>.NotFound("No hay área por defecto configurada"));

                return Ok(Response<BookingPortalAreaResponse>.Success(defaultArea, "Área por defecto obtenida exitosamente"));
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
        [SwaggerOperation(Summary = "Asignar área a portal", Description = "Asigna un área a un portal de reservas")]
        public async Task<IActionResult> AddAreaToPortal([FromBody] BookingPortalAreaRequest request)
        {
            try
            {
                if (request.PortalId <= 0 || request.AreaId <= 0)
                {
                    return BadRequest(Response<string>.BadRequest(new List<string> { "Portal ID y Area ID son requeridos" }, 400));
                }

                var response = await _portalAreasService.AddAreaToPortalAsync(request);
                return CreatedAtAction(nameof(GetAreasByPortal), new { portalId = request.PortalId },
                    Response<BookingPortalAreaResponse>.Created(response));
            }
            catch (Exception ex)
            {
                return StatusCode(500, Response<string>.ServerError(ex.Message));
            }
        }

        [HttpDelete("portal/{portalId}/area/{areaId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Remover área del portal", Description = "Remueve un área de un portal de reservas")]
        public async Task<IActionResult> RemoveAreaFromPortal(int portalId, int areaId)
        {
            try
            {
                await _portalAreasService.RemoveAreaFromPortalAsync(portalId, areaId);
                return Ok(Response<string>.Success(null, "Área removida del portal exitosamente"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, Response<string>.ServerError(ex.Message));
            }
        }

        [HttpPut("portal/{portalId}/area/{areaId}/set-default")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Establecer área por defecto", Description = "Establece un área como por defecto para el portal")]
        public async Task<IActionResult> SetDefaultArea(int portalId, int areaId)
        {
            try
            {
                var response = await _portalAreasService.SetDefaultAreaAsync(portalId, areaId);
                return Ok(Response<BookingPortalAreaResponse>.Success(response, "Área establecida como por defecto"));
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(Response<string>.NotFound(ex.Message));
            }
            catch (Exception ex)
            {
                return StatusCode(500, Response<string>.ServerError(ex.Message));
            }
        }

        [HttpPut("portal/{portalId}/areas")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Actualizar áreas del portal", Description = "Actualiza todas las áreas asignadas a un portal")]
        public async Task<IActionResult> UpdatePortalAreas(int portalId, [FromBody] List<BookingPortalAreaRequest> areas)
        {
            try
            {
                if (areas == null || !areas.Any())
                {
                    return BadRequest(Response<string>.BadRequest(new List<string> { "Lista de áreas no puede estar vacía" }, 400));
                }

                var response = await _portalAreasService.UpdatePortalAreasAsync(portalId, areas);
                return Ok(Response<List<BookingPortalAreaResponse>>.Success(response, "Áreas del portal actualizadas exitosamente"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, Response<string>.ServerError(ex.Message));
            }
        }
    }
}