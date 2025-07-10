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
    [SwaggerTag("Gestión de Usuarios del Portal de Reservas")]
    [Route("api/v1/[controller]")]
    [Authorize]
    [EnableBitacora]
    public class CtaBookingPortalUsersController : ControllerBase
    {
        private readonly ICtaBookingPortalUsersService _portalUsersService;

        public CtaBookingPortalUsersController(ICtaBookingPortalUsersService portalUsersService)
        {
            _portalUsersService = portalUsersService;
        }

        [HttpGet("portal/{portalId}")]
        [ProducesResponseType(typeof(Response<List<BookingPortalUserResponse>>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Obtener usuarios por portal", Description = "Obtiene todos los usuarios asignados a un portal específico")]
        [DisableBitacora]
        public async Task<IActionResult> GetUsersByPortal(int portalId)
        {
            try
            {
                var users = await _portalUsersService.GetUsersByPortalIdAsync(portalId);
                if (!users.Any())
                    return StatusCode(204, Response<string>.NoContent("No hay usuarios asignados a este portal"));

                return Ok(Response<List<BookingPortalUserResponse>>.Success(users, "Usuarios obtenidos exitosamente"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, Response<string>.ServerError(ex.Message));
            }
        }

        [HttpGet("user/{userId}")]
        [ProducesResponseType(typeof(Response<List<BookingPortalUserResponse>>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Obtener portales por usuario", Description = "Obtiene todos los portales asignados a un usuario específico")]
        [DisableBitacora]
        public async Task<IActionResult> GetPortalsByUser(int userId)
        {
            try
            {
                var portals = await _portalUsersService.GetPortalsByUserIdAsync(userId);
                if (!portals.Any())
                    return StatusCode(204, Response<string>.NoContent("Este usuario no está asignado a ningún portal"));

                return Ok(Response<List<BookingPortalUserResponse>>.Success(portals, "Portales obtenidos exitosamente"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, Response<string>.ServerError(ex.Message));
            }
        }

        [HttpGet("portal/{portalId}/main-assignee")]
        [ProducesResponseType(typeof(Response<BookingPortalUserResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Obtener usuario principal", Description = "Obtiene el usuario principal asignado de un portal")]
        [DisableBitacora]
        public async Task<IActionResult> GetMainAssignee(int portalId)
        {
            try
            {
                var mainAssignee = await _portalUsersService.GetMainAssigneeByPortalIdAsync(portalId);
                if (mainAssignee == null)
                    return NotFound(Response<string>.NotFound("No hay usuario principal configurado"));

                return Ok(Response<BookingPortalUserResponse>.Success(mainAssignee, "Usuario principal obtenido exitosamente"));
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
        [SwaggerOperation(Summary = "Asignar usuario a portal", Description = "Asigna un usuario a un portal de reservas")]
        public async Task<IActionResult> AddUserToPortal([FromBody] BookingPortalUserRequest request)
        {
            try
            {
                if (request.PortalId <= 0 || request.UserId <= 0)
                {
                    return BadRequest(Response<string>.BadRequest(new List<string> { "Portal ID y User ID son requeridos" }, 400));
                }

                var response = await _portalUsersService.AddUserToPortalAsync(request);
                return CreatedAtAction(nameof(GetUsersByPortal), new { portalId = request.PortalId },
                    Response<BookingPortalUserResponse>.Created(response));
            }
            catch (Exception ex)
            {
                return StatusCode(500, Response<string>.ServerError(ex.Message));
            }
        }

        [HttpDelete("portal/{portalId}/user/{userId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Remover usuario del portal", Description = "Remueve un usuario de un portal de reservas")]
        public async Task<IActionResult> RemoveUserFromPortal(int portalId, int userId)
        {
            try
            {
                await _portalUsersService.RemoveUserFromPortalAsync(portalId, userId);
                return Ok(Response<string>.Success(null, "Usuario removido del portal exitosamente"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, Response<string>.ServerError(ex.Message));
            }
        }

        [HttpPut("portal/{portalId}/user/{userId}/set-main-assignee")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Establecer usuario principal", Description = "Establece un usuario como principal para el portal")]
        public async Task<IActionResult> SetMainAssignee(int portalId, int userId)
        {
            try
            {
                var response = await _portalUsersService.SetMainAssigneeAsync(portalId, userId);
                return Ok(Response<BookingPortalUserResponse>.Success(response, "Usuario establecido como principal"));
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

        [HttpPut("portal/{portalId}/users")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Actualizar usuarios del portal", Description = "Actualiza todos los usuarios asignados a un portal")]
        public async Task<IActionResult> UpdatePortalUsers(int portalId, [FromBody] List<BookingPortalUserRequest> users)
        {
            try
            {
                if (users == null || !users.Any())
                {
                    return BadRequest(Response<string>.BadRequest(new List<string> { "Lista de usuarios no puede estar vacía" }, 400));
                }

                var response = await _portalUsersService.UpdatePortalUsersAsync(portalId, users);
                return Ok(Response<List<BookingPortalUserResponse>>.Success(response, "Usuarios del portal actualizados exitosamente"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, Response<string>.ServerError(ex.Message));
            }
        }
    }
}