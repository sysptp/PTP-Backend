using BussinessLayer.Atributes;
using BussinessLayer.DTOs.ModuloCitas.CtaSessionDetails;
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
    [SwaggerTag("Gestión de detalles de sesiones")]
    [Route("api/v1/[controller]")]
    [Authorize]
    [EnableBitacora]
    public class CtaSessionDetailsController : ControllerBase
    {
        private readonly ICtaSessionDetailsService _sessionDetailsService;
        private readonly IValidator<CtaSessionDetailsRequest> _validator;

        public CtaSessionDetailsController(ICtaSessionDetailsService sessionDetailsService, IValidator<CtaSessionDetailsRequest> validator)
        {
            _sessionDetailsService = sessionDetailsService;
            _validator = validator;
        }

        [HttpGet]
        [ProducesResponseType(typeof(Response<IEnumerable<CtaSessionDetailsResponse>>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Obtener detalles de sesiones", Description = "Devuelve una lista de detalles de sesiones o un detalle específico si se proporciona un ID")]
        public async Task<IActionResult> GetAllSessionDetails([FromQuery] int? IdSessionDetail)
        {
            try
            {
                if (IdSessionDetail.HasValue)
                {
                    var detail = await _sessionDetailsService.GetByIdResponse(IdSessionDetail.Value);
                    if (detail == null)
                        return NotFound(Response<CtaSessionDetailsResponse>.NotFound("Detalle de sesión no encontrado."));

                    return Ok(Response<CtaSessionDetailsResponse>.Success(detail, "Detalle de sesión encontrado."));
                }
                else
                {
                    var details = await _sessionDetailsService.GetAllDto();
                    if (details == null || !details.Any())
                        return StatusCode(204, Response<IEnumerable<CtaSessionDetailsResponse>>.NoContent("No hay detalles de sesiones disponibles."));

                    return Ok(Response<IEnumerable<CtaSessionDetailsResponse>>.Success(details, "Detalles de sesiones obtenidos correctamente."));
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
        [SwaggerOperation(Summary = "Crear un nuevo detalle de sesión", Description = "Endpoint para registrar un detalle de sesión")]
        public async Task<IActionResult> CreateSessionDetail([FromBody] CtaSessionDetailsRequest sessionDetailDto)
        {
            try
            {
                var validationResult = await _validator.ValidateAsync(sessionDetailDto);

                if (!validationResult.IsValid)
                {
                    var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                    return BadRequest(Response<string>.BadRequest(errors, 400));
                }

                var response = await _sessionDetailsService.Add(sessionDetailDto);
                return CreatedAtAction(nameof(GetAllSessionDetails), Response<CtaSessionDetailsResponse>.Created(response));
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
        [SwaggerOperation(Summary = "Actualizar un detalle de sesión", Description = "Endpoint para actualizar un detalle de sesión")]
        public async Task<IActionResult> UpdateSessionDetail(int id, [FromBody] CtaSessionDetailsRequest sessionDetailDto)
        {
            try
            {
                var validationResult = await _validator.ValidateAsync(sessionDetailDto);

                if (!validationResult.IsValid)
                {
                    var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                    return BadRequest(Response<string>.BadRequest(errors, 400));
                }

                var existingDetail = await _sessionDetailsService.GetByIdRequest(id);
                if (existingDetail == null)
                    return NotFound(Response<string>.NotFound("Detalle de sesión no encontrado."));

                sessionDetailDto.IdSessionDetail = id;
                await _sessionDetailsService.Update(sessionDetailDto, id);
                return Ok(Response<string>.Success(null, "Detalle de sesión actualizado correctamente."));
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
        [SwaggerOperation(Summary = "Eliminar un detalle de sesión", Description = "Endpoint para eliminar un detalle de sesión")]
        public async Task<IActionResult> DeleteSessionDetail(int id)
        {
            try
            {
                var existingDetail = await _sessionDetailsService.GetByIdRequest(id);
                if (existingDetail == null)
                    return NotFound(Response<string>.NotFound("Detalle de sesión no encontrado."));

                await _sessionDetailsService.Delete(id);
                return Ok(Response<string>.Success(null, "Detalle de sesión eliminado correctamente."));
            }
            catch (Exception ex)
            {
                return StatusCode(500, Response<string>.ServerError(ex.Message));
            }
        }
    }


}
