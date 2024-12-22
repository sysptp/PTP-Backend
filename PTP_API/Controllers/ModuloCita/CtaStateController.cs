using BussinessLayer.Atributes;
using BussinessLayer.DTOs.ModuloCitas.CtaState;
using BussinessLayer.Interfaces.Services.ModuloCitas;
using BussinessLayer.Wrappers;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net.Mime;

namespace PTP_API.Controllers.ModuloCitas
{
    [ApiController]
    [SwaggerTag("Gestión de estados de citas")]
    [Route("api/v1/[controller]")]
    [Authorize]
    [EnableBitacora]
    public class CtaStateController : ControllerBase
    {
        private readonly ICtaStateService _stateService;
        private readonly IValidator<CtaStateRequest> _validator;

        public CtaStateController(ICtaStateService stateService, IValidator<CtaStateRequest> validator)
        {
            _stateService = stateService;
            _validator = validator;
        }

        [HttpGet]
        [ProducesResponseType(typeof(Response<IEnumerable<CtaStateResponse>>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Obtener estados", Description = "Devuelve una lista de estados de citas o un estado específico si se proporciona un ID")]
        public async Task<IActionResult> GetAllStates([FromQuery] int? IdStateAppointment)
        {
            try
            {
                if (IdStateAppointment.HasValue)
                {
                    var state = await _stateService.GetByIdResponse(IdStateAppointment.Value);
                    if (state == null)
                        return NotFound(Response<CtaStateResponse>.NotFound("Estado no encontrado."));

                    return Ok(Response<CtaStateResponse>.Success(state, "Estado encontrado."));
                }
                else
                {
                    var states = await _stateService.GetAllDto();
                    if (states == null || !states.Any())
                        return StatusCode(204, Response<IEnumerable<CtaStateResponse>>.NoContent("No hay estados disponibles."));

                    return Ok(Response<IEnumerable<CtaStateResponse>>.Success(states, "Estados obtenidos correctamente."));
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
        [SwaggerOperation(Summary = "Crear un nuevo estado", Description = "Endpoint para registrar un estado de cita")]
        public async Task<IActionResult> CreateState([FromBody] CtaStateRequest stateDto)
        {
            try
            {
                var validationResult = await _validator.ValidateAsync(stateDto);

                if (!validationResult.IsValid)
                {
                    var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                    return BadRequest(Response<string>.BadRequest(errors, 400));
                }

                var response = await _stateService.Add(stateDto);
                return CreatedAtAction(nameof(GetAllStates), response);
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
        [SwaggerOperation(Summary = "Actualizar un estado", Description = "Endpoint para actualizar un estado de cita")]
        public async Task<IActionResult> UpdateState(int id, [FromBody] CtaStateRequest stateDto)
        {
            try
            {
                var validationResult = await _validator.ValidateAsync(stateDto);

                if (!validationResult.IsValid)
                {
                    var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                    return BadRequest(Response<string>.BadRequest(errors, 400));
                }

                var existingState = await _stateService.GetByIdRequest(id);
                if (existingState == null)
                    return NotFound(Response<string>.NotFound("Estado no encontrado."));

                stateDto.IdStateAppointment = id;
                await _stateService.Update(stateDto, id);
                return Ok(Response<string>.Success(null, "Estado actualizado correctamente."));
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
        [SwaggerOperation(Summary = "Eliminar un estado", Description = "Endpoint para eliminar un estado de cita")]
        public async Task<IActionResult> DeleteState(int id)
        {
            try
            {
                var existingState = await _stateService.GetByIdRequest(id);
                if (existingState == null)
                    return NotFound(Response<string>.NotFound("Estado no encontrado."));

                await _stateService.Delete(id);
                return Ok(Response<string>.Success(null, "Estado eliminado correctamente."));
            }
            catch (Exception ex)
            {
                return StatusCode(500, Response<string>.ServerError(ex.Message));
            }
        }
    }
}
