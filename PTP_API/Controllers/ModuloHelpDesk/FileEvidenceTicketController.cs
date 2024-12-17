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
    [SwaggerTag("Gestión de Archivos de Evidencias de Ticket")]
    [Authorize]
    [EnableBitacora]
    public class FileEvidenceTicketController : ControllerBase
    {
        private readonly IHdkFileEvidenceTicketService _fileEvidenceTicketService;
        private readonly IValidator<HdkFileEvidenceTicketRequest> _validator;

        public FileEvidenceTicketController(IHdkFileEvidenceTicketService fileEvidenceTicketService, IValidator<HdkFileEvidenceTicketRequest> validator)
        {
            _fileEvidenceTicketService = fileEvidenceTicketService;
            _validator = validator;
        }
        [HttpGet]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(Response<IEnumerable<HdkFileEvidenceTicketReponse>>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Obtener Archivo de Evidencia de ticket", Description = "Obtiene una lista de todos los departamento de ticket o un Archivo de Evidencia específico si se proporciona un ID.")]
        [DisableBitacora]
        public async Task<IActionResult> Get([FromQuery] int? id)
        {
            try
            {
                if (id.HasValue)
                {
                    var fileEvidenceTicketTicket = await _fileEvidenceTicketService.GetByIdResponse(id);
                    if (fileEvidenceTicketTicket == null)
                    {
                        return NotFound(Response<HdkFileEvidenceTicketReponse>.NotFound("Archivo de Evidencia no encontrado."));
                    }
                    return Ok(Response<List<HdkFileEvidenceTicketReponse>>.Success(new List<HdkFileEvidenceTicketReponse> { fileEvidenceTicketTicket }, "Archivo de Evidencia de ticket encontrado."));
                }
                else
                {
                    var fileEvidenceTicketTickets = await _fileEvidenceTicketService.GetAllDto();
                    if (fileEvidenceTicketTickets == null || fileEvidenceTicketTickets.Count == 0)
                    {
                        return NoContent();
                    }
                    return Ok(Response<IEnumerable<HdkFileEvidenceTicketReponse>>.Success(fileEvidenceTicketTickets, "Archivo de Evidencia de ticket obtenidos correctamente."));
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, Response<string>.ServerError("Ocurrió un error al obtener el Archivo de Evidencia de tickets. Por favor, intente nuevamente."));
            }
        }

        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Crear un nuevo Archivo de Evidencia de Ticket", Description = "Crea un nuevo Archivo de Evidencia de Ticket en el sistema.")]
        public async Task<IActionResult> Add([FromBody] HdkFileEvidenceTicketRequest request)
        {
            var validationResult = await _validator.ValidateAsync(request);

            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return BadRequest(Response<string>.BadRequest(errors, 400));
            }

            try
            {
                var fileEvidenceTicket = await _fileEvidenceTicketService.Add(request);
                return StatusCode(201, Response<HdkFileEvidenceTicketReponse>.Created(fileEvidenceTicket, "Archivo de Evidencia de Ticket creado correctamente."));
            }
            catch (Exception ex)
            {
                return StatusCode(500, Response<string>.ServerError("Ocurrió un error al crear un Archivo de Evidencia de Ticket. Por favor, intente nuevamente."));
            }
        }

        [HttpPut("{id}")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Actualizar un Archivo de Evidencia de Ticket", Description = "Actualiza la información de un Archivo de Evidencia de Ticket existente.")]
        public async Task<IActionResult> Update(int id, [FromBody] HdkFileEvidenceTicketRequest saveDto)
        {
            var validationResult = await _validator.ValidateAsync(saveDto);

            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return BadRequest(Response<string>.BadRequest(errors, 400));
            }

            try
            {
                var fileEvidenceTicketTicket = await _fileEvidenceTicketService.GetByIdResponse(id);
                if (fileEvidenceTicketTicket == null)
                {
                    return NotFound(Response<HdkFileEvidenceTicketReponse>.NotFound("Archivo de Evidencia no encontrado."));
                }
                saveDto.IdFileEvidence = id;
                await _fileEvidenceTicketService.Update(saveDto, id);
                return Ok(Response<string>.Success(null, "Archivo de Evidencia de Ticket actualizado correctamente"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, Response<string>.ServerError("Ocurrió un error al actualizar el Archivo de Evidencia de Ticket. Por favor, intente nuevamente."));
            }
        }
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Eliminar un Archivo de Evidencia de ticket", Description = "Elimina un Archivo de Evidencia de manera lógica.")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var fileEvidenceTicket = await _fileEvidenceTicketService.GetByIdResponse(id);
                if (fileEvidenceTicket == null)
                {
                    return NotFound(Response<string>.NotFound("Archivo de Evidencia de Ticket no encontrado."));
                }

                await _fileEvidenceTicketService.Delete(id);
                return Ok(Response<string>.Success(null, "Archivo de Evidencia de Ticket eliminado correctamente"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, Response<string>.ServerError("Ocurrió un error al eliminar el Archivo de Evidencia de Ticket. Por favor, intente nuevamente."));
            }
        }
    }
}
