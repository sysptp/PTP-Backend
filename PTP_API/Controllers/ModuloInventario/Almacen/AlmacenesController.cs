using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using Swashbuckle.AspNetCore.Annotations;
using BussinessLayer.Interfaces.ModuloInventario.Almacen;
using BussinessLayer.Wrappers;
using Microsoft.AspNetCore.Authorization;
using FluentValidation;
using BussinessLayer.DTOs.ModuloInventario.Almacenes;
using BussinessLayer.Atributes;


namespace PTP_API.Controllers.ModuloInventario.Almacen
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [SwaggerTag("Almacen")]
    [Authorize]
    [EnableAuditing]
    public class AlmacenesController : ControllerBase
    {
        private readonly IInvAlmacenesService _almacenesService;
        private readonly IValidator<InvAlmacenesRequest> _validator;

        public AlmacenesController(IInvAlmacenesService almacenesService, IValidator<InvAlmacenesRequest> validator)
        {
            _almacenesService = almacenesService;
            _validator = validator;
        }
        [HttpGet]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(Response<IEnumerable<InvAlmacenesReponse>>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Obtener Almacenes", Description = "Obtiene una lista de todos los Almacenes o un almacen específico si se proporciona un ID.")]
        [DisableAuditing]
        public async Task<IActionResult> Get([FromQuery] int? id)
        {
            try
            {
                if (id.HasValue)
                {
                    var almacenes = await _almacenesService.GetByIdResponse(id);
                    if (almacenes == null)
                    {
                        return NotFound(Response<InvAlmacenesReponse>.NotFound("Almacen no encontrado."));
                    }
                    return Ok(Response<List<InvAlmacenesReponse>>.Success(new List<InvAlmacenesReponse> { almacenes }, "Almacenes encontrado."));
                }
                else
                {
                    var Almaceness = await _almacenesService.GetAllDto();
                    if (Almaceness == null || Almaceness.Count == 0)
                    {
                        return NoContent();
                    }
                    return Ok(Response<IEnumerable<InvAlmacenesReponse>>.Success(Almaceness, "Almacenes obtenidos correctamente."));
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, Response<string>.ServerError("Ocurrió un error al obtener el Almacen. Por favor, intente nuevamente."));
            }
        }

        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Crear un nuevo Almacenes", Description = "Crea un nuevo Almacen en el sistema.")]
        public async Task<IActionResult> Add([FromBody] InvAlmacenesRequest request)
        {
            var validationResult = await _validator.ValidateAsync(request);

            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return BadRequest(Response<string>.BadRequest(errors, 400));
            }

            try
            {
                var almacenes = await _almacenesService.Add(request);
                return StatusCode(201, Response<InvAlmacenesReponse>.Created(almacenes, "Almacen creado correctamente."));
            }
            catch (Exception ex)
            {
                return StatusCode(500, Response<string>.ServerError("Ocurrió un error al crear un Almacen. Por favor, intente nuevamente."));
            }
        }

        [HttpPut("{id}")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Actualizar un Almacenes", Description = "Actualiza la información de un Almacen existente.")]
        public async Task<IActionResult> Update(int id, [FromBody] InvAlmacenesRequest saveDto)
        {
            var validationResult = await _validator.ValidateAsync(saveDto);

            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return BadRequest(Response<string>.BadRequest(errors, 400));
            }

            try
            {
                var almacenes = await _almacenesService.GetByIdResponse(id);
                if (almacenes == null)
                {
                    return NotFound(Response<InvAlmacenesReponse>.NotFound("Almacen no encontrado."));
                }
                saveDto.Id = id;
                await _almacenesService.Update(saveDto, id);
                return Ok(Response<string>.Success(null, "Almacen actualizado correctamente"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, Response<string>.ServerError("Ocurrió un error al actualizar el Almacen. Por favor, intente nuevamente."));
            }
        }
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Eliminar un Almacen", Description = "Elimina un Almacen de manera lógica.")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var almacenes = await _almacenesService.GetByIdResponse(id);
                if (almacenes == null)
                {
                    return NotFound(Response<string>.NotFound("Almacen no encontrado."));
                }

                await _almacenesService.Delete(id);
                return Ok(Response<string>.Success(null, "Almacen eliminado correctamente"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, Response<string>.ServerError("Ocurrió un error al eliminar el Almacen. Por favor, intente nuevamente."));
            }
        }

    }
}
