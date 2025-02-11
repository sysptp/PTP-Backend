using System.Net.Mime;
using BussinessLayer.DTOs.ModuloCitas.CtaContacts;
using BussinessLayer.Interfaces.Services.ModuloCitas;
using BussinessLayer.Wrappers;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace PTP_API.Controllers.ModuloCita
{
    [ApiController]
    [SwaggerTag("Gestión de Contactos de Citas")]
    [Route("api/v1/[controller]")]
    [Authorize]
    public class CtaContactController : ControllerBase
    {
        private readonly ICtaContactService _contactService;
        private readonly IValidator<CtaContactRequest> _validator;

        public CtaContactController(ICtaContactService contactService, IValidator<CtaContactRequest> validator)
        {
            _contactService = contactService;
            _validator = validator;
        }

        [HttpGet]
        [ProducesResponseType(typeof(Response<IEnumerable<CtaContactResponse>>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Obtener Contactos de Citas", Description = "Devuelve una lista de Contactos de Citas o un contacto específico si se proporciona un ID")]
        public async Task<IActionResult> GetAllContacts([FromQuery] int? id, long? companyId)
        {
            try
            {
                if (id.HasValue)
                {
                    var contact = await _contactService.GetByIdResponse(id.Value);
                    if (contact == null)
                        return NotFound(Response<CtaContactResponse>.NotFound("Contacto no encontrado."));

                    return Ok(Response<CtaContactResponse>.Success(contact, "Contacto encontrado."));
                }
                else
                {
                    var contacts = await _contactService.GetAllDto();
                    if (contacts == null || !contacts.Any())
                        return StatusCode(204, Response<IEnumerable<CtaContactResponse>>.NoContent("No hay contactos disponibles."));

                    return Ok(Response<IEnumerable<CtaContactResponse>>.Success(
                        companyId != null ? contacts.Where(x => x.CompanyId == companyId).ToList() : contacts, "Contactos obtenidos correctamente."));
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, Response<string>.ServerError(ex.Message));
            }
        }

        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Agregar un nuevo contacto", Description = "Endpoint para registrar un contacto")]
        public async Task<IActionResult> CreateContact([FromBody] CtaContactRequest contactDto)
        {
            try
            {
                var validationResult = await _validator.ValidateAsync(contactDto);

                if (!validationResult.IsValid)
                {
                    var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                    return BadRequest(Response<string>.BadRequest(errors, 400));
                }

                var response = await _contactService.Add(contactDto);
                return CreatedAtAction(nameof(GetAllContacts), Response<CtaContactResponse>.Created(response));
            }
            catch (Exception ex)
            {
                return StatusCode(500, Response<string>.ServerError(ex.Message));
            }
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Actualizar un contacto", Description = "Endpoint para actualizar un contacto")]
        public async Task<IActionResult> UpdateContact(int id, [FromBody] CtaContactRequest contactDto)
        {
            try
            {
                var validationResult = await _validator.ValidateAsync(contactDto);

                if (!validationResult.IsValid)
                {
                    var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                    return BadRequest(Response<string>.BadRequest(errors, 400));
                }

                var existingContact = await _contactService.GetByIdRequest(id);
                if (existingContact == null)
                    return NotFound(Response<string>.NotFound("Contacto no encontrado."));

                contactDto.Id = id;
                await _contactService.Update(contactDto, id);
                return Ok(Response<string>.Success(null, "Contacto actualizado correctamente."));
            }
            catch (Exception ex)
            {
                return StatusCode(500, Response<string>.ServerError(ex.Message));
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Eliminar un contacto", Description = "Endpoint para eliminar un contacto")]
        public async Task<IActionResult> DeleteContact(int id)
        {
            try
            {
                var existingContact = await _contactService.GetByIdRequest(id);
                if (existingContact == null)
                    return NotFound(Response<string>.NotFound("Contacto no encontrado."));

                await _contactService.Delete(id);
                return Ok(Response<string>.Success(null, "Contacto eliminado correctamente."));
            }
            catch (Exception ex)
            {
                return StatusCode(500, Response<string>.ServerError(ex.Message));
            }
        }
    }
}
