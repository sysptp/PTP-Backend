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
    [SwaggerTag("Gestión de Categoria del Ticket")]
    [Authorize]
    public class CatetgoryTickeController : ControllerBase
    {
        private readonly IHdkCategoryTicketService _categoryTicketService;
         private readonly IValidator<HdkCategoryTicketRequest> _validator;

        public CatetgoryTickeController(IHdkCategoryTicketService categoryTicketService, IValidator<HdkCategoryTicketRequest> validator)
        {
            _categoryTicketService = categoryTicketService;
            _validator = validator;
        }
        [HttpGet]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(Response<IEnumerable<HdkCategoryTicketReponse>>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Obtener categoria de ticket", Description = "Obtiene una lista de todas las categoria de ticket o una categoria específica si se proporciona un ID.")]
        public async Task<IActionResult> Get([FromQuery] int? id)
        {
            try
            {
                if (id.HasValue)
                {
                    var categoryTicket = await _categoryTicketService.GetByIdResponse(id);
                    if (categoryTicket == null)
                    {
                        return NotFound(Response<HdkCategoryTicketReponse>.NotFound("Categoria no encontrada."));
                    }
                    return Ok(Response<List<HdkCategoryTicketReponse>>.Success(new List<HdkCategoryTicketReponse> { categoryTicket }, "Categoria de ticket encontrada."));
                }
                else
                {
                    var categoryTickets = await _categoryTicketService.GetAllDto();
                    if (categoryTickets == null || categoryTickets.Count == 0)
                    {
                        return NoContent();
                    }
                    return Ok(Response<IEnumerable<HdkCategoryTicketReponse>>.Success(categoryTickets, "Categoria de ticket obtenidas correctamente."));
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, Response<string>.ServerError("Ocurrió un error al obtener las categorias de tickets. Por favor, intente nuevamente."));
            }
        }

        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Crear una nueva Categoria de Ticket", Description = "Crea una nueva Categoria de Ticket en el sistema.")]
        public async Task<IActionResult> Add([FromBody] HdkCategoryTicketRequest request)
        {
            var validationResult = await _validator.ValidateAsync(request);

            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return BadRequest(Response<string>.BadRequest(errors, 400));
            }

            try
            {
                var categoryTicket = await _categoryTicketService.Add(request);
                return StatusCode(201, Response<HdkCategoryTicketReponse>.Created(categoryTicket, "Categoria del Ticket creada correctamente."));
            }
            catch (Exception ex)
            {
                return StatusCode(500, Response<string>.ServerError("Ocurrió un error al crear la Categoria del Ticket. Por favor, intente nuevamente."));
            }
        }

        [HttpPut("{id}")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Actualizar una Categoria Ticket", Description = "Actualiza la información de una Categoria de Ticket existente.")]
        public async Task<IActionResult> Update(int id, [FromBody] HdkCategoryTicketRequest saveDto)
        {
            var validationResult = await _validator.ValidateAsync(saveDto);

            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return BadRequest(Response<string>.BadRequest(errors, 400));
            }

            try
            {
                var existingEmpresa = await _categoryTicketService.GetByIdResponse(id);
                if (existingEmpresa == null)
                {
                    return NotFound(Response<HdkCategoryTicketReponse>.NotFound("Categoria del Ticket no encontrada."));
                }
                saveDto.IdCategoria = id;
                await _categoryTicketService.Update(saveDto, id);
                return Ok(Response<string>.Success(null, "Categoria actualizada correctamente"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, Response<string>.ServerError("Ocurrió un error al actualizar la Categoria. Por favor, intente nuevamente."));
            }
        }
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Eliminar una Categoria de ticket", Description = "Elimina una Categoria de manera lógica.")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var categoryTicket = await _categoryTicketService.GetByIdResponse(id);
                if (categoryTicket == null)
                {
                    return NotFound(Response<string>.NotFound("Categoria de Ticket no encontrada."));
                }

                await _categoryTicketService.Delete(id);
                return Ok(Response<string>.Success(null, "Categoria de Ticket eliminada correctamente"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, Response<string>.ServerError("Ocurrió un error al eliminar la Categoria de Ticket. Por favor, intente nuevamente."));
            }
        }


    }
}
