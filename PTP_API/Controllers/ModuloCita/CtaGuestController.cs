using System.Net.Mime;
using BussinessLayer.Atributes;
using BussinessLayer.DTOs.ModuloCitas.CtaGuest;
using BussinessLayer.Interfaces.Services.ModuloCitas;
using BussinessLayer.Wrappers;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace PTP_API.Controllers.ModuloCita
{
    [ApiController]
    [SwaggerTag("Gestión de Invitados de Citas")]
    [Route("api/v1/[controller]")]
    [Authorize]
    [EnableBitacora]
    public class CtaGuestController : ControllerBase
    {
        private readonly ICtaGuestService _guestService;
        private readonly IValidator<CtaGuestRequest> _validator;

        public CtaGuestController(ICtaGuestService guestService, IValidator<CtaGuestRequest> validator)
        {
            _guestService = guestService;
            _validator = validator;
        }

        [HttpGet]
        [ProducesResponseType(typeof(Response<IEnumerable<CtaGuestResponse>>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Obtener Invitados de Citas", Description = "Devuelve una lista de invitados o un invitado específico si se proporciona un ID")]
        [DisableBitacora]
        public async Task<IActionResult> GetAllGuests([FromQuery] int? id, long? companyId)
        {
            try
            {
                if (id.HasValue)
                {
                    var guest = await _guestService.GetByIdResponse(id.Value);
                    if (guest == null)
                        return NotFound(Response<CtaGuestResponse>.NotFound("Invitado no encontrado."));

                    return Ok(Response<CtaGuestResponse>.Success(guest, "Invitado encontrado."));
                }
                else
                {
                    var guests = await _guestService.GetAllDto();
                    if (guests == null || !guests.Any())
                        return StatusCode(204, Response<IEnumerable<CtaGuestResponse>>.NoContent("No hay invitados disponibles."));

                    return Ok(Response<IEnumerable<CtaGuestResponse>>.Success(
                        companyId != null ? guests.Where(x => x.CompanyId == companyId).ToList() : guests, "Invitados obtenidos correctamente."));
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
        [SwaggerOperation(Summary = "Agregar un nuevo invitado", Description = "Endpoint para registrar un invitado")]
        public async Task<IActionResult> CreateGuest([FromBody] CtaGuestRequest guestDto)
        {
            try
            {
                var validationResult = await _validator.ValidateAsync(guestDto);

                if (!validationResult.IsValid)
                {
                    var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                    return BadRequest(Response<string>.BadRequest(errors, 400));
                }

                var response = await _guestService.Add(guestDto);
                return CreatedAtAction(nameof(GetAllGuests), Response<CtaGuestResponse>.Created(response));
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
        [SwaggerOperation(Summary = "Actualizar un invitado", Description = "Endpoint para actualizar un invitado")]
        public async Task<IActionResult> UpdateGuest(int id, [FromBody] CtaGuestRequest guestDto)
        {
            try
            {
                var validationResult = await _validator.ValidateAsync(guestDto);

                if (!validationResult.IsValid)
                {
                    var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                    return BadRequest(Response<string>.BadRequest(errors, 400));
                }

                var existingGuest = await _guestService.GetByIdRequest(id);
                if (existingGuest == null)
                    return NotFound(Response<string>.NotFound("Invitado no encontrado."));

                guestDto.Id = id;
                await _guestService.Update(guestDto, id);
                return Ok(Response<string>.Success(null, "Invitado actualizado correctamente."));
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
        [SwaggerOperation(Summary = "Eliminar un invitado", Description = "Endpoint para eliminar un invitado")]
        public async Task<IActionResult> DeleteGuest(int id)
        {
            try
            {
                var existingGuest = await _guestService.GetByIdRequest(id);
                if (existingGuest == null)
                    return NotFound(Response<string>.NotFound("Invitado no encontrado."));

                await _guestService.Delete(id);
                return Ok(Response<string>.Success(null, "Invitado eliminado correctamente."));
            }
            catch (Exception ex)
            {
                return StatusCode(500, Response<string>.ServerError(ex.Message));
            }
        }
    }
}
