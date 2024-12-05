using BussinessLayer.Atributes;
using BussinessLayer.DTOs.ModuloGeneral.Configuracion.Seguridad.Schedule;
using BussinessLayer.Interfaces.ISeguridad;
using BussinessLayer.Wrappers;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net.Mime;

namespace PTP_API.Controllers.ModuloGeneral.Configuracion.Seguridad
{
    [ApiController]
    [SwaggerTag("Servicio de manejo de horarios de usuarios")]
    [Route("api/v1/[controller]")]
    [Authorize]
    [EnableAuditing]
    public class ScheduleUserController : ControllerBase
    {
        private readonly IGnScheduleUserService _gnScheduleUserService;
        private readonly IValidator<GnScheduleUserRequest> _validator;

        public ScheduleUserController(IGnScheduleUserService gnScheduleUserService, IValidator<GnScheduleUserRequest> validator)
        {
            _gnScheduleUserService = gnScheduleUserService;
            _validator = validator;
        }

        [HttpGet]
        [ProducesResponseType(typeof(Response<IEnumerable<GnScheduleUserResponse>>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Obtener Horarios de usuarios", Description = "Devuelve una lista de horarios de usuarios o un horario de usuario específico si se proporciona un ID")]
        [DisableAuditing]
        public async Task<IActionResult> GetAllUserSchedules([FromQuery] long? companyId, int? userId, int? scheduleId)
        {
            try
            {
               
                 var schedules = await _gnScheduleUserService.GetAllByFilters(companyId,userId,scheduleId);
                 if (schedules == null || !schedules.Any())
                 {
                    return StatusCode(204, Response<IEnumerable<GnScheduleUserResponse>>.NoContent("No hay horarios de usuarios disponibles."));
                 }
                 return Ok(Response<IEnumerable<GnScheduleUserResponse>>.Success(schedules, "Horarios de usuarios obtenidos correctamente."));
                
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
        [SwaggerOperation(Summary = "Crear un nuevo horario de usuario", Description = "Endpoint para crear un horario de usuario nuevo")]
        public async Task<IActionResult> CreateUserSchedule([FromBody] GnScheduleUserRequest horarioDto)
        {
            try
            {
                var validationResult = await _validator.ValidateAsync(horarioDto);

                if (!validationResult.IsValid)
                {
                    var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                    return BadRequest(Response<string>.BadRequest(errors, 400));
                }

                var response = await _gnScheduleUserService.Add(horarioDto);

                return CreatedAtAction(nameof(GetAllUserSchedules), response);

            }
            catch (Exception ex)
            {
                return StatusCode(500, Response<string>.ServerError("Ocurrió un error al crear el horario. Por favor, intente más tarde."));
            }
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Actualizar horario", Description = "Endpoint para actualizar los datos de un horario")]
        public async Task<IActionResult> UpdateUserSchedule(int id, [FromBody] GnScheduleUserRequest scheduleDto)
        {
            var validationResult = await _validator.ValidateAsync(scheduleDto);

            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return BadRequest(Response<string>.BadRequest(errors, 400));
            }

            try
            {
                var existingSchedule = await _gnScheduleUserService.GetByIdRequest(id);
                if (existingSchedule == null)
                    return NotFound(Response<string>.NotFound("horario no encontrado"));

                scheduleDto.Id = id;
                await _gnScheduleUserService.Update(scheduleDto, id);
                return Ok(Response<string>.Success(null, "horario actualizado correctamente"));
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
        [SwaggerOperation(Summary = "Eliminar horario", Description = "Endpoint para eliminar un horario")]
        public async Task<IActionResult> DeleteUserSchedule(int id)
        {
            try
            {
                var existingSchedule = await _gnScheduleUserService.GetByIdRequest(id);
                if (existingSchedule == null)
                    return NotFound(Response<string>.NotFound("horario no encontrado"));

                await _gnScheduleUserService.Delete(id);
                return Ok(Response<string>.Success(null, "horario eliminado correctamente"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, Response<string>.ServerError("Ocurrió un error al eliminar el horario. Por favor, intente más tarde."));
            }
        }
    }
}
