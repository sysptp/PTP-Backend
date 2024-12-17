using BussinessLayer.Atributes;
using BussinessLayer.DTOs.ModuloCitas.CtaAppointmentManagement;
using BussinessLayer.Interface.Modulo_Citas;
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
    [EnableBitacora]
    public class CtaAppointmentManagementController : ControllerBase
    {
        private readonly ICtaAppointmentManagementService _appointmentService;
        private readonly IValidator<CtaAppointmentManagementRequest> _validator;

        public CtaAppointmentManagementController(ICtaAppointmentManagementService appointmentService, IValidator<CtaAppointmentManagementRequest> validator)
        {
            _appointmentService = appointmentService;
            _validator = validator;
        }

        [HttpGet]
        [ProducesResponseType(typeof(Response<IEnumerable<CtaAppointmentManagementResponse>>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Obtener citas", Description = "Devuelve una lista de citas o una cita específica si se proporciona un ID")]
        [DisableBitacora]
        public async Task<IActionResult> GetAllAppointments([FromQuery] int? IdManagmentAppoinment)
        {
            try
            {
                if (IdManagmentAppoinment.HasValue)
                {
                    var appointment = await _appointmentService.GetByIdResponse(IdManagmentAppoinment);
                    if (appointment == null)
                    {
                        return NotFound(Response<CtaAppointmentManagementResponse>.NotFound("Cita no encontrada."));
                    }
                    return Ok(Response<CtaAppointmentManagementResponse>.Success(appointment, "Cita encontrada."));
                }
                else
                {
                    var appointments = await _appointmentService.GetAllDto();

                    if (appointments == null || !appointments.Any())
                    {
                        return StatusCode(204, Response<IEnumerable<CtaAppointmentManagementResponse>>.NoContent("No hay citas disponibles."));
                    }
                    return Ok(Response<IEnumerable<CtaAppointmentManagementResponse>>.Success(appointments, "Citas obtenidas correctamente."));
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
        [SwaggerOperation(Summary = "Crear una nueva cita", Description = "Endpoint para crear una nueva cita")]
        public async Task<IActionResult> CreateAppointment([FromBody] CtaAppointmentManagementRequest appointmentDto)
        {
            try
            {
                var validationResult = await _validator.ValidateAsync(appointmentDto);

                if (!validationResult.IsValid)
                {
                    var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                    return BadRequest(Response<string>.BadRequest(errors, 400));
                }

                var response = await _appointmentService.Add(appointmentDto);

                return CreatedAtAction(nameof(GetAllAppointments), response);
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
        [SwaggerOperation(Summary = "Actualizar una cita", Description = "Endpoint para actualizar los datos de una cita")]
        public async Task<IActionResult> UpdateAppointment(int id, [FromBody] CtaAppointmentManagementRequest appointmentDto)
        {
            var validationResult = await _validator.ValidateAsync(appointmentDto);

            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return BadRequest(Response<string>.BadRequest(errors, 400));
            }

            try
            {
                var existingAppointment = await _appointmentService.GetByIdRequest(id);
                if (existingAppointment == null)
                    return NotFound(Response<string>.NotFound("Cita no encontrada."));

                appointmentDto.IdManagementAppointment = id;
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
    }

}
