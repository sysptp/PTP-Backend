using BussinessLayer.Atributes;
using BussinessLayer.DTOs.ModuloCitas.CtaMeetingPlace;
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
    [SwaggerTag("Gestión de lugares de reunión")]
    [Route("api/v1/[controller]")]
    [Authorize]
    [EnableBitacora]
    public class CtaMeetingPlaceController : ControllerBase
    {
        private readonly ICtaMeetingPlaceService _meetingPlaceService;
        private readonly IValidator<CtaMeetingPlaceRequest> _validator;

        public CtaMeetingPlaceController(ICtaMeetingPlaceService meetingPlaceService, IValidator<CtaMeetingPlaceRequest> validator)
        {
            _meetingPlaceService = meetingPlaceService;
            _validator = validator;
        }

        [HttpGet]
        [ProducesResponseType(typeof(Response<IEnumerable<CtaMeetingPlaceResponse>>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Obtener lugares de reunión", Description = "Devuelve una lista de lugares de reunión o un lugar específico si se proporciona un ID")]
        public async Task<IActionResult> GetAllMeetingPlaces([FromQuery] int? IdMeetingPlace,long? companyId)
        {
            try
            {
                if (IdMeetingPlace.HasValue)
                {
                    var place = await _meetingPlaceService.GetByIdResponse(IdMeetingPlace.Value);
                    if (place == null)
                        return NotFound(Response<CtaMeetingPlaceResponse>.NotFound("Lugar de reunión no encontrado."));

                    return Ok(Response<CtaMeetingPlaceResponse>.Success(place, "Lugar de reunión encontrado."));
                }
                else
                {
                    var places = await _meetingPlaceService.GetAllDto();
                    if (places == null || !places.Any())
                        return StatusCode(204, Response<IEnumerable<CtaMeetingPlaceResponse>>.NoContent("No hay lugares de reunión disponibles."));

                    return Ok(Response<IEnumerable<CtaMeetingPlaceResponse>>.Success(
                        companyId != null ? places.Where(x => x.CompanyId == companyId).ToList() : places, "Lugares de reunión obtenidos correctamente."));
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
        [SwaggerOperation(Summary = "Crear un nuevo lugar de reunión", Description = "Endpoint para registrar un lugar de reunión")]
        public async Task<IActionResult> CreateMeetingPlace([FromBody] CtaMeetingPlaceRequest meetingPlaceDto)
        {
            try
            {
                var validationResult = await _validator.ValidateAsync(meetingPlaceDto);

                if (!validationResult.IsValid)
                {
                    var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                    return BadRequest(Response<string>.BadRequest(errors, 400));
                }

                var response = await _meetingPlaceService.Add(meetingPlaceDto);
                return CreatedAtAction(nameof(GetAllMeetingPlaces), Response<CtaMeetingPlaceResponse>.Created(response));
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
        [SwaggerOperation(Summary = "Actualizar un lugar de reunión", Description = "Endpoint para actualizar un lugar de reunión")]
        public async Task<IActionResult> UpdateMeetingPlace(int id, [FromBody] CtaMeetingPlaceRequest meetingPlaceDto)
        {
            try
            {
                var validationResult = await _validator.ValidateAsync(meetingPlaceDto);

                if (!validationResult.IsValid)
                {
                    var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                    return BadRequest(Response<string>.BadRequest(errors, 400));
                }

                var existingPlace = await _meetingPlaceService.GetByIdRequest(id);
                if (existingPlace == null)
                    return NotFound(Response<string>.NotFound("Lugar de reunión no encontrado."));

                meetingPlaceDto.IdMeetingPlace = id;
                await _meetingPlaceService.Update(meetingPlaceDto, id);
                return Ok(Response<string>.Success(null, "Lugar de reunión actualizado correctamente."));
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
        [SwaggerOperation(Summary = "Eliminar un lugar de reunión", Description = "Endpoint para eliminar un lugar de reunión")]
        public async Task<IActionResult> DeleteMeetingPlace(int id)
        {
            try
            {
                var existingPlace = await _meetingPlaceService.GetByIdRequest(id);
                if (existingPlace == null)
                    return NotFound(Response<string>.NotFound("Lugar de reunión no encontrado."));

                await _meetingPlaceService.Delete(id);
                return Ok(Response<string>.Success(null, "Lugar de reunión eliminado correctamente."));
            }
            catch (Exception ex)
            {
                return StatusCode(500, Response<string>.ServerError(ex.Message));
            }
        }
    }


}
