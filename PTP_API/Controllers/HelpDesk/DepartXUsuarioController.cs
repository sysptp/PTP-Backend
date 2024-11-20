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
    [SwaggerTag("Gestión de DepartXUsuario de Ticket")]
    [Authorize]
    public class DepartXUsuarioController : ControllerBase
    {
        private readonly IHdkDepartXUsuarioService _departXUsuarioService;
        private readonly IValidator<HdkDepartXUsuarioRequest> _validator;

        public DepartXUsuarioController(IHdkDepartXUsuarioService departXUsuarioService, IValidator<HdkDepartXUsuarioRequest> validator)
        {
            _departXUsuarioService = departXUsuarioService;
            _validator = validator;
        }

        [HttpGet]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(Response<IEnumerable<HdkDepartXUsuarioReponse>>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Obtener Departamento por usuario de ticket", Description = "Obtiene una lista de todos los departamento de ticket o un departamento por usuario específico si se proporciona un ID.")]
        public async Task<IActionResult> Get([FromQuery] int? id)
        {
            try
            {
                if (id.HasValue)
                {
                    var departXUsuarioTicket = await _departXUsuarioService.GetByIdResponse(id);
                    if (departXUsuarioTicket == null)
                    {
                        return NotFound(Response<HdkDepartXUsuarioReponse>.NotFound("Departamento por usuario no encontrado."));
                    }
                    return Ok(Response<List<HdkDepartXUsuarioReponse>>.Success(new List<HdkDepartXUsuarioReponse> { departXUsuarioTicket }, "Departamento por usuario de ticket encontrado."));
                }
                else
                {
                    var departXUsuarioTickets = await _departXUsuarioService.GetAllDto();
                    if (departXUsuarioTickets == null || departXUsuarioTickets.Count == 0)
                    {
                        return NoContent();
                    }
                    return Ok(Response<IEnumerable<HdkDepartXUsuarioReponse>>.Success(departXUsuarioTickets, "Departamentos por usuario de ticket obtenidos correctamente."));
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, Response<string>.ServerError("Ocurrió un error al obtener el departamento por usuario de tickets. Por favor, intente nuevamente."));
            }
        }

        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Crear un nuevo Departamento por usuario de Ticket", Description = "Crea un nuevo Departamento por usuario de Ticket en el sistema.")]
        public async Task<IActionResult> Add([FromBody] HdkDepartXUsuarioRequest request)
        {
            var validationResult = await _validator.ValidateAsync(request);

            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return BadRequest(Response<string>.BadRequest(errors, 400));
            }

            try
            {
                var departamentoXusuarioTicket = await _departXUsuarioService.Add(request);
                return StatusCode(201, Response<HdkDepartXUsuarioReponse>.Created(departamentoXusuarioTicket, "Departamento por usuario de Ticket creado correctamente."));
            }
            catch (Exception ex)
            {
                return StatusCode(500, Response<string>.ServerError("Ocurrió un error al crear un Departamento por Usuario de Ticket. Por favor, intente nuevamente."));
            }
        }

        [HttpPut("{id}")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Actualizar un Departamento por usuario de Ticket", Description = "Actualiza la información de un Departamento por usuario de Ticket existente.")]
        public async Task<IActionResult> Update(int id, [FromBody] HdkDepartXUsuarioRequest saveDto)
        {
            var validationResult = await _validator.ValidateAsync(saveDto);

            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return BadRequest(Response<string>.BadRequest(errors, 400));
            }

            try
            {
                var departamentoXUsuarioTicket = await _departXUsuarioService.GetByIdResponse(id);
                if (departamentoXUsuarioTicket == null)
                {
                    return NotFound(Response<HdkDepartXUsuarioReponse>.NotFound("Departamento de Ticket no encontrado."));
                }
                saveDto.IdDepartXUsuario = id;
                await _departXUsuarioService.Update(saveDto, id);
                return Ok(Response<string>.Success(null, "Departamento por usuario de Ticket actualizado correctamente"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, Response<string>.ServerError("Ocurrió un error al actualizar el Departamento por usuario de Ticket. Por favor, intente nuevamente."));
            }
        }
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Eliminar un Departamento por usuario de ticket", Description = "Elimina un Departamento por usuario de manera lógica.")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var departamentoXusuarioTicket = await _departXUsuarioService.GetByIdResponse(id);
                if (departamentoXusuarioTicket == null)
                {
                    return NotFound(Response<string>.NotFound("Departamento por usuario de Ticket no encontrado."));
                }

                await _departXUsuarioService.Delete(id);
                return Ok(Response<string>.Success(null, "Departamento por usuario de Ticket eliminado correctamente"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, Response<string>.ServerError("Ocurrió un error al eliminar el Departamento por usuario de Ticket. Por favor, intente nuevamente."));
            }
        }
    }
}
