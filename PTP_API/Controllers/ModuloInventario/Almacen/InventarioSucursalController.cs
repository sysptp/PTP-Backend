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
    [SwaggerTag("Inventario Sucursal")]
    [Authorize]
    [EnableBitacora]
    public class InventarioSucursalController : ControllerBase
    {
        private readonly IInvInventarioSucursalService _inventarioSucursal;
        private readonly IValidator<InvInventarioSucursalRequest> _validator;
        public InventarioSucursalController(IInvInventarioSucursalService inventarioSucursal, IValidator<InvInventarioSucursalRequest> validator)
        {
            _inventarioSucursal = inventarioSucursal;
            _validator = validator;
        }
        [HttpGet]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(Response<IEnumerable<InvInventarioSucursalReponse>>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Obtener Inventario Sucursal", Description = "Obtiene una lista de todos los Inventarios Sucursales o un Inventario Sucursal específico si se proporciona un ID.")]
        [DisableBitacora]
        public async Task<IActionResult> Get([FromQuery] int? id)
        {
            try
            {
                if (id.HasValue)
                {
                    var inventarioSucursal = await _inventarioSucursal.GetByIdResponse(id);
                    if (inventarioSucursal == null)
                    {
                        return NotFound(Response<InvInventarioSucursalReponse>.NotFound("Inventario Sucursal no encontrado."));
                    }
                    return Ok(Response<List<InvInventarioSucursalReponse>>.Success(new List<InvInventarioSucursalReponse> { inventarioSucursal }, "Almacenes encontrado."));
                }
                else
                {
                    var inventarioSucursals = await _inventarioSucursal.GetAllDto();
                    if (inventarioSucursals == null || inventarioSucursals.Count == 0)
                    {
                        return NoContent();
                    }
                    return Ok(Response<IEnumerable<InvInventarioSucursalReponse>>.Success(inventarioSucursals, "Inventarios Sucursales obtenidos correctamente."));
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, Response<string>.ServerError("Ocurrió un error al obtener el Inventario Sucursal. Por favor, intente nuevamente."));
            }
        }

        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Crear un nuevo Inventario Sucursal", Description = "Crea un nuevo Inventario Sucursal en el sistema.")]
        public async Task<IActionResult> Add([FromBody] InvInventarioSucursalRequest request)
        {
            var validationResult = await _validator.ValidateAsync(request);

            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return BadRequest(Response<string>.BadRequest(errors, 400));
            }

            try
            {
                var almacenes = await _inventarioSucursal.Add(request);
                return StatusCode(201, Response<InvInventarioSucursalReponse>.Created(almacenes, "Almacen creado correctamente."));
            }
            catch (Exception ex)
            {
                return StatusCode(500, Response<string>.ServerError("Ocurrió un error al crear un Inventario Sucursal. Por favor, intente nuevamente."));
            }
        }

        [HttpPut("{id}")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Actualizar un Inventario Sucursal", Description = "Actualiza la información de un Inventario Sucursal existente.")]
        public async Task<IActionResult> Update(int id, [FromBody] InvInventarioSucursalRequest saveDto)
        {
            var validationResult = await _validator.ValidateAsync(saveDto);

            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return BadRequest(Response<string>.BadRequest(errors, 400));
            }

            try
            {
                var almacenes = await _inventarioSucursal.GetByIdResponse(id);
                if (almacenes == null)
                {
                    return NotFound(Response<InvInventarioSucursalReponse>.NotFound("Inventario Sucursal no encontrado."));
                }
                saveDto.Id = id;
                await _inventarioSucursal.Update(saveDto, id);
                return Ok(Response<string>.Success(null, "Inventario Sucursal actualizado correctamente"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, Response<string>.ServerError("Ocurrió un error al actualizar el Inventario Sucursal. Por favor, intente nuevamente."));
            }
        }
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Eliminar un Inventario Sucursal", Description = "Elimina un Inventario Sucursal de manera lógica.")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var almacenes = await _inventarioSucursal.GetByIdResponse(id);
                if (almacenes == null)
                {
                    return NotFound(Response<string>.NotFound("Inventario Sucursal no encontrado."));
                }

                await _inventarioSucursal.Delete(id);
                return Ok(Response<string>.Success(null, "Inventario Sucursal eliminado correctamente"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, Response<string>.ServerError("Ocurrió un error al eliminar el Inventario Sucursal. Por favor, intente nuevamente."));
            }
        }

    }
}
