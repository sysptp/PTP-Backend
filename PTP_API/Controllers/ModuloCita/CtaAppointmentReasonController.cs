using BussinessLayer.Atributes;
using BussinessLayer.DTOs.ModuloCitas.CtaAppointmentReason;
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
    [SwaggerTag("Gestión de razones de citas")]
    [Route("api/v1/[controller]")]
    [Authorize]
    [EnableBitacora]
    public class CtaAppointmentReasonController : ControllerBase
    {
        private readonly ICtaAppointmentReasonService _reasonService;
        private readonly IValidator<CtaAppointmentReasonRequest> _validator;

        public CtaAppointmentReasonController(ICtaAppointmentReasonService reasonService, IValidator<CtaAppointmentReasonRequest> validator)
        {
            _reasonService = reasonService;
            _validator = validator;
        }

        [HttpGet]
        [ProducesResponseType(typeof(Response<IEnumerable<CtaAppointmentReasonResponse>>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Obtener razones de citas", Description = "Devuelve una lista de razones de citas o una razón específica si se proporciona un ID")]
        public async Task<IActionResult> GetAllReasons([FromQuery] int? IdReason,long? companyId)
        {
            try
            {
                if (IdReason.HasValue)
                {
                    var reason = await _reasonService.GetByIdResponse(IdReason);
                    if (reason == null)
                        return NotFound(Response<CtaAppointmentReasonResponse>.NotFound("Razón no encontrada."));

                    return Ok(Response<CtaAppointmentReasonResponse>.Success(reason, "Razón encontrada."));
                }
                else
                {
                    var reasons = await _reasonService.GetAllDto();
                    if (reasons == null || !reasons.Any())
                        return StatusCode(204, Response<IEnumerable<CtaAppointmentReasonResponse>>.NoContent("No hay razones disponibles."));

                    return Ok(Response<IEnumerable<CtaAppointmentReasonResponse>>.Success(
                        companyId != null ? reasons.Where(x => x.CompanyId == companyId).ToList() : reasons, "Razones obtenidas correctamente."));
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
        [SwaggerOperation(Summary = "Crear una nueva razón de cita", Description = "Endpoint para registrar una razón de cita")]
        public async Task<IActionResult> CreateReason([FromBody] CtaAppointmentReasonRequest reasonDto)
        {
            try
            {
                var validationResult = await _validator.ValidateAsync(reasonDto);

                if (!validationResult.IsValid)
                {
                    var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                    return BadRequest(Response<string>.BadRequest(errors, 400));
                }

                var response = await _reasonService.Add(reasonDto);
                return CreatedAtAction(nameof(GetAllReasons), Response<CtaAppointmentReasonResponse>.Created(response));
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
        [SwaggerOperation(Summary = "Actualizar una razón de cita", Description = "Endpoint para actualizar una razón de cita")]
        public async Task<IActionResult> UpdateReason(int id, [FromBody] CtaAppointmentReasonRequest reasonDto)
        {
            try
            {
                var validationResult = await _validator.ValidateAsync(reasonDto);

                if (!validationResult.IsValid)
                {
                    var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                    return BadRequest(Response<string>.BadRequest(errors, 400));
                }

                var existingReason = await _reasonService.GetByIdRequest(id);
                if (existingReason == null)
                    return NotFound(Response<string>.NotFound("Razón no encontrada."));

                reasonDto.IdReason = id;
                await _reasonService.Update(reasonDto, id);
                return Ok(Response<string>.Success(null, "Razón actualizada correctamente."));
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
        [SwaggerOperation(Summary = "Eliminar una razón de cita", Description = "Endpoint para eliminar una razón de cita")]
        public async Task<IActionResult> DeleteReason(int id)
        {
            try
            {
                var existingReason = await _reasonService.GetByIdRequest(id);
                if (existingReason == null)
                    return NotFound(Response<string>.NotFound("Razón no encontrada."));

                await _reasonService.Delete(id);
                return Ok(Response<string>.Success(null, "Razón eliminada correctamente."));
            }
            catch (Exception ex)
            {
                return StatusCode(500, Response<string>.ServerError(ex.Message));
            }
        }
    }
}
