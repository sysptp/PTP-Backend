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
    [SwaggerTag("Movimiento Almacen Sucursal")]
    [Authorize]
    [EnableBitacora]
    public class MovAlmacenSucursalController : ControllerBase
    {
        private readonly IInvMovAlmacenSucursalService _MovAlmacenSucursal;
        private readonly IValidator<InvMovAlmacenSucursalRequest> _validator;
        public MovAlmacenSucursalController(IInvMovAlmacenSucursalService MovAlmacenSucursal, IValidator<InvMovAlmacenSucursalRequest> validator)
        {
            _MovAlmacenSucursal = MovAlmacenSucursal;
            _validator = validator;
        }
        [HttpGet]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(Response<IEnumerable<InvMovAlmacenSucursalReponse>>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Obtener Movimiento Almacen Sucursal", Description = "Obtiene una lista de todos los Inventarios Sucursales o un Movimiento Almacen Sucursal específico si se proporciona un ID.")]
        [DisableBitacora]
        public async Task<IActionResult> Get([FromQuery] int? id)
        {
            try
            {
                if (id.HasValue)
                {
                    var MovAlmacenSucursal = await _MovAlmacenSucursal.GetByIdResponse(id);
                    if (MovAlmacenSucursal == null)
                    {
                        return NotFound(Response<InvMovAlmacenSucursalReponse>.NotFound("Movimiento Almacen Sucursal no encontrado."));
                    }
                    return Ok(Response<List<InvMovAlmacenSucursalReponse>>.Success(new List<InvMovAlmacenSucursalReponse> { MovAlmacenSucursal }, "Almacenes encontrado."));
                }
                else
                {
                    var MovAlmacenSucursals = await _MovAlmacenSucursal.GetAllDto();
                    if (MovAlmacenSucursals == null || MovAlmacenSucursals.Count == 0)
                    {
                        return NoContent();
                    }
                    return Ok(Response<IEnumerable<InvMovAlmacenSucursalReponse>>.Success(MovAlmacenSucursals, "Inventarios Sucursales obtenidos correctamente."));
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, Response<string>.ServerError("Ocurrió un error al obtener el Movimiento Almacen Sucursal. Por favor, intente nuevamente."));
            }
        }

        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Crear un nuevo Movimiento Almacen Sucursal", Description = "Crea un nuevo Movimiento Almacen Sucursal en el sistema.")]
        public async Task<IActionResult> Add([FromBody] InvMovAlmacenSucursalRequest request)
        {
            var validationResult = await _validator.ValidateAsync(request);

            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return BadRequest(Response<string>.BadRequest(errors, 400));
            }

            try
            {
                var almacenes = await _MovAlmacenSucursal.Add(request);
                return StatusCode(201, Response<InvMovAlmacenSucursalReponse>.Created(almacenes, "Almacen creado correctamente."));
            }
            catch (Exception ex)
            {
                return StatusCode(500, Response<string>.ServerError("Ocurrió un error al crear un Movimiento Almacen Sucursal. Por favor, intente nuevamente."));
            }
        }

        [HttpPut("{id}")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Actualizar un Movimiento Almacen Sucursal", Description = "Actualiza la información de un Movimiento Almacen Sucursal existente.")]
        public async Task<IActionResult> Update(int id, [FromBody] InvMovAlmacenSucursalRequest saveDto)
        {
            var validationResult = await _validator.ValidateAsync(saveDto);

            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return BadRequest(Response<string>.BadRequest(errors, 400));
            }

            try
            {
                var almacenes = await _MovAlmacenSucursal.GetByIdResponse(id);
                if (almacenes == null)
                {
                    return NotFound(Response<InvMovAlmacenSucursalReponse>.NotFound("Movimiento Almacen Sucursal no encontrado."));
                }
                saveDto.Id = id;
                await _MovAlmacenSucursal.Update(saveDto, id);
                return Ok(Response<string>.Success(null, "Movimiento Almacen Sucursal actualizado correctamente"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, Response<string>.ServerError("Ocurrió un error al actualizar el Movimiento Almacen Sucursal. Por favor, intente nuevamente."));
            }
        }
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Eliminar un Movimiento Almacen Sucursal", Description = "Elimina un Movimiento Almacen Sucursal de manera lógica.")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var almacenes = await _MovAlmacenSucursal.GetByIdResponse(id);
                if (almacenes == null)
                {
                    return NotFound(Response<string>.NotFound("Movimiento Almacen Sucursal no encontrado."));
                }

                await _MovAlmacenSucursal.Delete(id);
                return Ok(Response<string>.Success(null, "Movimiento Almacen Sucursal eliminado correctamente"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, Response<string>.ServerError("Ocurrió un error al eliminar el Movimiento Almacen Sucursal. Por favor, intente nuevamente."));
            }
        }
    }
}
