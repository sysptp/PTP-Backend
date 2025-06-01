using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using Swashbuckle.AspNetCore.Annotations;
using BussinessLayer.Wrappers;
using Microsoft.AspNetCore.Authorization;
using FluentValidation;
using BussinessLayer.Atributes;
using BussinessLayer.DTOs.ModuloHelpDesk;
using BussinessLayer.Interfaces.Services.ModuloHelpDesk;

namespace PTP_API.Controllers.ModuloHelpDesk
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [SwaggerTag("Gestión de Notas de Ticket")]
    [Authorize]
    [EnableBitacora]
    public class HdkNoteTicketController : ControllerBase
    {
        private readonly IHdkNoteTicketService _noteTicketService;
        private readonly IValidator<HdkNoteTicketRequest> _validator;

        public HdkNoteTicketController(IHdkNoteTicketService noteTicketService, IValidator<HdkNoteTicketRequest> validator)
        {
            _noteTicketService = noteTicketService;
            _validator = validator;
        }
        [HttpGet]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(Response<IEnumerable<HdkNoteTicketReponse>>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Obtener Notas de ticket", Description = "Obtiene una lista de todos los departamento de ticket o un Notas específico si se proporciona un ID.")]
        [DisableBitacora]
        public async Task<IActionResult> Get([FromQuery] int? id)
        {
            try
            {
                if (id.HasValue)
                {
                    var noteTicketTicket = await _noteTicketService.GetByIdResponse(id);
                    if (noteTicketTicket == null)
                    {
                        return NotFound(Response<HdkNoteTicketReponse>.NotFound("Notas no encontrado."));
                    }
                    return Ok(Response<List<HdkNoteTicketReponse>>.Success(new List<HdkNoteTicketReponse> { noteTicketTicket }, "Notas de ticket encontrado."));
                }
                else
                {
                    var noteTicketTickets = await _noteTicketService.GetAllDto();
                    if (noteTicketTickets == null || noteTicketTickets.Count == 0)
                    {
                        return NoContent();
                    }
                    return Ok(Response<IEnumerable<HdkNoteTicketReponse>>.Success(noteTicketTickets, "Notas de ticket obtenidas correctamente."));
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, Response<string>.ServerError("Ocurrió un error al obtener el Notas de tickets. Por favor, intente nuevamente."));
            }
        }

        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Crear un nueva Nota de Ticket", Description = "Crea un nuevo Nota de Ticket en el sistema.")]
        public async Task<IActionResult> Add([FromBody] HdkNoteTicketRequest request)
        {
            var validationResult = await _validator.ValidateAsync(request);

            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return BadRequest(Response<string>.BadRequest(errors, 400));
            }

            try
            {
                var noteTicket = await _noteTicketService.Add(request);
                return StatusCode(201, Response<HdkNoteTicketReponse>.Created(noteTicket, "Nota de Ticket creado correctamente."));
            }
            catch (Exception ex)
            {
                return StatusCode(500, Response<string>.ServerError("Ocurrió un error al crear un Nota de Ticket. Por favor, intente nuevamente."));
            }
        }

        [HttpPut("{id}")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Actualizar una Nota de Ticket", Description = "Actualiza la información de una Notaa de Ticket existente.")]
        public async Task<IActionResult> Update(int id, [FromBody] HdkNoteTicketRequest saveDto)
        {
            var validationResult = await _validator.ValidateAsync(saveDto);

            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return BadRequest(Response<string>.BadRequest(errors, 400));
            }

            try
            {
                var noteTicket = await _noteTicketService.GetByIdResponse(id);
                if (noteTicket == null)
                {
                    return NotFound(Response<HdkNoteTicketReponse>.NotFound("Nota de Ticket no encontrado."));
                }
                saveDto.IdNota = id;
                await _noteTicketService.Update(saveDto, id);
                return Ok(Response<string>.Success(null, "Nota de Ticket actualizado correctamente"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, Response<string>.ServerError("Ocurrió un error al actualizar la Nota de Ticket. Por favor, intente nuevamente."));
            }
        }
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Eliminar una Nota de ticket", Description = "Elimina una Nota de manera lógica.")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var noteTicket = await _noteTicketService.GetByIdResponse(id);
                if (noteTicket == null)
                {
                    return NotFound(Response<string>.NotFound("Notas de Ticket no encontrado."));
                }

                await _noteTicketService.Delete(id);
                return Ok(Response<string>.Success(null, "Notas de Ticket eliminado correctamente"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, Response<string>.ServerError("Ocurrió un error al eliminar la Nota de Ticket. Por favor, intente nuevamente."));
            }
        }
    }
}
