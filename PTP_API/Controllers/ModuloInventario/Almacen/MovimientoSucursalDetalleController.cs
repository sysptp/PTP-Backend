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
    [SwaggerTag("Movimiento Sucursal Detalle")]
    [Authorize]
    [EnableBitacora]
    public class MovimientoSucursalDetalleController : ControllerBase
    {
        private readonly IInvMovimientoSucursalDetalleService _MovimientoSucursalDetalle;
        private readonly IValidator<InvMovimientoSucursalDetalleRequest> _validator;
        public MovimientoSucursalDetalleController(IInvMovimientoSucursalDetalleService MovimientoSucursalDetalle, IValidator<InvMovimientoSucursalDetalleRequest> validator)
        {
            _MovimientoSucursalDetalle = MovimientoSucursalDetalle;
            _validator = validator;
        }
        [HttpGet]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(Response<IEnumerable<InvMovimientoSucursalDetalleReponse>>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Obtener Movimiento Sucursal Detalle", Description = "Obtiene una lista de todos los Inventarios Sucursales o un Movimiento Sucursal Detalle específico si se proporciona un ID.")]
        [DisableBitacora]
        public async Task<IActionResult> Get([FromQuery] int? id)
        {
            try
            {
                if (id.HasValue)
                {
                    var MovimientoSucursalDetalle = await _MovimientoSucursalDetalle.GetByIdResponse(id);
                    if (MovimientoSucursalDetalle == null)
                    {
                        return NotFound(Response<InvMovimientoSucursalDetalleReponse>.NotFound("Movimiento Sucursal Detalle no encontrado."));
                    }
                    return Ok(Response<List<InvMovimientoSucursalDetalleReponse>>.Success(new List<InvMovimientoSucursalDetalleReponse> { MovimientoSucursalDetalle }, "Almacenes encontrado."));
                }
                else
                {
                    var MovimientoSucursalDetalles = await _MovimientoSucursalDetalle.GetAllDto();
                    if (MovimientoSucursalDetalles == null || MovimientoSucursalDetalles.Count == 0)
                    {
                        return NoContent();
                    }
                    return Ok(Response<IEnumerable<InvMovimientoSucursalDetalleReponse>>.Success(MovimientoSucursalDetalles, "Inventarios Sucursales obtenidos correctamente."));
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
        [SwaggerOperation(Summary = "Crear un nuevo Movimiento Sucursal Detalle", Description = "Crea un nuevo Movimiento Sucursal Detalle en el sistema.")]
        public async Task<IActionResult> Add([FromBody] InvMovimientoSucursalDetalleRequest request)
        {
            var validationResult = await _validator.ValidateAsync(request);

            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return BadRequest(Response<string>.BadRequest(errors, 400));
            }

            try
            {
                var almacenes = await _MovimientoSucursalDetalle.Add(request);
                return StatusCode(201, Response<InvMovimientoSucursalDetalleReponse>.Created(almacenes, "Almacen creado correctamente."));
            }
            catch (Exception ex)
            {
                return StatusCode(500, Response<string>.ServerError(ex.Message));
            }
        }

        [HttpPut("{id}")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Actualizar un Movimiento Sucursal Detalle", Description = "Actualiza la información de un Movimiento Sucursal Detalle existente.")]
        public async Task<IActionResult> Update(int id, [FromBody] InvMovimientoSucursalDetalleRequest saveDto)
        {
            var validationResult = await _validator.ValidateAsync(saveDto);

            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return BadRequest(Response<string>.BadRequest(errors, 400));
            }

            try
            {
                var almacenes = await _MovimientoSucursalDetalle.GetByIdResponse(id);
                if (almacenes == null)
                {
                    return NotFound(Response<InvMovimientoSucursalDetalleReponse>.NotFound("Movimiento Sucursal Detalle no encontrado."));
                }
                saveDto.Id = id;
                await _MovimientoSucursalDetalle.Update(saveDto, id);
                return Ok(Response<string>.Success(null, "Movimiento Sucursal Detalle actualizado correctamente"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, Response<string>.ServerError(ex.Message));
            }
        }
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Eliminar un Movimiento Sucursal Detalle", Description = "Elimina un Movimiento Sucursal Detalle de manera lógica.")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var almacenes = await _MovimientoSucursalDetalle.GetByIdResponse(id);
                if (almacenes == null)
                {
                    return NotFound(Response<string>.NotFound("Movimiento Sucursal Detalle no encontrado."));
                }

                await _MovimientoSucursalDetalle.Delete(id);
                return Ok(Response<string>.Success(null, "Movimiento Sucursal Detalle eliminado correctamente"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, Response<string>.ServerError(ex.Message));
            }
        }
    }
}
