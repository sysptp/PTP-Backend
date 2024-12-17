using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using Swashbuckle.AspNetCore.Annotations;
using BussinessLayer.Wrappers;
using Microsoft.AspNetCore.Authorization;
using FluentValidation;
using BussinessLayer.Atributes;
using BussinessLayer.Interfaces.ModuloHelpDesk;
using BussinessLayer.DTOs.ModuloHelpDesk;

namespace PTP_API.Controllers.ModuloHelpDesk
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [SwaggerTag("Gestión de Tipo de Ticket")]
    [Authorize]
    [EnableBitacora]
    public class TypeTicketController : ControllerBase
    {
        private readonly IHdkTypeTicketService _typeTicketService;
        private readonly IValidator<HdkTypeTicketRequest> _validator;

        public TypeTicketController(IHdkTypeTicketService typeTicketService, IValidator<HdkTypeTicketRequest> validator)
        {
            _typeTicketService = typeTicketService;
            _validator = validator;
        }
        [HttpGet]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(Response<IEnumerable<HdkTypeTicketReponse>>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Obtener Tipo de ticket", Description = "Obtiene una lista de todos los tipos de ticket o un Tipo específico si se proporciona un ID.")]
        [DisableBitacora]
        public async Task<IActionResult> Get([FromQuery] int? id)
        {
            try
            {
                if (id.HasValue)
                {
                    var TypeTicketTicket = await _typeTicketService.GetByIdResponse(id);
                    if (TypeTicketTicket == null)
                    {
                        return NotFound(Response<HdkTypeTicketReponse>.NotFound("Tipo de ticket no encontrado."));
                    }
                    return Ok(Response<List<HdkTypeTicketReponse>>.Success(new List<HdkTypeTicketReponse> { TypeTicketTicket }, "Tipo de ticket encontrado."));
                }
                else
                {
                    var typeTicketTickets = await _typeTicketService.GetAllDto();
                    if (typeTicketTickets == null || typeTicketTickets.Count == 0)
                    {
                        return NoContent();
                    }
                    return Ok(Response<IEnumerable<HdkTypeTicketReponse>>.Success(typeTicketTickets, "Tipo de ticket obtenidos correctamente."));
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, Response<string>.ServerError("Ocurrió un error al obtener el Tipo de tickets. Por favor, intente nuevamente."));
            }
        }

        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Crear un nuevo Tipo de Ticket", Description = "Crea un nuevo Tipo de Ticket en el sistema.")]
        public async Task<IActionResult> Add([FromBody] HdkTypeTicketRequest request)
        {
            var validationResult = await _validator.ValidateAsync(request);

            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return BadRequest(Response<string>.BadRequest(errors, 400));
            }

            try
            {
                var typeTicket = await _typeTicketService.Add(request);
                return StatusCode(201, Response<HdkTypeTicketReponse>.Created(typeTicket, "Tipo de Ticket creado correctamente."));
            }
            catch (Exception ex)
            {
                return StatusCode(500, Response<string>.ServerError("Ocurrió un error al crear un Tipo de Ticket. Por favor, intente nuevamente."));
            }
        }

        [HttpPut("{id}")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Actualizar un Tipo de Ticket", Description = "Actualiza la información de un Tipo de Ticket existente.")]
        public async Task<IActionResult> Update(int id, [FromBody] HdkTypeTicketRequest saveDto)
        {
            var validationResult = await _validator.ValidateAsync(saveDto);

            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return BadRequest(Response<string>.BadRequest(errors, 400));
            }

            try
            {
                var typeTicket = await _typeTicketService.GetByIdResponse(id);
                if (typeTicket == null)
                {
                    return NotFound(Response<HdkTypeTicketReponse>.NotFound("Tipo de Ticket no encontrado."));
                }
                saveDto.IdTipoTicket = id;
                await _typeTicketService.Update(saveDto, id);
                return Ok(Response<string>.Success(null, "Tipo de Ticket actualizado correctamente"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, Response<string>.ServerError("Ocurrió un error al actualizar el Tipo de Ticket. Por favor, intente nuevamente."));
            }
        }
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Eliminar un Tipo de ticket", Description = "Elimina un Tipo de manera lógica.")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var departamentoXusuarioTicket = await _typeTicketService.GetByIdResponse(id);
                if (departamentoXusuarioTicket == null)
                {
                    return NotFound(Response<string>.NotFound("Tipo de Ticket no encontrado."));
                }

                await _typeTicketService.Delete(id);
                return Ok(Response<string>.Success(null, "Tipo de Ticket eliminado correctamente"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, Response<string>.ServerError("Ocurrió un error al eliminar el Tipo de Ticket. Por favor, intente nuevamente."));
            }
        }
    }
}
