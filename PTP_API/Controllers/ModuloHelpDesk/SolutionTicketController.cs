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
    [SwaggerTag("Gestión de Solución de Ticket")]
    [Authorize]
    [EnableBitacora]
    public class SolutionTicketController : ControllerBase
    {
        private readonly IHdkSolutionTicketService _solutionTicketService;
        private readonly IValidator<HdkSolutionTicketRequest> _validator;

        public SolutionTicketController(IHdkSolutionTicketService solutionTicketService, IValidator<HdkSolutionTicketRequest> validator)
        {
            _solutionTicketService = solutionTicketService;
            _validator = validator;
        }

        [HttpGet]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(Response<IEnumerable<HdkSolutionTicketReponse>>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Obtener Solución de Ticket", Description = "Obtiene una lista de todos los departamento de ticket o un Solucion específico si se proporciona un ID.")]
        [DisableBitacora]
        public async Task<IActionResult> Get([FromQuery] int? id)
        {
            try
            {
                if (id.HasValue)
                {
                    var solutionTicketTicket = await _solutionTicketService.GetByIdResponse(id);
                    if (solutionTicketTicket == null)
                    {
                        return NotFound(Response<HdkSolutionTicketReponse>.NotFound("Solucion no encontrado."));
                    }
                    return Ok(Response<List<HdkSolutionTicketReponse>>.Success(new List<HdkSolutionTicketReponse> { solutionTicketTicket }, "Solución de Ticket encontrado."));
                }
                else
                {
                    var solutionTicketTickets = await _solutionTicketService.GetAllDto();
                    if (solutionTicketTickets == null || solutionTicketTickets.Count == 0)
                    {
                        return NoContent();
                    }
                    return Ok(Response<IEnumerable<HdkSolutionTicketReponse>>.Success(solutionTicketTickets, "Solución de Ticket obtenidos correctamente."));
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, Response<string>.ServerError("Ocurrió un error al obtener la Solución de Tickets. Por favor, intente nuevamente."));
            }
        }

        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Crear un nueva Solución de Ticket", Description = "Crea un nueva Solución de Ticket en el sistema.")]
        public async Task<IActionResult> Add([FromBody] HdkSolutionTicketRequest request)
        {
            var validationResult = await _validator.ValidateAsync(request);

            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return BadRequest(Response<string>.BadRequest(errors, 400));
            }

            try
            {
                var solucuionTicket = await _solutionTicketService.Add(request);
                return StatusCode(201, Response<HdkSolutionTicketReponse>.Created(solucuionTicket, "Solución de Ticket creada correctamente."));
            }
            catch (Exception ex)
            {
                return StatusCode(500, Response<string>.ServerError("Ocurrió un error al crear una Solución de Ticket. Por favor, intente nuevamente."));
            }
        }

        [HttpPut("{id}")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Actualizar una Solución de Ticket", Description = "Actualiza la información de una Solución de Ticket existente.")]
        public async Task<IActionResult> Update(int id, [FromBody] HdkSolutionTicketRequest saveDto)
        {
            var validationResult = await _validator.ValidateAsync(saveDto);

            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return BadRequest(Response<string>.BadRequest(errors, 400));
            }

            try
            {
                var solucionTicket = await _solutionTicketService.GetByIdResponse(id);
                if (solucionTicket == null)
                {
                    return NotFound(Response<HdkSolutionTicketReponse>.NotFound("Solución de Ticket no encontrado."));
                }
                saveDto.IdSolution = id;
                await _solutionTicketService.Update(saveDto, id);
                return Ok(Response<string>.Success(null, "Solución de Ticket actualizado correctamente"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, Response<string>.ServerError("Ocurrió un error al actualizar el Solución de Ticket. Por favor, intente nuevamente."));
            }
        }
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Eliminar un Solución de Ticket", Description = "Elimina un Solución de manera lógica.")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var departamentoXusuarioTicket = await _solutionTicketService.GetByIdResponse(id);
                if (departamentoXusuarioTicket == null)
                {
                    return NotFound(Response<string>.NotFound("Solución de Ticket no encontrado."));
                }

                await _solutionTicketService.Delete(id);
                return Ok(Response<string>.Success(null, "Solución de Ticket eliminado correctamente"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, Response<string>.ServerError("Ocurrió un error al eliminar el Solución de Ticket. Por favor, intente nuevamente."));
            }
        }
    }
}
