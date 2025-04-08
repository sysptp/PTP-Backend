using BussinessLayer.Atributes;
using BussinessLayer.DTOs.ModuloCitas.CtaSessions;
using BussinessLayer.Interfaces.Services.ModuloCitas;
using BussinessLayer.Wrappers;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net.Mime;

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
        [ProducesResponseType(typeof(Response<IEnumerable<CtaSessionsResponse>>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Obtener sesiones", Description = "Devuelve una lista de sesiones o una sesión específica si se proporciona un ID")]
        [DisableBitacora]
        public async Task<IActionResult> GetAllSessions([FromQuery] int? IdSession, long? companyId, int? userId)
        {
            try
            {
                if (IdSession.HasValue)
                {
                    var session = await _sessionsService.GetByIdResponse(IdSession.Value);
                    if (session == null)
                        return NotFound(Response<CtaSessionsResponse>.NotFound("Sesión no encontrada."));

                    return Ok(Response<CtaSessionsResponse>.Success(session, "Sesión encontrada."));
                }
                else
                {
                    var sessions = await _sessionsService.GetAllDto();
                    if (sessions == null || !sessions.Any())
                        return StatusCode(204, Response<IEnumerable<CtaSessionsResponse>>.NoContent("No hay sesiones disponibles."));

                    var filteredSessions = sessions.AsQueryable();

                    if (companyId.HasValue)
                        filteredSessions = filteredSessions.Where(x => x.CompanyId == companyId.Value);

                    if (userId.HasValue)
                        filteredSessions = filteredSessions.Where(x => x.IdUser == userId.Value);

                    return Ok(Response<IEnumerable<CtaSessionsResponse>>.Success(
                        filteredSessions.ToList(),
                        "Sesiones obtenidas correctamente."));
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
        public async Task<IActionResult> UpdateSession(int id, [FromBody] CtaSessionsRequest sessionDto)
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
                await _sessionsService.Update(sessionDto, id);
                return Ok(Response<string>.Success(null, "Sesión actualizada correctamente."));
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
