using BussinessLayer.Atributes;
using BussinessLayer.DTOs.ModuloCitas.CtaAppointmentMovements;
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
    [SwaggerTag("Gestión de movimientos de citas")]
    [Route("api/v1/[controller]")]
    [Authorize]
    [EnableBitacora]
    public class CtaAppointmentMovementsController : ControllerBase
    {
        private readonly ICtaAppointmentMovementsService _movementsService;
        private readonly IValidator<CtaAppointmentMovementsRequest> _validator;

        public CtaAppointmentMovementsController(ICtaAppointmentMovementsService movementsService, IValidator<CtaAppointmentMovementsRequest> validator)
        {
            _movementsService = movementsService;
            _validator = validator;
        }

        [HttpGet]
        [ProducesResponseType(typeof(Response<IEnumerable<CtaAppointmentMovementsResponse>>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Obtener movimientos de citas", Description = "Devuelve una lista de movimientos de citas o un movimiento específico si se proporciona un ID")]
        [DisableBitacora]
        public async Task<IActionResult> GetAllMovements([FromQuery] int? appointmentMovementId)
        {
            try
            {
                if (appointmentMovementId.HasValue)
                {
                    var movement = await _movementsService.GetByIdResponse(appointmentMovementId);
                    if (movement == null)
                        return NotFound(Response<CtaAppointmentMovementsResponse>.NotFound("Movimiento no encontrado."));

                    return Ok(Response<CtaAppointmentMovementsResponse>.Success(movement, "Movimiento encontrado."));
                }
                else
                {
                    var movements = await _movementsService.GetAllDto();

                    if (movements == null || !movements.Any())
                        return StatusCode(204, Response<IEnumerable<CtaAppointmentMovementsResponse>>.NoContent("No hay movimientos disponibles."));

                    return Ok(Response<IEnumerable<CtaAppointmentMovementsResponse>>.Success(movements, "Movimientos obtenidos correctamente."));
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
        [SwaggerOperation(Summary = "Crear un nuevo movimiento de cita", Description = "Endpoint para registrar un movimiento de cita")]
        public async Task<IActionResult> CreateMovement([FromBody] CtaAppointmentMovementsRequest movementDto)
        {
            try
            {
                var validationResult = await _validator.ValidateAsync(movementDto);

                if (!validationResult.IsValid)
                {
                    var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                    return BadRequest(Response<string>.BadRequest(errors, 400));
                }

                var response = await _movementsService.Add(movementDto);

                return CreatedAtAction(nameof(GetAllMovements), Response<CtaAppointmentMovementsResponse>.Created(response));
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
        [SwaggerOperation(Summary = "Actualizar un movimiento de cita", Description = "Endpoint para actualizar un movimiento de cita")]
        public async Task<IActionResult> UpdateMovement(int id, [FromBody] CtaAppointmentMovementsRequest movementDto)
        {
            try
            {
                var validationResult = await _validator.ValidateAsync(movementDto);

                if (!validationResult.IsValid)
                {
                    var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                    return BadRequest(Response<string>.BadRequest(errors, 400));
                }

                var existingMovement = await _movementsService.GetByIdRequest(id);
                if (existingMovement == null)
                    return NotFound(Response<string>.NotFound("Movimiento no encontrado."));

                movementDto.IdMovement = id;
                await _movementsService.Update(movementDto, id);
                return Ok(Response<string>.Success(null, "Movimiento actualizado correctamente."));
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
        [SwaggerOperation(Summary = "Eliminar un movimiento de cita", Description = "Endpoint para eliminar un movimiento de cita")]
        public async Task<IActionResult> DeleteMovement(int id)
        {
            try
            {
                var existingMovement = await _movementsService.GetByIdRequest(id);
                if (existingMovement == null)
                    return NotFound(Response<string>.NotFound("Movimiento no encontrado."));

                await _movementsService.Delete(id);
                return Ok(Response<string>.Success(null, "Movimiento eliminado correctamente."));
            }
            catch (Exception ex)
            {
                return StatusCode(500, Response<string>.ServerError(ex.Message));
            }
        }
    }

}
