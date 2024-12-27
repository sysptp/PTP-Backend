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
    [SwaggerTag("Movovimiento Almacen Sucursal Detalle")]
    [Authorize]
    [EnableAuditing]
    public class MovAlmacenSucursalDetalleController : ControllerBase
    {
        private readonly IInvMovAlmacenSucursalDetalleService _MovAlmacenSucursalDetalle;
        private readonly IValidator<InvMovAlmacenSucursalDetalleRequest> _validator;
        public MovAlmacenSucursalDetalleController(IInvMovAlmacenSucursalDetalleService MovAlmacenSucursalDetalle, IValidator<InvMovAlmacenSucursalDetalleRequest> validator)
        {
            _MovAlmacenSucursalDetalle = MovAlmacenSucursalDetalle;
            _validator = validator;
        }
        [HttpGet]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(Response<IEnumerable<InvMovAlmacenSucursalDetalleReponse>>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Obtener Movimiento Almacen Sucursal Detalle", Description = "Obtiene una lista de todos los Inventarios Sucursales o un Movimiento Almacen Sucursal Detalle específico si se proporciona un ID.")]
        [DisableAuditing]
        public async Task<IActionResult> Get([FromQuery] int? id)
        {
            try
            {
                if (id.HasValue)
                {
                    var MovAlmacenSucursalDetalle = await _MovAlmacenSucursalDetalle.GetByIdResponse(id);
                    if (MovAlmacenSucursalDetalle == null)
                    {
                        return NotFound(Response<InvMovAlmacenSucursalDetalleReponse>.NotFound("Movimiento Almacen Sucursal Detalle no encontrado."));
                    }
                    return Ok(Response<List<InvMovAlmacenSucursalDetalleReponse>>.Success(new List<InvMovAlmacenSucursalDetalleReponse> { MovAlmacenSucursalDetalle }, "Almacenes encontrado."));
                }
                else
                {
                    var MovAlmacenSucursalDetalles = await _MovAlmacenSucursalDetalle.GetAllDto();
                    if (MovAlmacenSucursalDetalles == null || MovAlmacenSucursalDetalles.Count == 0)
                    {
                        return NoContent();
                    }
                    return Ok(Response<IEnumerable<InvMovAlmacenSucursalDetalleReponse>>.Success(MovAlmacenSucursalDetalles, "Inventarios Sucursales obtenidos correctamente."));
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, Response<string>.ServerError("Ocurrió un error al obtener el Movimiento Almacen Sucursal Detalle. Por favor, intente nuevamente."));
            }
        }

        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Crear un nuevo Movimiento Almacen Sucursal Detalle", Description = "Crea un nuevo Movimiento Almacen Sucursal Detalle en el sistema.")]
        public async Task<IActionResult> Add([FromBody] InvMovAlmacenSucursalDetalleRequest request)
        {
            var validationResult = await _validator.ValidateAsync(request);

            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return BadRequest(Response<string>.BadRequest(errors, 400));
            }

            try
            {
                var almacenes = await _MovAlmacenSucursalDetalle.Add(request);
                return StatusCode(201, Response<InvMovAlmacenSucursalDetalleReponse>.Created(almacenes, "Almacen creado correctamente."));
            }
            catch (Exception ex)
            {
                return StatusCode(500, Response<string>.ServerError("Ocurrió un error al crear un Movimiento Almacen Sucursal Detalle. Por favor, intente nuevamente."));
            }
        }

        [HttpPut("{id}")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Actualizar un Movimiento Almacen Sucursal Detalle", Description = "Actualiza la información de un Movimiento Almacen Sucursal Detalle existente.")]
        public async Task<IActionResult> Update(int id, [FromBody] InvMovAlmacenSucursalDetalleRequest saveDto)
        {
            var validationResult = await _validator.ValidateAsync(saveDto);

            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return BadRequest(Response<string>.BadRequest(errors, 400));
            }

            try
            {
                var almacenes = await _MovAlmacenSucursalDetalle.GetByIdResponse(id);
                if (almacenes == null)
                {
                    return NotFound(Response<InvMovAlmacenSucursalDetalleReponse>.NotFound("Movimiento Almacen Sucursal Detalle no encontrado."));
                }
                saveDto.Id = id;
                await _MovAlmacenSucursalDetalle.Update(saveDto, id);
                return Ok(Response<string>.Success(null, "Movimiento Almacen Sucursal Detalle actualizado correctamente"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, Response<string>.ServerError("Ocurrió un error al actualizar el Movimiento Almacen Sucursal Detalle. Por favor, intente nuevamente."));
            }
        }
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Eliminar un Movimiento Almacen Sucursal Detalle", Description = "Elimina un Movimiento Almacen Sucursal Detalle de manera lógica.")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var almacenes = await _MovAlmacenSucursalDetalle.GetByIdResponse(id);
                if (almacenes == null)
                {
                    return NotFound(Response<string>.NotFound("Movimiento Almacen Sucursal Detalle no encontrado."));
                }

                await _MovAlmacenSucursalDetalle.Delete(id);
                return Ok(Response<string>.Success(null, "Movimiento Almacen Sucursal Detalle eliminado correctamente"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, Response<string>.ServerError("Ocurrió un error al eliminar el Movimiento Almacen Sucursal Detalle. Por favor, intente nuevamente."));
            }
        }
    }
}
