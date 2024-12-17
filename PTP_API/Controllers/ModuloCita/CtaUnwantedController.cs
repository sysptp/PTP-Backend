using BussinessLayer.Atributes;
using BussinessLayer.DTOs.ModuloCitas.CtaUnwanted;
using BussinessLayer.Interface.Modulo_Citas;
using BussinessLayer.Wrappers;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net.Mime;

namespace PTP_API.Controllers.ModuloCitas
{
    [ApiController]
    [SwaggerTag("Gestión de contactos no deseados")]
    [Route("api/v1/[controller]")]
    [Authorize]
    [EnableBitacora]
    public class CtaUnwantedController : ControllerBase
    {
        private readonly ICtaUnwantedService _unwantedService;
        private readonly IValidator<CtaUnwantedRequest> _validator;

        public CtaUnwantedController(ICtaUnwantedService unwantedService, IValidator<CtaUnwantedRequest> validator)
        {
            _unwantedService = unwantedService;
            _validator = validator;
        }

        [HttpGet]
        [ProducesResponseType(typeof(Response<IEnumerable<CtaUnwantedResponse>>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Obtener contactos no deseados", Description = "Devuelve una lista de contactos no deseados o un contacto específico si se proporciona un ID")]
        public async Task<IActionResult> GetAllUnwanted([FromQuery] int? IdUnwanted)
        {
            try
            {
                if (IdUnwanted.HasValue)
                {
                    var unwanted = await _unwantedService.GetByIdResponse(IdUnwanted.Value);
                    if (unwanted == null)
                        return NotFound(Response<CtaUnwantedResponse>.NotFound("Contacto no deseado no encontrado."));

                    return Ok(Response<CtaUnwantedResponse>.Success(unwanted, "Contacto no deseado encontrado."));
                }
                else
                {
                    var unwanteds = await _unwantedService.GetAllDto();
                    if (unwanteds == null || !unwanteds.Any())
                        return StatusCode(204, Response<IEnumerable<CtaUnwantedResponse>>.NoContent("No hay contactos no deseados disponibles."));

                    return Ok(Response<IEnumerable<CtaUnwantedResponse>>.Success(unwanteds, "Contactos no deseados obtenidos correctamente."));
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
        [SwaggerOperation(Summary = "Agregar un contacto no deseado", Description = "Endpoint para registrar un contacto no deseado")]
        public async Task<IActionResult> CreateUnwanted([FromBody] CtaUnwantedRequest unwantedDto)
        {
            try
            {
                var validationResult = await _validator.ValidateAsync(unwantedDto);

                if (!validationResult.IsValid)
                {
                    var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                    return BadRequest(Response<string>.BadRequest(errors, 400));
                }

                var response = await _unwantedService.Add(unwantedDto);
                return CreatedAtAction(nameof(GetAllUnwanted), response);
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
        [SwaggerOperation(Summary = "Actualizar un contacto no deseado", Description = "Endpoint para actualizar un contacto no deseado")]
        public async Task<IActionResult> UpdateUnwanted(int id, [FromBody] CtaUnwantedRequest unwantedDto)
        {
            try
            {
                var validationResult = await _validator.ValidateAsync(unwantedDto);

                if (!validationResult.IsValid)
                {
                    var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                    return BadRequest(Response<string>.BadRequest(errors, 400));
                }

                var existingUnwanted = await _unwantedService.GetByIdRequest(id);
                if (existingUnwanted == null)
                    return NotFound(Response<string>.NotFound("Contacto no deseado no encontrado."));

                unwantedDto.IdUnwanted = id;
                await _unwantedService.Update(unwantedDto, id);
                return Ok(Response<string>.Success(null, "Contacto no deseado actualizado correctamente."));
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
        [SwaggerOperation(Summary = "Eliminar un contacto no deseado", Description = "Endpoint para eliminar un contacto no deseado")]
        public async Task<IActionResult> DeleteUnwanted(int id)
        {
            try
            {
                var existingUnwanted = await _unwantedService.GetByIdRequest(id);
                if (existingUnwanted == null)
                    return NotFound(Response<string>.NotFound("Contacto no deseado no encontrado."));

                await _unwantedService.Delete(id);
                return Ok(Response<string>.Success(null, "Contacto no deseado eliminado correctamente."));
            }
            catch (Exception ex)
            {
                return StatusCode(500, Response<string>.ServerError(ex.Message));
            }
        }
    }
}
