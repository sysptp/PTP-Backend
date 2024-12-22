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
    [SwaggerTag("Gestión de Estado de Ticket")]
    [Authorize]
    [EnableBitacora]
    public class StatusTicketController : ControllerBase
    {
        private readonly IHdkStatusTicketService _statusTicketService;
        private readonly IValidator<HdkStatusTicketRequest> _validator;

        public StatusTicketController(IHdkStatusTicketService statusTicketService, IValidator<HdkStatusTicketRequest> validator)
        {
            _statusTicketService = statusTicketService;
            _validator = validator;
        }

        [HttpGet]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(Response<IEnumerable<HdkStatusTicketReponse>>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Obtener Estado de ticket", Description = "Obtiene una lista de todos los Estado de ticket o un Estado específico si se proporciona un ID.")]
        [DisableBitacora]
        public async Task<IActionResult> Get([FromQuery] int? id)
        {
            try
            {
                if (id.HasValue)
                {
                    var statusTicketTicket = await _statusTicketService.GetByIdResponse(id);
                    if (statusTicketTicket == null)
                    {
                        return NotFound(Response<HdkStatusTicketReponse>.NotFound("Estado no encontrado."));
                    }
                    return Ok(Response<List<HdkStatusTicketReponse>>.Success(new List<HdkStatusTicketReponse> { statusTicketTicket }, "Estado de ticket encontrado."));
                }
                else
                {
                    var statusTicketTickets = await _statusTicketService.GetAllDto();
                    if (statusTicketTickets == null || statusTicketTickets.Count == 0)
                    {
                        return NoContent();
                    }
                    return Ok(Response<IEnumerable<HdkStatusTicketReponse>>.Success(statusTicketTickets, "Estado por usuario de ticket obtenidos correctamente."));
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, Response<string>.ServerError("Ocurrió un error al obtener el Estado de tickets. Por favor, intente nuevamente."));
            }
        }

        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Crear un nuevo Estado de Ticket", Description = "Crea un nuevo Estado de Ticket en el sistema.")]
        public async Task<IActionResult> Add([FromBody] HdkStatusTicketRequest request)
        {
            var validationResult = await _validator.ValidateAsync(request);

            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return BadRequest(Response<string>.BadRequest(errors, 400));
            }

            try
            {
                var statusTicket = await _statusTicketService.Add(request);
                return StatusCode(201, Response<HdkStatusTicketReponse>.Created(statusTicket, "Estado de Ticket creado correctamente."));
            }
            catch (Exception ex)
            {
                return StatusCode(500, Response<string>.ServerError("Ocurrió un error al crear un Estado de Ticket. Por favor, intente nuevamente."));
            }
        }

        [HttpPut("{id}")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Actualizar un Estado de Ticket", Description = "Actualiza la información de un Estado de Ticket existente.")]
        public async Task<IActionResult> Update(int id, [FromBody] HdkStatusTicketRequest saveDto)
        {
            var validationResult = await _validator.ValidateAsync(saveDto);

            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return BadRequest(Response<string>.BadRequest(errors, 400));
            }

            try
            {
                var statusTicket = await _statusTicketService.GetByIdResponse(id);
                if (statusTicket == null)
                {
                    return NotFound(Response<HdkStatusTicketReponse>.NotFound("Estado de Ticket no encontrado."));
                }
                saveDto.IdEstado = id;
                await _statusTicketService.Update(saveDto, id);
                return Ok(Response<string>.Success(null, "Estado de Ticket actualizado correctamente"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, Response<string>.ServerError("Ocurrió un error al actualizar el Estado de Ticket. Por favor, intente nuevamente."));
            }
        }
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Eliminar un Estado de ticket", Description = "Elimina un Estado de manera lógica.")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var statusTicket = await _statusTicketService.GetByIdResponse(id);
                if (statusTicket == null)
                {
                    return NotFound(Response<string>.NotFound("Estado de Ticket no encontrado."));
                }

                await _statusTicketService.Delete(id);
                return Ok(Response<string>.Success(null, "Estado de Ticket eliminado correctamente"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, Response<string>.ServerError("Ocurrió un error al eliminar el Estado de Ticket. Por favor, intente nuevamente."));
            }
        }
    }
}
