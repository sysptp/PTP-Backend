using System.Net.Mime;
using BussinessLayer.Atributes;
using BussinessLayer.DTOs.Common;
using BussinessLayer.DTOs.ModuloCitas.CtaSessions;
using BussinessLayer.Interfaces.Services.ModuloCitas;
using BussinessLayer.Wrappers;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace PTP_API.Controllers.ModuloCita
{

    [ApiController]
    [SwaggerTag("Gestión de sesiones")]
    [Route("api/v1/[controller]")]
    [Authorize]
    [EnableBitacora]
    public class CtaSessionsController : ControllerBase
    {
        private readonly ICtaSessionsService _sessionsService;
        private readonly IValidator<CtaSessionsRequest> _validator;

        public CtaSessionsController(ICtaSessionsService sessionsService, IValidator<CtaSessionsRequest> validator)
        {
            _sessionsService = sessionsService;
            _validator = validator;
        }

        [HttpGet]
        [ProducesResponseType(typeof(Response<PaginatedResponse<CtaSessionsResponse>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response<CtaSessionsResponse>), StatusCodes.Status200OK)]
        [SwaggerOperation(Summary = "Obtener sesiones con paginación eficiente")]
        [DisableBitacora]
        public async Task<IActionResult> GetAllSessions(
            [FromQuery] int? IdSession,
            [FromQuery] long? companyId,
            [FromQuery] int? userId,
            [FromQuery] int? page,
            [FromQuery] int? size)
        {
            try
            {
                // Si solicita una sesión específica
                if (IdSession.HasValue)
                {
                    var session = await _sessionsService.GetByIdResponse(IdSession.Value);
                    if (session == null)
                        return NotFound(Response<CtaSessionsResponse>.NotFound("Sesión no encontrada."));

                    return Ok(Response<CtaSessionsResponse>.Success(session, "Sesión encontrada."));
                }

                // ⭐ USAR PAGINACIÓN A NIVEL DE BD
                var pagination = new PaginationRequest(page, size);
                var paginatedResult = await _sessionsService.GetAllPaginatedAsync(
                    pagination,
                    companyId,
                    userId
                );

                if (paginatedResult.TotalCount == 0)
                {
                    return StatusCode(204, Response<PaginatedResponse<CtaSessionsResponse>>.NoContent("No hay sesiones disponibles."));
                }

                return Ok(Response<PaginatedResponse<CtaSessionsResponse>>.Success(
                    paginatedResult,
                    $"Sesiones obtenidas correctamente. Página {paginatedResult.Page} de {paginatedResult.TotalPages}"));
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
        [SwaggerOperation(Summary = "Crear una nueva sesión", Description = "Endpoint para registrar una sesión")]
        public async Task<IActionResult> CreateSession([FromBody] CtaSessionsRequest sessionDto, bool? deleteExistingAppointmentsWithCoincides)
        {
            try
            {
                var validationResult = await _validator.ValidateAsync(sessionDto);

                if (!validationResult.IsValid)
                {
                    var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                    return BadRequest(Response<string>.BadRequest(errors, 400));
                }

                if (deleteExistingAppointmentsWithCoincides != null && deleteExistingAppointmentsWithCoincides == true)
                {
                    await _sessionsService.DeleteAppointmentsInSessionRange(sessionDto);
                }
                else
                {

                    var existsMessage = await _sessionsService.GetConflictingAppointmentsInSessionRange(sessionDto);
                    if (existsMessage != null)
                    {
                        return Ok(Response<DetailMessage>.Success(existsMessage));
                    }
                }

                var response = await _sessionsService.CreateSessionAndGenerateAppointments(sessionDto);
                return CreatedAtAction(nameof(GetAllSessions), Response<CtaSessionsRequest>.Created(response));
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
        [SwaggerOperation(Summary = "Actualizar una sesión", Description = "Endpoint para actualizar una sesión")]
        public async Task<IActionResult> UpdateSessionSchedule(int id, [FromBody] CtaSessionsRequest sessionDto)
        {
            try
            {
                var validationResult = await _validator.ValidateAsync(sessionDto);
                if (!validationResult.IsValid)
                {
                    var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                    return BadRequest(Response<string>.BadRequest(errors, 400));
                }

                var existingSession = await _sessionsService.GetByIdRequest(id);
                if (existingSession == null)
                    return NotFound(Response<string>.NotFound("Sesión no encontrada."));

                sessionDto.IdSession = id;

                var updatedSession = await _sessionsService.UpdateSessionAndAppointments(sessionDto, id);

                return Ok(Response<CtaSessionsRequest>.Success(updatedSession, "Sesión actualizada correctamente."));
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
        [SwaggerOperation(Summary = "Eliminar una sesión", Description = "Endpoint para eliminar una sesión")]
        public async Task<IActionResult> DeleteSession(int id)
        {
            try
            {
                var existingSession = await _sessionsService.GetByIdRequest(id);
                if (existingSession == null)
                    return NotFound(Response<string>.NotFound("Sesión no encontrada."));

                await _sessionsService.Delete(id);
                return Ok(Response<string>.Success(null, "Sesión eliminada correctamente."));
            }
            catch (Exception ex)
            {
                return StatusCode(500, Response<string>.ServerError(ex.Message));
            }
        }
    }


}
