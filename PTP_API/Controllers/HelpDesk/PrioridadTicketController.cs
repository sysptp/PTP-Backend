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
    [SwaggerTag("Gestión de Prioridad de Ticket")]
    [Authorize]
    public class PrioridadTicketController : ControllerBase
    {
        private readonly IHdkPrioridadTicketService _prioridadTicketService;
        private readonly IValidator<HdkPrioridadTicketRequest> _validator;

        public PrioridadTicketController(IHdkPrioridadTicketService prioridadTicketService, IValidator<HdkPrioridadTicketRequest> validator)
        {
            _prioridadTicketService = prioridadTicketService;
            _validator = validator;
        }

        [HttpGet]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(Response<IEnumerable<HdkPrioridadTicketReponse>>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Obtener Prioridad de ticket", Description = "Obtiene una lista de todos las prioridades de ticket o un Prioridad específico si se proporciona un ID.")]
        public async Task<IActionResult> Get([FromQuery] int? id)
        {
            try
            {
                if (id.HasValue)
                {
                    var prioridadTicketTicket = await _prioridadTicketService.GetByIdResponse(id);
                    if (prioridadTicketTicket == null)
                    {
                        return NotFound(Response<HdkPrioridadTicketReponse>.NotFound("Prioridad no encontrado."));
                    }
                    return Ok(Response<List<HdkPrioridadTicketReponse>>.Success(new List<HdkPrioridadTicketReponse> { prioridadTicketTicket }, "Prioridad de ticket encontrada."));
                }
                else
                {
                    var prioridadTicketTickets = await _prioridadTicketService.GetAllDto();
                    if (prioridadTicketTickets == null || prioridadTicketTickets.Count == 0)
                    {
                        return NoContent();
                    }
                    return Ok(Response<IEnumerable<HdkPrioridadTicketReponse>>.Success(prioridadTicketTickets, "Prioridades de ticket obtenidas correctamente."));
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, Response<string>.ServerError("Ocurrió un error al obtener el Prioridad de tickets. Por favor, intente nuevamente."));
            }
        }

        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Crear un nueva Prioridad de Ticket", Description = "Crea una nueva Prioridad de Ticket en el sistema.")]
        public async Task<IActionResult> Add([FromBody] HdkPrioridadTicketRequest request)
        {
            var validationResult = await _validator.ValidateAsync(request);

            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return BadRequest(Response<string>.BadRequest(errors, 400));
            }

            try
            {
                var prioridadTicket = await _prioridadTicketService.Add(request);
                return StatusCode(201, Response<HdkPrioridadTicketReponse>.Created(prioridadTicket, "Prioridad de Ticket creada correctamente."));
            }
            catch (Exception ex)
            {
                return StatusCode(500, Response<string>.ServerError("Ocurrió un error al crear una Prioridad de Ticket. Por favor, intente nuevamente."));
            }
        }

        [HttpPut("{id}")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Actualizar una Prioridad de Ticket", Description = "Actualiza la información de una Prioridad de Ticket existente.")]
        public async Task<IActionResult> Update(int id, [FromBody] HdkPrioridadTicketRequest saveDto)
        {
            var validationResult = await _validator.ValidateAsync(saveDto);

            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return BadRequest(Response<string>.BadRequest(errors, 400));
            }

            try
            {
                var prioridadTicket = await _prioridadTicketService.GetByIdResponse(id);
                if (prioridadTicket == null)
                {
                    return NotFound(Response<HdkPrioridadTicketReponse>.NotFound("Prioridad de Ticket no encontrada."));
                }
                saveDto.IdPrioridad = id;
                await _prioridadTicketService.Update(saveDto, id);
                return Ok(Response<string>.Success(null, "Prioridad de Ticket actualizada correctamente"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, Response<string>.ServerError("Ocurrió un error al actualizar la Prioridad de Ticket. Por favor, intente nuevamente."));
            }
        }
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Eliminar una Prioridad de ticket", Description = "Elimina una Prioridad de manera lógica.")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var prioridadTicket = await _prioridadTicketService.GetByIdResponse(id);
                if (prioridadTicket == null)
                {
                    return NotFound(Response<string>.NotFound("Prioridad de Ticket no encontrada."));
                }

                await _prioridadTicketService.Delete(id);
                return Ok(Response<string>.Success(null, "Prioridad de Ticket eliminada correctamente"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, Response<string>.ServerError("Ocurrió un error al eliminar la Prioridad de Ticket. Por favor, intente nuevamente."));
            }
        }
    }
}
