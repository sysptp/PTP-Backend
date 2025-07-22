using System.Net.Mime;
using BussinessLayer.Atributes;
using BussinessLayer.DTOs.Common;
using BussinessLayer.DTOs.ModuloCitas.CtaAppointments;
using BussinessLayer.Interfaces.Services.ModuloCitas;
using BussinessLayer.Interfaces.Services.ModuloGeneral.Email;
using BussinessLayer.Wrappers;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace PTP_API.Controllers.ModuloCita
{

    [ApiController]
    [SwaggerTag("Gestión de citas")]
    [Route("api/v1/[controller]")]
    [Authorize]
    [EnableBitacora]
    public class CtaAppointmentsController : ControllerBase
    {
        private readonly ICtaAppointmentsService _appointmentService;
        private readonly IGnEmailService _gnEmailService;
        private readonly IValidator<CtaAppointmentsRequest> _validator;

        public CtaAppointmentsController(ICtaAppointmentsService appointmentService, IValidator<CtaAppointmentsRequest> validator, IGnEmailService gnEmailService)
        {
            _appointmentService = appointmentService;
            _validator = validator;
            _gnEmailService = gnEmailService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(Response<PaginatedResponse<CtaAppointmentsResponse>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response<CtaAppointmentsResponse>), StatusCodes.Status200OK)]
        [SwaggerOperation(Summary = "Obtener citas con paginación eficiente", Description = "Devuelve citas con paginación a nivel de base de datos para mejor rendimiento")]
        [DisableBitacora]
        public async Task<IActionResult> GetAllAppointments(
            [FromQuery] string? appointmentCode,
            [FromQuery] int? appointmentId,
            [FromQuery] long? companyId,
            [FromQuery] int? userId,
            [FromQuery] int? page,
            [FromQuery] int? size)
        {
            try
            {
                // Si solicita una cita específica, usar método existente
                if (appointmentId.HasValue)
                {
                    var appointment = await _appointmentService.GetByIdResponse(appointmentId);
                    if (appointment == null)
                        return NotFound(Response<CtaAppointmentsResponse>.NotFound("Cita no encontrada."));

                    return Ok(Response<CtaAppointmentsResponse>.Success(appointment, "Cita encontrada."));
                }

                // ⭐ USAR PAGINACIÓN A NIVEL DE BD
                var pagination = new PaginationRequest(page, size);
                var paginatedResult = await _appointmentService.GetAllPaginatedAsync(
                    pagination,
                    companyId,
                    userId,
                    appointmentCode
                );

                if (paginatedResult.TotalCount == 0)
                {
                    return StatusCode(204, Response<PaginatedResponse<CtaAppointmentsResponse>>.NoContent("No hay citas disponibles."));
                }

                return Ok(Response<PaginatedResponse<CtaAppointmentsResponse>>.Success(
                    paginatedResult,
                    $"Citas obtenidas correctamente. Página {paginatedResult.Page} de {paginatedResult.TotalPages}"));
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
        [SwaggerOperation(Summary = "Crear una nueva cita", Description = "Endpoint para registrar una cita")]
        public async Task<IActionResult> CreateAppointment([FromBody] CtaAppointmentsRequest appointmentDto, bool deleteExistingAppointment = false)
        {
            try
            {
                var validationResult = await _validator.ValidateAsync(appointmentDto);

                if (!validationResult.IsValid)
                {
                    var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                    return BadRequest(Response<string>.BadRequest(errors, 400));
                }

                if (deleteExistingAppointment)
                {
                    await _appointmentService.DeleteExistsAppointmentInTimeRange(appointmentDto);
                }
                else
                {

                    var existsMessage = await _appointmentService.ExistsAppointmentInTimeRange(appointmentDto);
                    if (existsMessage != null)
                    {
                        return Ok(Response<DetailMessage>.Success(existsMessage));
                    }
                }

                var response = await _appointmentService.AddAppointment(appointmentDto,false);
                return CreatedAtAction(nameof(GetAllAppointments), Response<CtaAppointmentsResponse>.Created(response));
            }
            catch (Exception ex)
            {
                return StatusCode(500, Response<string>.ServerError(ex.Message));
            }
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Actualizar una cita", Description = "Endpoint para actualizar una cita")]
        public async Task<IActionResult> UpdateAppointment(int id, [FromBody] CtaAppointmentsRequest appointmentDto)
        {
            try
            {
                var validationResult = await _validator.ValidateAsync(appointmentDto);

                if (!validationResult.IsValid)
                {
                    var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                    return BadRequest(Response<string>.BadRequest(errors, 400));
                }

                var existingAppointment = await _appointmentService.GetByIdRequest(id);
                if (existingAppointment == null)
                    return NotFound(Response<string>.NotFound("Cita no encontrada."));

                appointmentDto.AppointmentId = id;
                appointmentDto.AppointmentCode = existingAppointment.AppointmentCode;
                await _appointmentService.Update(appointmentDto, id);
                return Ok(Response<string>.Success(null, "Cita actualizada correctamente."));
            }
            catch (Exception ex)
            {
                return StatusCode(500, Response<string>.ServerError(ex.Message));
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Eliminar una cita", Description = "Endpoint para eliminar una cita")]
        public async Task<IActionResult> DeleteAppointment(int id)
        {
            try
            {
                var existingAppointment = await _appointmentService.GetByIdRequest(id);
                if (existingAppointment == null)
                    return NotFound(Response<string>.NotFound("Cita no encontrada."));

                await _appointmentService.Delete(id);
                return Ok(Response<string>.Success(null, "Cita eliminada correctamente."));
            }
            catch (Exception ex)
            {
                return StatusCode(500, Response<string>.ServerError(ex.Message));
            }
        }


        [HttpGet("GetAllParticipants")]
        [ProducesResponseType(typeof(Response<IEnumerable<CtaAppointmentsResponse>>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Obtener Todos los participantes que pueden particar en Citas", Description = "Devuelve una lista de posibles participantes para citas o un participante específico si se proporciona un ID")]
        [DisableBitacora]
        public async Task<IActionResult> GetAllParticipants([FromQuery] int? participantTypeId, int? participantId, long? companyId)
        {
            try
            {
                var participants = await _appointmentService.GetAllParticipants();
                if (participants == null || !participants.Any())
                    return StatusCode(204, Response<IEnumerable<AppointmentParticipantsResponse>>.NoContent("No hay participantes disponibles."));

                var filteredParticipants = participants.AsQueryable();

                if (companyId.HasValue)
                    filteredParticipants = filteredParticipants.Where(x => x.CompanyId == companyId);

                if (participantId.HasValue)
                    filteredParticipants = filteredParticipants.Where(x => x.ParticipantId == participantId);

                if (participantTypeId.HasValue)
                    filteredParticipants = filteredParticipants.Where(x => x.ParticipantTypeId == participantTypeId);

                return Ok(Response<IEnumerable<AppointmentParticipantsResponse>>.Success(
                    filteredParticipants.ToList(),
                    "participantes obtenidos correctamente."));

            }
            catch (Exception ex)
            {
                return StatusCode(500, Response<string>.ServerError(ex.Message));
            }
        }

        //[HttpPost("MarkNotificationAsRead")]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[SwaggerOperation(Summary = "Marca una notificación como leída")]
        //public async Task<IActionResult> MarkNotificationAsRead([FromBody] MarkNotificationReadRequest request)
        //{
        //    try
        //    {
        //        await _notificationService.MarkAsRead(request.UserId, request.NotificationId, request.NotificationType);
        //        return Ok(Response<string>.Success(null, "Notificación marcada como leída."));
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, Response<string>.ServerError(ex.Message));
        //    }
        //}
    }

}
