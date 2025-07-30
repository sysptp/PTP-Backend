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
    [SwaggerTag("Gestión de Departamentos de Ticket")]
    [Authorize]
    [EnableBitacora]
    public class HdkDepartamentsController : ControllerBase
    {
        private readonly IHdkDepartamentsService _departamentsService;
        private readonly IValidator<HdkDepartamentsRequest> _validator;

        public HdkDepartamentsController(IHdkDepartamentsService departamentsService, IValidator<HdkDepartamentsRequest> validator)
        {
            _departamentsService = departamentsService;
            _validator = validator;
        }
        [HttpGet]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(Response<IEnumerable<HdkDepartamentsReponse>>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Obtener Departamento de ticket", Description = "Obtiene una lista de todos los departamento de ticket o un departamento específico si se proporciona un ID.")]
        [DisableBitacora]
        public async Task<IActionResult> Get([FromQuery] int? id, long? companyId)
        {
            try
            {
                if (id.HasValue)
                {
                    var departamentoTicket = await _departamentsService.GetByIdResponse(id);
                    if (departamentoTicket == null)
                    {
                        return NotFound(Response<HdkDepartamentsReponse>.NotFound("Departamento no encontrado."));
                    }
                    return Ok(Response<List<HdkDepartamentsReponse>>.Success(new List<HdkDepartamentsReponse> { departamentoTicket }, "Departamento de ticket encontrado."));
                }
                else
                {
                    var departamentoTickets = await _departamentsService.GetAllDto();
                    if (departamentoTickets == null || departamentoTickets.Count == 0)
                    {
                        return NoContent();
                    }
                    return Ok(Response<IEnumerable<HdkDepartamentsReponse>>.Success(
                        companyId.HasValue ? departamentoTickets.Where(x => x.IdEmpresa == companyId) : departamentoTickets, "Departamentos de ticket obtenidos correctamente."));
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, Response<string>.ServerError("Ocurrió un error al obtener el departamento de tickets. Por favor, intente nuevamente."));
            }
        }

        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Crear un nuevo Departamento de Ticket", Description = "Crea un nuevo Departamento de Ticket en el sistema.")]
        public async Task<IActionResult> Add([FromBody] HdkDepartamentsRequest request)
        {
            var validationResult = await _validator.ValidateAsync(request);

            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return BadRequest(Response<string>.BadRequest(errors, 400));
            }

            try
            {
                var departamentoTicket = await _departamentsService.Add(request);
                return StatusCode(201, Response<HdkDepartamentsReponse>.Created(departamentoTicket, "Departamento de Ticket creado correctamente."));
            }
            catch (Exception ex)
            {
                return StatusCode(500, Response<string>.ServerError("Ocurrió un error al crear un Departamento de Ticket. Por favor, intente nuevamente."));
            }
        }

        [HttpPut("{id}")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Actualizar un Departamento de Ticket", Description = "Actualiza la información de un Departamento de Ticket existente.")]
        public async Task<IActionResult> Update(int id, [FromBody] HdkDepartamentsRequest saveDto)
        {
            var validationResult = await _validator.ValidateAsync(saveDto);

            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return BadRequest(Response<string>.BadRequest(errors, 400));
            }

            try
            {
                var departamentoTicket = await _departamentsService.GetByIdResponse(id);
                if (departamentoTicket == null)
                {
                    return NotFound(Response<HdkDepartamentsReponse>.NotFound("Departamento de Ticket no encontrado."));
                }
                saveDto.IdDepartamentos = id;
                await _departamentsService.Update(saveDto, id);
                return Ok(Response<string>.Success(null, "Departamento de Ticket actualizado correctamente"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, Response<string>.ServerError("Ocurrió un error al actualizar el Departamento de Ticket. Por favor, intente nuevamente."));
            }
        }
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Eliminar un Departamento de ticket", Description = "Elimina un Departamento de manera lógica.")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var departamentoTicket = await _departamentsService.GetByIdResponse(id);
                if (departamentoTicket == null)
                {
                    return NotFound(Response<string>.NotFound("Departamento de Ticket no encontrado."));
                }

                await _departamentsService.Delete(id);
                return Ok(Response<string>.Success(null, "Departamento de Ticket eliminado correctamente"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, Response<string>.ServerError("Ocurrió un error al eliminar el Departamento de Ticket. Por favor, intente nuevamente."));
            }
        }

    }
}
