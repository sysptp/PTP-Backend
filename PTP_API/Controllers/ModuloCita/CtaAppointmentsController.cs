using BussinessLayer.Atributes;
using BussinessLayer.DTOs.ModuloCitas.CtaAppointments;
using BussinessLayer.DTOs.ModuloGeneral.Email;
using BussinessLayer.Interfaces.Services.ModuloCitas;
using BussinessLayer.Interfaces.Services.ModuloGeneral.Email;
using BussinessLayer.Wrappers;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net.Mime;

namespace PTP_API.Controllers.ModuloCita
{

    [ApiController]
    [SwaggerTag("Gestión de citas")]
    [Route("api/v1/[controller]")]
    [Authorize]
    //[EnableBitacora]
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
        [ProducesResponseType(typeof(Response<IEnumerable<CtaAppointmentsResponse>>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Obtener citas", Description = "Devuelve una lista de citas o una cita específica si se proporciona un ID")]
        public async Task<IActionResult> GetAllAppointments([FromQuery] string? appointmentCode, int? appointmentId, long? companyId, int? userId)
        {
            try
            {
                if (appointmentId.HasValue)
                {
                    var appointment = await _appointmentService.GetByIdResponse(appointmentId);
                    if (appointment == null)
                        return NotFound(Response<CtaAppointmentsResponse>.NotFound("Cita no encontrada."));

                    return Ok(Response<CtaAppointmentsResponse>.Success(appointment, "Cita encontrada."));
                }
                else
                {
                    var appointments = await _appointmentService.GetAllDto();
                    if (appointments == null || !appointments.Any())
                        return StatusCode(204, Response<IEnumerable<CtaAppointmentsResponse>>.NoContent("No hay citas disponibles."));

                    // Aplicando filtros de manera más clara y manejando valores nulos
                    var filteredAppointments = appointments.AsQueryable();

                    if (companyId.HasValue)
                        filteredAppointments = filteredAppointments.Where(x => x.CompanyId == companyId.Value);

                    if (!string.IsNullOrEmpty(appointmentCode))
                        filteredAppointments = filteredAppointments.Where(x => x.AppointmentCode == appointmentCode);

                    if (userId.HasValue)
                        filteredAppointments = filteredAppointments.Where(x => x.UserId == userId.Value);

                    return Ok(Response<IEnumerable<CtaAppointmentsResponse>>.Success(
                        filteredAppointments.ToList(),
                        "Citas obtenidas correctamente."));
                }
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

                var response = await _appointmentService.Add(appointmentDto);
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
        public async Task<IActionResult> GetAlParticipants([FromQuery] int? participantTypeId, int? participantId, long? companyId)
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
    }

}
