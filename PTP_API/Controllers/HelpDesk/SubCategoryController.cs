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
    [SwaggerTag("Gestión de SubCategoria de Ticket")]
    [Authorize]
    public class SubCategoryController : ControllerBase
    {
        private readonly IHdkSubCategoryService _subCategoryService;
        private readonly IValidator<HdkSubCategoryRequest> _validator;

        public SubCategoryController(IHdkSubCategoryService subCategoryService, IValidator<HdkSubCategoryRequest> validator)
        {
            _subCategoryService = subCategoryService;
            _validator = validator;
        }

        [HttpGet]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(Response<IEnumerable<HdkSubCategoryReponse>>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Obtener SubCategoria de ticket", Description = "Obtiene una lista de todos los departamento de ticket o un SubCategoria específico si se proporciona un ID.")]
        public async Task<IActionResult> Get([FromQuery] int? id)
        {
            try
            {
                if (id.HasValue)
                {
                    var subCategoryTicket = await _subCategoryService.GetByIdResponse(id);
                    if (subCategoryTicket == null)
                    {
                        return NotFound(Response<HdkSubCategoryReponse>.NotFound("SubCategoria no encontrado."));
                    }
                    return Ok(Response<List<HdkSubCategoryReponse>>.Success(new List<HdkSubCategoryReponse> { subCategoryTicket }, "SubCategoria de ticket encontrado."));
                }
                else
                {
                    var subCategoryTickets = await _subCategoryService.GetAllDto();
                    if (subCategoryTickets == null || subCategoryTickets.Count == 0)
                    {
                        return NoContent();
                    }
                    return Ok(Response<IEnumerable<HdkSubCategoryReponse>>.Success(subCategoryTickets, "SubCategoria de ticket obtenidos correctamente."));
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, Response<string>.ServerError("Ocurrió un error al obtener la SubCategoria de tickets. Por favor, intente nuevamente."));
            }
        }

        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Crear un nueva SubCategoria de Ticket", Description = "Crea un nueva SubCategoria de Ticket en el sistema.")]
        public async Task<IActionResult> Add([FromBody] HdkSubCategoryRequest request)
        {
            var validationResult = await _validator.ValidateAsync(request);

            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return BadRequest(Response<string>.BadRequest(errors, 400));
            }

            try
            {
                var departamentoXusuarioTicket = await _subCategoryService.Add(request);
                return StatusCode(201, Response<HdkSubCategoryReponse>.Created(departamentoXusuarioTicket, "SubCategoria de Ticket creado correctamente."));
            }
            catch (Exception ex)
            {
                return StatusCode(500, Response<string>.ServerError("Ocurrió un error al crear un SubCategoria de Ticket. Por favor, intente nuevamente."));
            }
        }

        [HttpPut("{id}")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Actualizar un SubCategoria de Ticket", Description = "Actualiza la información de un SubCategoria de Ticket existente.")]
        public async Task<IActionResult> Update(int id, [FromBody] HdkSubCategoryRequest saveDto)
        {
            var validationResult = await _validator.ValidateAsync(saveDto);

            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return BadRequest(Response<string>.BadRequest(errors, 400));
            }

            try
            {
                var subCategoryTicket = await _subCategoryService.GetByIdResponse(id);
                if (subCategoryTicket == null)
                {
                    return NotFound(Response<HdkSubCategoryReponse>.NotFound("SubCategoria de Ticket no encontrado."));
                }
                saveDto.IdSubCategory = id;
                await _subCategoryService.Update(saveDto, id);
                return Ok(Response<string>.Success(null, "SubCategoria de Ticket actualizado correctamente"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, Response<string>.ServerError("Ocurrió un error al actualizar la SubCategoria de Ticket. Por favor, intente nuevamente."));
            }
        }
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Eliminar un SubCategoria de ticket", Description = "Elimina un SubCategoria de manera lógica.")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var departamentoXusuarioTicket = await _subCategoryService.GetByIdResponse(id);
                if (departamentoXusuarioTicket == null)
                {
                    return NotFound(Response<string>.NotFound("SubCategoria de Ticket no encontrado."));
                }

                await _subCategoryService.Delete(id);
                return Ok(Response<string>.Success(null, "SubCategoria de Ticket eliminada correctamente"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, Response<string>.ServerError("Ocurrió un error al eliminar la SubCategoria de Ticket. Por favor, intente nuevamente."));
            }
        }
    }
}
