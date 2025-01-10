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
    [SwaggerTag("Movimiento Almacen Detalle")]
    [Authorize]
    [EnableBitacora]
    public class MovimientoAlmacenDetalleController : ControllerBase
    {
        private readonly IInvMovimientoAlmacenDetalleService _MovimientoAlmacenDetalle;
        private readonly IValidator<InvMovimientoAlmacenDetalleRequest> _validator;
        public MovimientoAlmacenDetalleController(IInvMovimientoAlmacenDetalleService MovimientoAlmacenDetalle, IValidator<InvMovimientoAlmacenDetalleRequest> validator)
        {
            _MovimientoAlmacenDetalle = MovimientoAlmacenDetalle;
            _validator = validator;
        }
        [HttpGet]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(Response<IEnumerable<InvMovimientoAlmacenDetalleReponse>>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Obtener Movimiento Almacen Detalle", Description = "Obtiene una lista de todos los Inventarios Sucursales o un Movimiento Almacen Detalle específico si se proporciona un ID.")]
        [DisableBitacora]
        public async Task<IActionResult> Get([FromQuery] int? id, long? idCompany)
        {
            try
            {
                if (id.HasValue)
                {
                    var MovimientoAlmacenDetalle = await _MovimientoAlmacenDetalle.GetByIdResponse(id);
                    if (MovimientoAlmacenDetalle == null)
                    {
                        return NotFound(Response<InvMovimientoAlmacenDetalleReponse>.NotFound("Movimiento Almacen Detalle no encontrado."));
                    }
                    return Ok(Response<List<InvMovimientoAlmacenDetalleReponse>>.Success(new List<InvMovimientoAlmacenDetalleReponse> { MovimientoAlmacenDetalle }, "Almacenes encontrado."));
                }
                else
                {
                    var MovimientoAlmacenDetalles = await _MovimientoAlmacenDetalle.GetAllDto();
                    if (MovimientoAlmacenDetalles == null || MovimientoAlmacenDetalles.Count == 0)
                    {
                        return NoContent();
                    }
                    return Ok(Response<IEnumerable<InvMovimientoAlmacenDetalleReponse>>.Success(
                       idCompany != null ? MovimientoAlmacenDetalles.Where(x => x.IdEmpresa == idCompany).ToList()
                       : MovimientoAlmacenDetalles, "Inventarios Sucursales obtenidos correctamente."));
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
        [SwaggerOperation(Summary = "Crear un nuevo Movimiento Almacen Detalle", Description = "Crea un nuevo Movimiento Almacen Detalle en el sistema.")]
        public async Task<IActionResult> Add([FromBody] InvMovimientoAlmacenDetalleRequest request)
        {
            var validationResult = await _validator.ValidateAsync(request);

            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return BadRequest(Response<string>.BadRequest(errors, 400));
            }

            try
            {
                var almacenes = await _MovimientoAlmacenDetalle.Add(request);
                return StatusCode(201, Response<InvMovimientoAlmacenDetalleReponse>.Created(almacenes, "Almacen creado correctamente."));
            }
            catch (Exception ex)
            {
                return StatusCode(500, Response<string>.ServerError("Ocurrió un error al crear un Movimiento Almacen Detalle. Por favor, intente nuevamente."));
            }
        }

        [HttpPut("{id}")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Actualizar un Movimiento Almacen Detalle", Description = "Actualiza la información de un Movimiento Almacen Detalle existente.")]
        public async Task<IActionResult> Update(int id, [FromBody] InvMovimientoAlmacenDetalleRequest saveDto)
        {
            var validationResult = await _validator.ValidateAsync(saveDto);

            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return BadRequest(Response<string>.BadRequest(errors, 400));
            }

            try
            {
                var almacenes = await _MovimientoAlmacenDetalle.GetByIdResponse(id);
                if (almacenes == null)
                {
                    return NotFound(Response<InvMovimientoAlmacenDetalleReponse>.NotFound("Movimiento Almacen Detalle no encontrado."));
                }
                saveDto.Id = id;
                await _MovimientoAlmacenDetalle.Update(saveDto, id);
                return Ok(Response<string>.Success(null, "Movimiento Almacen Detalle actualizado correctamente"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, Response<string>.ServerError("Ocurrió un error al actualizar el Movimiento Almacen Detalle. Por favor, intente nuevamente."));
            }
        }
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Eliminar un Movimiento Almacen Detalle", Description = "Elimina un Movimiento Almacen Detalle de manera lógica.")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var almacenes = await _MovimientoAlmacenDetalle.GetByIdResponse(id);
                if (almacenes == null)
                {
                    return NotFound(Response<string>.NotFound("Movimiento Almacen Detalle no encontrado."));
                }

                await _MovimientoAlmacenDetalle.Delete(id);
                return Ok(Response<string>.Success(null, "Movimiento Almacen Detalle eliminado correctamente"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, Response<string>.ServerError("Ocurrió un error al eliminar el Movimiento Almacen Detalle. Por favor, intente nuevamente."));
            }
        }
    }
}
