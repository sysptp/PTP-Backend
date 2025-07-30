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
    [SwaggerTag("Gestión de áreas de citas")]
    [Route("api/v1/[controller]")]
    [Authorize]
    [EnableBitacora]
    public class CtaAppointmentAreaController : ControllerBase
    {
        private readonly ICtaAppointmentAreaService _appointmentAreaService;
        private readonly IValidator<CtaAppointmentAreaRequest> _validator;

        public CtaAppointmentAreaController(
            ICtaAppointmentAreaService appointmentAreaService,
            IValidator<CtaAppointmentAreaRequest> validator)
        {
            _appointmentAreaService = appointmentAreaService;
            _validator = validator;
        }

        [HttpGet]
        [ProducesResponseType(typeof(Response<IEnumerable<CtaAppointmentAreaResponse>>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Obtener áreas de citas", Description = "Devuelve una lista de áreas de citas o un área específica si se proporciona un ID")]
        [DisableBitacora]
        public async Task<IActionResult> GetAllAreas([FromQuery] int? id, long? companyId)
        {
            try
            {
                if (id.HasValue)
                {
                    var area = await _appointmentAreaService.GetByIdResponse(id.Value);
                    if (area == null)
                        return NotFound(Response<CtaAppointmentAreaResponse>.NotFound("Área no encontrada."));

                    return Ok(Response<CtaAppointmentAreaResponse>.Success(area, "Área encontrada."));
                }

                var areas = await _appointmentAreaService.GetAllDto();
                if (areas == null || !areas.Any())
                    return StatusCode(204, Response<IEnumerable<CtaAppointmentAreaResponse>>.NoContent("No hay áreas disponibles."));

                return Ok(Response<IEnumerable<CtaAppointmentAreaResponse>>.Success(
                   companyId != null ? areas.Where(x => x.CompanyId == companyId) : areas, "Áreas obtenidas correctamente."));
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
        [SwaggerOperation(Summary = "Crear una nueva área de citas", Description = "Endpoint para registrar una nueva área de citas")]
        public async Task<IActionResult> CreateArea([FromBody] CtaAppointmentAreaRequest areaRequest)
        {
            try
            {
                var validationResult = await _validator.ValidateAsync(areaRequest);

                if (!validationResult.IsValid)
                {
                    var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                    return BadRequest(Response<string>.BadRequest(errors, 400));
                }

                var response = await _appointmentAreaService.Add(areaRequest);
                return CreatedAtAction(nameof(GetAllAreas), Response<CtaAppointmentAreaResponse>.Created(response));
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
        [SwaggerOperation(Summary = "Actualizar un área de citas", Description = "Endpoint para actualizar un área de citas")]
        public async Task<IActionResult> UpdateArea(int id, [FromBody] CtaAppointmentAreaRequest areaRequest)
        {
            try
            {
                var validationResult = await _validator.ValidateAsync(areaRequest);

                if (!validationResult.IsValid)
                {
                    var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                    return BadRequest(Response<string>.BadRequest(errors, 400));
                }

                var existingArea = await _appointmentAreaService.GetByIdRequest(id);
                if (existingArea == null)
                    return NotFound(Response<string>.NotFound("Área no encontrada."));

                areaRequest.AreaId = id;
                await _appointmentAreaService.Update(areaRequest, id);
                return Ok(Response<string>.Success(null, "Área actualizada correctamente."));
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
        [SwaggerOperation(Summary = "Eliminar un área de citas", Description = "Endpoint para eliminar un área de citas")]
        public async Task<IActionResult> DeleteArea(int id)
        {
            try
            {
                var existingArea = await _appointmentAreaService.GetByIdRequest(id);
                if (existingArea == null)
                    return NotFound(Response<string>.NotFound("Área no encontrada."));

                await _appointmentAreaService.Delete(id);
                return Ok(Response<string>.Success(null, "Área eliminada correctamente."));
            }
            catch (Exception ex)
            {
                return StatusCode(500, Response<string>.ServerError(ex.Message));
            }
        }
    }
}