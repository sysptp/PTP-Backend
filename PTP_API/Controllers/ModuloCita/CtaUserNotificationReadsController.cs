using System.Net.Mime;
using BussinessLayer.DTOs.ModuloCitas.CtaUserNotificationReads;
using BussinessLayer.Interfaces.Services.ModuloCitas;
using BussinessLayer.Wrappers;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace PTP_API.Controllers.ModuloCita
{
    [ApiController]
    [SwaggerTag("Gesti�n de CtaUserNotificationReads")]
    [Route("api/v1/[controller]")]
    [Authorize]
    public class CtaUserNotificationReadsController : ControllerBase
    {
        private readonly ICtaUserNotificationReadsService _ctausernotificationreadsService;
        private readonly IValidator<CtaUserNotificationReadsRequest> _validator;

        public CtaUserNotificationReadsController(ICtaUserNotificationReadsService ctausernotificationreadsService, IValidator<CtaUserNotificationReadsRequest> validator)
        {
            _ctausernotificationreadsService = ctausernotificationreadsService;
            _validator = validator;
        }

        [HttpGet]
        [ProducesResponseType(typeof(Response<IEnumerable<CtaUserNotificationReadsResponse>>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Obtener CtaUserNotificationReads", Description = "Devuelve una lista de CtaUserNotificationReads o un elemento espec�fico si se proporciona un ID")]
        public async Task<IActionResult> GetAll([FromQuery] long? id, long? companyId)
        {
            try
            {
                if (id.HasValue)
                {
                    var item = await _ctausernotificationreadsService.GetByIdResponse(id.Value);
                    if (item == null)
                        return NotFound(Response<CtaUserNotificationReadsResponse>.NotFound("CtaUserNotificationReads no encontrado."));

                    return Ok(Response<CtaUserNotificationReadsResponse>.Success(item, "CtaUserNotificationReads encontrado."));
                }
                else
                {
                    var items = await _ctausernotificationreadsService.GetAllDto();
                    if (items == null || !items.Any())
                        return StatusCode(204, Response<IEnumerable<CtaUserNotificationReadsResponse>>.NoContent("No hay CtaUserNotificationReads disponibles."));

                    return Ok(Response<IEnumerable<CtaUserNotificationReadsResponse>>.Success(items, "CtaUserNotificationReads obtenidos correctamente."));
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, Response<string>.ServerError(ex.Message));
            }
        }

        [HttpGet("user/{userId}/paginated")]
        [ProducesResponseType(typeof(Response<UserNotificationsPagedResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Obtener notificaciones de usuario paginadas", Description = "Devuelve las notificaciones de un usuario con paginaci�n y filtros")]
        public async Task<IActionResult> GetUserNotificationsPaginated(
            int userId,
            [FromQuery] long companyId,
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 20,
            [FromQuery] bool? isRead = null,
            [FromQuery] string notificationType = null,
            [FromQuery] DateTime? fromDate = null,
            [FromQuery] DateTime? toDate = null)
        {
            try
            {
                if (pageSize > 100)
                    return BadRequest(Response<string>.BadRequest(new List<string> { "El tama�o de p�gina no puede ser mayor a 100" }));

                var request = new UserNotificationsPagedRequest
                {
                    UserId = userId,
                    CompanyId = companyId,
                    Page = page,
                    PageSize = pageSize,
                    IsRead = isRead,
                    NotificationType = notificationType,
                    FromDate = fromDate,
                    ToDate = toDate
                };

                var result = await _ctausernotificationreadsService.GetUserNotificationsPaginatedAsync(request);
                return Ok(Response<UserNotificationsPagedResponse>.Success(result, "Notificaciones obtenidas correctamente."));
            }
            catch (Exception ex)
            {
                return StatusCode(500, Response<string>.ServerError(ex.Message));
            }
        }

        [HttpGet("user/{userId}/unread-count")]
        [ProducesResponseType(typeof(Response<int>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Obtener contador de notificaciones no le�das", Description = "Devuelve el n�mero de notificaciones no le�das de un usuario")]
        public async Task<IActionResult> GetUnreadCount(int userId, [FromQuery] long companyId)
        {
            try
            {
                var count = new { count = await _ctausernotificationreadsService.GetUnreadCountAsync(userId, companyId) };
                return Ok(Response<object>.Success(count, "Contador obtenido correctamente."));
            }
            catch (Exception ex)
            {
                return StatusCode(500, Response<string>.ServerError(ex.Message));
            }
        }

        [HttpPatch("mark-as-read")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Marcar notificaci�n como le�da", Description = "Marca una notificaci�n espec�fica como le�da")]
        public async Task<IActionResult> MarkAsRead([FromBody] MarkNotificationAsReadRequest request)
        {
            try
            {
                var success = await _ctausernotificationreadsService.MarkNotificationAsReadAsync(request);
                if (!success)
                    return NotFound(Response<string>.NotFound("Notificaci�n no encontrada."));

                return Ok(Response<string>.Success(null, "Notificaci�n marcada como le�da."));
            }
            catch (Exception ex)
            {
                return StatusCode(500, Response<string>.ServerError(ex.Message));
            }
        }

        [HttpPatch("mark-multiple-as-read")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Marcar m�ltiples notificaciones como le�das", Description = "Marca m�ltiples notificaciones como le�das")]
        public async Task<IActionResult> MarkMultipleAsRead([FromBody] MarkMultipleNotificationsAsReadRequest request)
        {
            try
            {
                if (request.NotificationIds == null || !request.NotificationIds.Any())
                    return BadRequest(Response<string>.BadRequest(new List<string> { "Se debe proporcionar al menos un ID de notificaci�n" }));

                var success = await _ctausernotificationreadsService.MarkMultipleNotificationsAsReadAsync(request);
                if (!success)
                    return BadRequest(Response<string>.BadRequest(new List<string> {"No se encontraron notificaciones para marcar"}));

                return Ok(Response<string>.Success(null, "Notificaciones marcadas como le�das."));
            }
            catch (Exception ex)
            {
                return StatusCode(500, Response<string>.ServerError(ex.Message));
            }
        }

        [HttpPatch("user/{userId}/mark-all-as-read")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Marcar todas las notificaciones como le�das", Description = "Marca todas las notificaciones de un usuario como le�das")]
        public async Task<IActionResult> MarkAllAsRead(int userId, [FromQuery] long companyId)
        {
            try
            {
                var success = await _ctausernotificationreadsService.MarkAllNotificationsAsReadAsync(userId, companyId);
                if (!success)
                    return BadRequest(Response<string>.BadRequest(new List<string> { "No hay notificaciones no le�das para marcar" }));

                return Ok(Response<string>.Success(null, "Todas las notificaciones marcadas como le�das."));
            }
            catch (Exception ex)
            {
                return StatusCode(500, Response<string>.ServerError(ex.Message));
            }
        }

    }
}
