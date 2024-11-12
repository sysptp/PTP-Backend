using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using Swashbuckle.AspNetCore.Annotations;
using BussinessLayer.Interfaces.IHelpDesk;
using BussinessLayer.Wrappers;
using Microsoft.AspNetCore.Authorization;
using FluentValidation;
using BussinessLayer.DTOs.HelpDesk;

namespace PTP_API.Controllers.HelpDesk
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [SwaggerTag("Gestión de Ticket")]
    [Authorize]
    public class TicketsController : ControllerBase
    {
        private readonly IHdkTicketsService _ticketsService;
        private readonly IValidator<HdkTicketsRequest> _validator;

        public TicketsController(IHdkTicketsService ticketsService, IValidator<HdkTicketsRequest> validator)
        {
            _ticketsService = ticketsService;
            _validator = validator;
        }

        [HttpGet]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(Response<IEnumerable<HdkTicketsReponse>>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Obtener ticket", Description = "Obtiene una de ticket o ticket específico si se proporciona un ID.")]
        public async Task<IActionResult> Get([FromQuery] int? id)
        {
            try
            {
                if (id.HasValue)
                {
                    var ticketsTicket = await _ticketsService.GetByIdResponse(id);
                    if (ticketsTicket == null)
                    {
                        return NotFound(Response<HdkTicketsReponse>.NotFound("Ticket no encontrado."));
                    }
                    return Ok(Response<List<HdkTicketsReponse>>.Success(new List<HdkTicketsReponse> { ticketsTicket }, " ticket encontrado."));
                }
                else
                {
                    var TicketsTickets = await _ticketsService.GetAllDto();
                    if (TicketsTickets == null || TicketsTickets.Count == 0)
                    {
                        return NoContent();
                    }
                    return Ok(Response<IEnumerable<HdkTicketsReponse>>.Success(TicketsTickets, "Ticket obtenidos correctamente."));
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, Response<string>.ServerError("Ocurrió un error al obtener el  tickets. Por favor, intente nuevamente."));
            }
        }

        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Crear un nuevo Ticket", Description = "Crea un nuevo Ticket en el sistema.")]
        public async Task<IActionResult> Add([FromBody] HdkTicketsRequest request)
        {
            var validationResult = await _validator.ValidateAsync(request);

            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return BadRequest(Response<string>.BadRequest(errors, 400));
            }

            try
            {
                var departamentoXusuarioTicket = await _ticketsService.Add(request);
                return StatusCode(201, Response<HdkTicketsReponse>.Created(departamentoXusuarioTicket, "Ticket creado correctamente."));
            }
            catch (Exception ex)
            {
                return StatusCode(500, Response<string>.ServerError("Ocurrió un error al crear un Ticket. Por favor, intente nuevamente."));
            }
        }

        [HttpPut("{id}")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Actualizar un Ticket", Description = "Actualiza la información de un Ticket existente.")]
        public async Task<IActionResult> Update(int id, [FromBody] HdkTicketsRequest saveDto)
        {
            var validationResult = await _validator.ValidateAsync(saveDto);

            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return BadRequest(Response<string>.BadRequest(errors, 400));
            }

            try
            {
                var departamentoXUsuarioTicket = await _ticketsService.GetByIdResponse(id);
                if (departamentoXUsuarioTicket == null)
                {
                    return NotFound(Response<HdkTicketsReponse>.NotFound("Departamento de Ticket no encontrado."));
                }
                saveDto.IdTicket = id;
                await _ticketsService.Update(saveDto, id);
                return Ok(Response<string>.Success(null, " Ticket actualizado correctamente"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, Response<string>.ServerError("Ocurrió un error al actualizar el Ticket. Por favor, intente nuevamente."));
            }
        }
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Eliminar un ticket", Description = "Elimina un ticket manera lógica.")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var departamentoXusuarioTicket = await _ticketsService.GetByIdResponse(id);
                if (departamentoXusuarioTicket == null)
                {
                    return NotFound(Response<string>.NotFound("Ticket no encontrado."));
                }

                await _ticketsService.Delete(id);
                return Ok(Response<string>.Success(null, "Ticket eliminado correctamente"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, Response<string>.ServerError("Ocurrió un error al eliminar el Ticket. Por favor, intente nuevamente."));
            }
        }
    }
}
