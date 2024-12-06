using BussinessLayer.DTOs.ModuloGeneral.ParametroGenerales;
using BussinessLayer.Interfaces.ModuloGeneral.ParametrosGenerales;
using BussinessLayer.Wrappers;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net.Mime;

namespace PTP_API.Controllers.ModuloGeneral.ParametrosGenerales
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [SwaggerTag("Gestión de Parametros Generales")]
    [Authorize]
    public class ParametrosGeneralesController : ControllerBase
    {
        private readonly IGnParametrosGeneralesService _parametrosGeneralesService;
        private readonly IValidator<GnParametrosGeneralesRequest> _validator;

        public ParametrosGeneralesController(IGnParametrosGeneralesService parametrosGeneralesService, IValidator<GnParametrosGeneralesRequest> validator)
        {
            _parametrosGeneralesService = parametrosGeneralesService;
            _validator = validator;
        }
        [HttpGet]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(Response<IEnumerable<GnParametrosGeneralesReponse>>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Obtener Parametros Generales", Description = "Obtiene una lista de todas las Parametros Generales o una categoria específica si se proporciona un ID.")]
        public async Task<IActionResult> Get([FromQuery] long? id)
        {
            try
            {
                if (id.HasValue)
                {
                    var parametrosGenerales = await _parametrosGeneralesService.GetByIdResponse(id);
                    if (parametrosGenerales == null)
                    {
                        return NotFound(Response<GnParametrosGeneralesReponse>.NotFound("Parametros Generales no encontrada."));
                    }
                    return Ok(Response<List<GnParametrosGeneralesReponse>>.Success(new List<GnParametrosGeneralesReponse> { parametrosGenerales }, "Parametros Generales encontrada."));
                }
                else
                {
                    var parametrosGeneraless = await _parametrosGeneralesService.GetAllDto();
                    if (parametrosGeneraless == null || parametrosGeneraless.Count == 0)
                    {
                        return NoContent();
                    }
                    return Ok(Response<IEnumerable<GnParametrosGeneralesReponse>>.Success(parametrosGeneraless, "Parametros Generales obtenidas correctamente."));
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
        [SwaggerOperation(Summary = "Crear una nueva Parametros Generales", Description = "Crea una nueva Parametros Generales en el sistema.")]
        public async Task<IActionResult> Add([FromBody] GnParametrosGeneralesRequest request)
        {
            var validationResult = await _validator.ValidateAsync(request);

            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return BadRequest(Response<string>.BadRequest(errors, 400));
            }

            try
            {
                var parametrosGenerales = await _parametrosGeneralesService.Add(request);
                return StatusCode(201, Response<GnParametrosGeneralesReponse>.Created(parametrosGenerales, "Categoria del Ticket creada correctamente."));
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
        [SwaggerOperation(Summary = "Actualizar una Parametros Generales", Description = "Actualiza la información de una Parametros Generales existente.")]
        public async Task<IActionResult> Update(long id, [FromBody] GnParametrosGeneralesRequest saveDto)
        {
            var validationResult = await _validator.ValidateAsync(saveDto);

            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return BadRequest(Response<string>.BadRequest(errors, 400));
            }

            try
            {
                var existingEmpresa = await _parametrosGeneralesService.GetByIdResponse(id);
                if (existingEmpresa == null)
                {
                    return NotFound(Response<GnParametrosGeneralesReponse>.NotFound("Parametros generales no encontrada."));
                }
                saveDto.IdParametro = id;
                await _parametrosGeneralesService.Update(saveDto, id);
                return Ok(Response<string>.Success(null, "Parametro genreales actualizado correctamente"));
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
        [SwaggerOperation(Summary = "Eliminar una Parametros Generales", Description = "Elimina un Parametro general de manera lógica.")]
        public async Task<IActionResult> Delete(long id)
        {
            try
            {
                var parametrosGenerales = await _parametrosGeneralesService.GetByIdResponse(id);
                if (parametrosGenerales == null)
                {
                    return NotFound(Response<string>.NotFound("Parametros Generales no encontrada."));
                }

                await _parametrosGeneralesService.Delete(id);
                return Ok(Response<string>.Success(null, "Parametros Generales eliminada correctamente"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, Response<string>.ServerError("Ocurrió un error al eliminar la Parametros Generales. Por favor, intente nuevamente."));
            }
        }
    }
}
