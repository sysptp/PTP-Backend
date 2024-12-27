using BussinessLayer.Atributes;
using BussinessLayer.DTOs.ModuloGeneral.Configuracion.Seguridad.Schedule;
using BussinessLayer.Interfaces.Services.ModuloGeneral.Seguridad;
using BussinessLayer.Wrappers;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net.Mime;

namespace PTP_API.Controllers.ModuloGeneral.Configuracion.Seguridad
{
    [ApiController]
    [SwaggerTag("Servicio de manejo de horarios")]
    [Route("api/v1/[controller]")]
    [Authorize]
    [EnableBitacora]
    public class ScheduleController : ControllerBase
    {
        private readonly IGnScheduleService _gnScheduleService;
        private readonly IValidator<GnScheduleRequest> _validator;

        public ScheduleController(IGnScheduleService gnScheduleService, IValidator<GnScheduleRequest> validator)
        {
            _gnScheduleService = gnScheduleService;
            _validator = validator;
        }

        [HttpGet]
        [ProducesResponseType(typeof(Response<IEnumerable<GnScheduleResponse>>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Obtener Horarios", Description = "Devuelve una lista de horarios o un horario específico si se proporciona un ID")]
        [DisableBitacora]
        public async Task<IActionResult> GetAllSchedule([FromQuery] long? companyId, int? id)
        {
            try
            {
                if (id.HasValue)
                {
                    var schedule = await _gnScheduleService.GetByIdResponse((int)id);
                    if (schedule == null)
                    {
                        return NotFound(Response<GnScheduleResponse>.NotFound("Horario no encontrado."));
                    }
                    return Ok(Response<GnScheduleResponse>.Success(schedule, "Horario encontrado."));
                }
                else
                {
                    var schedules = await _gnScheduleService.GetAllDto();
                    schedules = companyId != null ? schedules : schedules.Where(x => x.CompanyId == companyId).ToList();    
                    if (schedules == null || !schedules.Any())
                    {
                        return StatusCode(204, Response<IEnumerable<GnScheduleResponse>>.NoContent("No hay horarios disponibles."));
                    }
                    return Ok(Response<IEnumerable<GnScheduleResponse>>.Success(schedules, "Horarios obtenidos correctamente."));
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
        [SwaggerOperation(Summary = "Crear un nuevo horario", Description = "Endpoint para crear un horario nuevo")]
        public async Task<IActionResult> CreateSchedule([FromBody] GnScheduleRequest horarioDto)
        {
            try
            {
                var validationResult = await _validator.ValidateAsync(horarioDto);

                if (!validationResult.IsValid)
                {
                    var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                    return BadRequest(Response<string>.BadRequest(errors, 400));
                }

                var response = await _gnScheduleService.Add(horarioDto);

                return CreatedAtAction(nameof(GetAllSchedule), response);
                
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
        [SwaggerOperation(Summary = "Actualizar horario", Description = "Endpoint para actualizar los datos de un horario")]
        public async Task<IActionResult> UpdateSchedule(int id, [FromBody] GnScheduleRequest scheduleDto)
        {
            var validationResult = await _validator.ValidateAsync(scheduleDto);

            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return BadRequest(Response<string>.BadRequest(errors, 400));
            }

            try
            {
                var existingSchedule = await _gnScheduleService.GetByIdRequest(id);
                if (existingSchedule == null)
                    return NotFound(Response<string>.NotFound("horario no encontrado"));

                scheduleDto.Id = id;
                await _gnScheduleService.Update(scheduleDto, id);
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
        public async Task<IActionResult> DeleteSchedule(int id)
        {
            try
            {
                var existingSchedule = await _gnScheduleService.GetByIdRequest(id);
                if (existingSchedule == null)
                    return NotFound(Response<string>.NotFound("horario no encontrado"));

                await _gnScheduleService.Delete(id);
                return Ok(Response<string>.Success(null, "horario eliminado correctamente"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, Response<string>.ServerError(ex.Message));
            }
        }
    }
}
