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
    [SwaggerTag("Movimiento Almacen")]
    [Authorize]
    [EnableBitacora]
    public class MovimientoAlmacenController : ControllerBase
    {
        private readonly IInvMovimientoAlmacenService _MovimientoAlmacen;
        private readonly IValidator<InvMovimientoAlmacenRequest> _validator;
        public MovimientoAlmacenController(IInvMovimientoAlmacenService MovimientoAlmacen, IValidator<InvMovimientoAlmacenRequest> validator)
        {
            _MovimientoAlmacen = MovimientoAlmacen;
            _validator = validator;
        }
        [HttpGet]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(Response<IEnumerable<InvMovimientoAlmacenReponse>>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Obtener Movimiento Almacen", Description = "Obtiene una lista de todos los Inventarios Sucursales o un Movimiento Almacen específico si se proporciona un ID.")]
        [DisableBitacora]
        public async Task<IActionResult> Get([FromQuery] int? id, long? idCompany)
        {
            try
            {
                if (id.HasValue)
                {
                    var MovimientoAlmacen = await _MovimientoAlmacen.GetByIdResponse(id);
                    if (MovimientoAlmacen == null)
                    {
                        return NotFound(Response<InvMovimientoAlmacenReponse>.NotFound("Movimiento Almacen no encontrado."));
                    }
                    return Ok(Response<List<InvMovimientoAlmacenReponse>>.Success(new List<InvMovimientoAlmacenReponse> { MovimientoAlmacen }, "Almacenes encontrado."));
                }
                else
                {
                    var MovimientoAlmacens = await _MovimientoAlmacen.GetAllDto();
                    if (MovimientoAlmacens == null || MovimientoAlmacens.Count == 0)
                    {
                        return NoContent();
                    }
                    return Ok(Response<IEnumerable<InvMovimientoAlmacenReponse>>.Success
                        (idCompany != null ? MovimientoAlmacens.Where(x => x.IdEmpresa == idCompany).ToList() 
                        : MovimientoAlmacens, "Inventarios Sucursales obtenidos correctamente."));
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
        [SwaggerOperation(Summary = "Crear un nuevo Movimiento Almacen", Description = "Crea un nuevo Movimiento Almacen en el sistema.")]
        public async Task<IActionResult> Add([FromBody] InvMovimientoAlmacenRequest request)
        {
            var validationResult = await _validator.ValidateAsync(request);

            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return BadRequest(Response<string>.BadRequest(errors, 400));
            }

            try
            {
                var almacenes = await _MovimientoAlmacen.Add(request);
                return StatusCode(201, Response<InvMovimientoAlmacenReponse>.Created(almacenes, "Almacen creado correctamente."));
            }
            catch (Exception ex)
            {
                return StatusCode(500, Response<string>.ServerError("Ocurrió un error al crear un Movimiento Almacen. Por favor, intente nuevamente."));
            }
        }

        [HttpPut("{id}")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Actualizar un Movimiento Almacen", Description = "Actualiza la información de un Movimiento Almacen existente.")]
        public async Task<IActionResult> Update(int id, [FromBody] InvMovimientoAlmacenRequest saveDto)
        {
            var validationResult = await _validator.ValidateAsync(saveDto);

            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return BadRequest(Response<string>.BadRequest(errors, 400));
            }

            try
            {
                var almacenes = await _MovimientoAlmacen.GetByIdResponse(id);
                if (almacenes == null)
                {
                    return NotFound(Response<InvMovimientoAlmacenReponse>.NotFound("Movimiento Almacen no encontrado."));
                }
                saveDto.Id = id;
                await _MovimientoAlmacen.Update(saveDto, id);
                return Ok(Response<string>.Success(null, "Movimiento Almacen actualizado correctamente"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, Response<string>.ServerError("Ocurrió un error al actualizar el Movimiento Almacen. Por favor, intente nuevamente."));
            }
        }
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Eliminar un Movimiento Almacen", Description = "Elimina un Movimiento Almacen de manera lógica.")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var almacenes = await _MovimientoAlmacen.GetByIdResponse(id);
                if (almacenes == null)
                {
                    return NotFound(Response<string>.NotFound("Movimiento Almacen no encontrado."));
                }

                await _MovimientoAlmacen.Delete(id);
                return Ok(Response<string>.Success(null, "Movimiento Almacen eliminado correctamente"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, Response<string>.ServerError("Ocurrió un error al eliminar el Movimiento Almacen. Por favor, intente nuevamente."));
            }
        }
    }
}
