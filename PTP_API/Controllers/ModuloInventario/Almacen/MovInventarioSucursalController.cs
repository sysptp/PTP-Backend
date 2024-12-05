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
    [SwaggerTag("Movimiento Movimiento Inventario Sucursal")]
    [Authorize]
    [EnableAuditing]
    public class MovMovInventarioSucursalController : ControllerBase
    {
        private readonly IInvMovInventarioSucursalService _MovInventarioSucursal;
        private readonly IValidator<InvMovInventarioSucursalRequest> _validator;
        public MovMovInventarioSucursalController(IInvMovInventarioSucursalService MovInventarioSucursal, IValidator<InvMovInventarioSucursalRequest> validator)
        {
            _MovInventarioSucursal = MovInventarioSucursal;
            _validator = validator;
        }
        [HttpGet]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(Response<IEnumerable<InvMovInventarioSucursalReponse>>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Obtener Movimiento Inventario Sucursal", Description = "Obtiene una lista de todos los Inventarios Sucursales o un Movimiento Inventario Sucursal específico si se proporciona un ID.")]
        [DisableAuditing]
        public async Task<IActionResult> Get([FromQuery] int? id)
        {
            try
            {
                if (id.HasValue)
                {
                    var MovInventarioSucursal = await _MovInventarioSucursal.GetByIdResponse(id);
                    if (MovInventarioSucursal == null)
                    {
                        return NotFound(Response<InvMovInventarioSucursalReponse>.NotFound("Movimiento Inventario Sucursal no encontrado."));
                    }
                    return Ok(Response<List<InvMovInventarioSucursalReponse>>.Success(new List<InvMovInventarioSucursalReponse> { MovInventarioSucursal }, "Almacenes encontrado."));
                }
                else
                {
                    var MovInventarioSucursals = await _MovInventarioSucursal.GetAllDto();
                    if (MovInventarioSucursals == null || MovInventarioSucursals.Count == 0)
                    {
                        return NoContent();
                    }
                    return Ok(Response<IEnumerable<InvMovInventarioSucursalReponse>>.Success(MovInventarioSucursals, "Inventarios Sucursales obtenidos correctamente."));
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, Response<string>.ServerError("Ocurrió un error al obtener el Movimiento Inventario Sucursal. Por favor, intente nuevamente."));
            }
        }

        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Crear un nuevo Movimiento Inventario Sucursal", Description = "Crea un nuevo Movimiento Inventario Sucursal en el sistema.")]
        public async Task<IActionResult> Add([FromBody] InvMovInventarioSucursalRequest request)
        {
            var validationResult = await _validator.ValidateAsync(request);

            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return BadRequest(Response<string>.BadRequest(errors, 400));
            }

            try
            {
                var almacenes = await _MovInventarioSucursal.Add(request);
                return StatusCode(201, Response<InvMovInventarioSucursalReponse>.Created(almacenes, "Almacen creado correctamente."));
            }
            catch (Exception ex)
            {
                return StatusCode(500, Response<string>.ServerError("Ocurrió un error al crear un Movimiento Inventario Sucursal. Por favor, intente nuevamente."));
            }
        }

        [HttpPut("{id}")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Actualizar un Movimiento Inventario Sucursal", Description = "Actualiza la información de un Movimiento Inventario Sucursal existente.")]
        public async Task<IActionResult> Update(int id, [FromBody] InvMovInventarioSucursalRequest saveDto)
        {
            var validationResult = await _validator.ValidateAsync(saveDto);

            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return BadRequest(Response<string>.BadRequest(errors, 400));
            }

            try
            {
                var almacenes = await _MovInventarioSucursal.GetByIdResponse(id);
                if (almacenes == null)
                {
                    return NotFound(Response<InvMovInventarioSucursalReponse>.NotFound("Movimiento Inventario Sucursal no encontrado."));
                }
                saveDto.Id = id;
                await _MovInventarioSucursal.Update(saveDto, id);
                return Ok(Response<string>.Success(null, "Movimiento Inventario Sucursal actualizado correctamente"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, Response<string>.ServerError("Ocurrió un error al actualizar el Movimiento Inventario Sucursal. Por favor, intente nuevamente."));
            }
        }
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Eliminar un Movimiento Inventario Sucursal", Description = "Elimina un Movimiento Inventario Sucursal de manera lógica.")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var almacenes = await _MovInventarioSucursal.GetByIdResponse(id);
                if (almacenes == null)
                {
                    return NotFound(Response<string>.NotFound("Movimiento Inventario Sucursal no encontrado."));
                }

                await _MovInventarioSucursal.Delete(id);
                return Ok(Response<string>.Success(null, "Movimiento Inventario Sucursal eliminado correctamente"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, Response<string>.ServerError("Ocurrió un error al eliminar el Movimiento Inventario Sucursal. Por favor, intente nuevamente."));
            }
        }

    }
}
