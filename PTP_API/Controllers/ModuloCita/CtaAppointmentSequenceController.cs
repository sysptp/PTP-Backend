using BussinessLayer.Atributes;
using BussinessLayer.DTOs.ModuloCitas;
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
    [SwaggerTag("Gestión de secuencias de citas")]
    [Route("api/v1/[controller]")]
    [Authorize]
    [EnableBitacora]
    public class CtaAppointmentSequenceController : ControllerBase
    {
        private readonly ICtaAppointmentSequenceService _appointmentSequenceService;
        private readonly IValidator<CtaAppointmentSequenceRequest> _validator;

        public CtaAppointmentSequenceController(
            ICtaAppointmentSequenceService appointmentSequenceService,
            IValidator<CtaAppointmentSequenceRequest> validator)
        {
            _appointmentSequenceService = appointmentSequenceService;
            _validator = validator;
        }

        [HttpGet]
        [ProducesResponseType(typeof(Response<IEnumerable<CtaAppointmentSequenceResponse>>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Obtener secuencias de citas", Description = "Devuelve una lista de secuencias de citas o una secuencia específica si se proporciona un ID")]
        [DisableBitacora]
        public async Task<IActionResult> GetAllSequences([FromQuery] int? id, long? companyId)
        {
            try
            {
                if (id.HasValue)
                {
                    var sequence = await _appointmentSequenceService.GetByIdResponse(id.Value);
                    if (sequence == null)
                        return NotFound(Response<CtaAppointmentSequenceResponse>.NotFound("Secuencia no encontrada."));

                    return Ok(Response<CtaAppointmentSequenceResponse>.Success(sequence, "Secuencia encontrada."));
                }

                var sequences = await _appointmentSequenceService.GetAllDto();
                if (sequences == null || !sequences.Any())
                    return StatusCode(204, Response<IEnumerable<CtaAppointmentSequenceResponse>>.NoContent("No hay secuencias disponibles."));

                return Ok(Response<IEnumerable<CtaAppointmentSequenceResponse>>.Success(
                    companyId != null ? sequences.Where(x => x.CompanyId == companyId) : sequences, "Secuencias obtenidas correctamente."));
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
        [SwaggerOperation(Summary = "Crear una nueva secuencia de citas", Description = "Endpoint para registrar una nueva secuencia de citas")]
        public async Task<IActionResult> CreateSequence([FromBody] CtaAppointmentSequenceRequest sequenceRequest)
        {
            try
            {
                var validationResult = await _validator.ValidateAsync(sequenceRequest);

                if (!validationResult.IsValid)
                {
                    var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                    return BadRequest(Response<string>.BadRequest(errors, 400));
                }

                var response = await _appointmentSequenceService.Add(sequenceRequest);
                return CreatedAtAction(nameof(GetAllSequences), Response<CtaAppointmentSequenceResponse>.Created(response));
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
        [SwaggerOperation(Summary = "Actualizar una secuencia de citas", Description = "Endpoint para actualizar una secuencia de citas")]
        public async Task<IActionResult> UpdateSequence(int id, [FromBody] CtaAppointmentSequenceRequest sequenceRequest)
        {
            try
            {
                var validationResult = await _validator.ValidateAsync(sequenceRequest);

                if (!validationResult.IsValid)
                {
                    var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                    return BadRequest(Response<string>.BadRequest(errors, 400));
                }

                var existingSequence = await _appointmentSequenceService.GetByIdRequest(id);
                if (existingSequence == null)
                    return NotFound(Response<string>.NotFound("Secuencia no encontrada."));

                sequenceRequest.Id = id;
                sequenceRequest.LastUsed = existingSequence.LastUsed;
                sequenceRequest.SequenceNumber = existingSequence.SequenceNumber;
                await _appointmentSequenceService.Update(sequenceRequest, id);
                return Ok(Response<string>.Success(null, "Secuencia actualizada correctamente."));
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
        [SwaggerOperation(Summary = "Eliminar una secuencia de citas", Description = "Endpoint para eliminar una secuencia de citas")]
        public async Task<IActionResult> DeleteSequence(int id)
        {
            try
            {
                var existingSequence = await _appointmentSequenceService.GetByIdRequest(id);
                if (existingSequence == null)
                    return NotFound(Response<string>.NotFound("Secuencia no encontrada."));

                await _appointmentSequenceService.Delete(id);
                return Ok(Response<string>.Success(null, "Secuencia eliminada correctamente."));
            }
            catch (Exception ex)
            {
                return StatusCode(500, Response<string>.ServerError(ex.Message));
            }
        }
    }
}
