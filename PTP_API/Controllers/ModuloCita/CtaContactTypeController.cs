using BussinessLayer.Atributes;
using BussinessLayer.DTOs.ModuloCitas;
using BussinessLayer.DTOs.ModuloCitas.CtaEmailConfiguracion;
using BussinessLayer.Interfaces.Services.ModuloCitas;
using BussinessLayer.Wrappers;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net.Mime;

namespace PTP_API.Controllers.ModuloCita
{

    [ApiController]
    [SwaggerTag("Gestión de configuraciones de tipo de contactos")]
    [Route("api/v1/[controller]")]
    [Authorize]
    [EnableBitacora]
    public class CtaContactTypeController : ControllerBase
    {

        private readonly ICtaContactTypeService _contactTypeService;
        private readonly IValidator<CtaContactTypeRequest> _validator;

        public CtaContactTypeController(ICtaContactTypeService emailConfigService, IValidator<CtaContactTypeRequest> validator)
        {
            _contactTypeService = emailConfigService;
            _validator = validator;
        }

        [HttpGet]
        [ProducesResponseType(typeof(Response<IEnumerable<CtaEmailConfiguracionResponse>>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Obtener tipo de contactos", Description = "Devuelve una lista de tipos de contactos o un tipo de contacto específica si se proporciona un ID")]
        public async Task<IActionResult> GetAllConfigurations([FromQuery] int? id, long? companyId)
        {
            try
            {
                if (id.HasValue)
                {
                    var contactType = await _contactTypeService.GetByIdResponse(id.Value);
                    if (contactType == null)
                        return NotFound(Response<CtaEmailConfiguracionResponse>.NotFound("Tipo de Contacto no encontrado."));

                    return Ok(Response<CtaContactTypeResponse>.Success(contactType, "Tipo de Contacto encontrado."));
                }
                else
                {
                    var contactTypes = await _contactTypeService.GetAllDto();
                    if (contactTypes == null || !contactTypes.Any())
                        return StatusCode(204, Response<IEnumerable<CtaContactTypeResponse>>.NoContent("No hay Tipo de Contactos disponibles."));

                    return Ok(Response<IEnumerable<CtaContactTypeResponse>>.Success(
                        companyId != null ? contactTypes.Where(x => x.CompanyId == companyId) : contactTypes, "Tipo de Contactos obtenidos correctamente."));
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
        [SwaggerOperation(Summary = "Crear un Tipo de Contacto de correo", Description = "Endpoint para registrar un Tipo de Contacto")]
        public async Task<IActionResult> CreateConfiguration([FromBody] CtaContactTypeRequest request)
        {
            try
            {
                var validationResult = await _validator.ValidateAsync(request);

                if (!validationResult.IsValid)
                {
                    var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                    return BadRequest(Response<string>.BadRequest(errors, 400));
                }

                var response = await _contactTypeService.Add(request);
                return CreatedAtAction(nameof(GetAllConfigurations), Response<CtaContactTypeResponse>.Created(response));
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
        [SwaggerOperation(Summary = "Actualizar un Tipo de Contacto", Description = "Endpoint para actualizar un Tipo de Contacto")]
        public async Task<IActionResult> UpdateConfiguration(int id, [FromBody] CtaContactTypeRequest contactTypeDto)
        {
            try
            {
                var validationResult = await _validator.ValidateAsync(contactTypeDto);

                if (!validationResult.IsValid)
                {
                    var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                    return BadRequest(Response<string>.BadRequest(errors, 400));
                }

                var contactType = await _contactTypeService.GetByIdRequest(id);
                if (contactType == null)
                    return NotFound(Response<string>.NotFound("Tipo de Contacto no encontrado."));

                contactTypeDto.Id = id;
                await _contactTypeService.Update(contactTypeDto, id);
                return Ok(Response<string>.Success(null, "Tipo de Contacto actualizado correctamente."));
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
        [SwaggerOperation(Summary = "Eliminar un Tipo de Contacto", Description = "Endpoint para eliminar un Tipo de Contacto")]
        public async Task<IActionResult> DeleteConfiguration(int id)
        {
            try
            {
                var existingConfig = await _contactTypeService.GetByIdRequest(id);
                if (existingConfig == null)
                    return NotFound(Response<string>.NotFound("Tipo de Contacto no encontrado."));

                await _contactTypeService.Delete(id);
                return Ok(Response<string>.Success(null, "Tipo de Contacto eliminado correctamente."));
            }
            catch (Exception ex)
            {
                return StatusCode(500, Response<string>.ServerError(ex.Message));
            }
        }
    }

}

