using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using Swashbuckle.AspNetCore.Annotations;
using BussinessLayer.Wrappers;
using Microsoft.AspNetCore.Authorization;
using BussinessLayer.Interfaces.IEmpresa;
using BussinessLayer.DTOs.ModuloGeneral.Sucursal;
using FluentValidation;
using BussinessLayer.DTOs.ModuloGeneral.Empresas;
using BussinessLayer.Services.SEmpresa;

namespace PTP_API.Controllers.ModuloGeneral.Empresa
{
    [ApiController]
    [Route("api/v1/Sucursal")]
    [SwaggerTag("Gestión de Sucursales")]
    [Authorize]
    public class SucursalController : ControllerBase
    {
        private readonly IGnSucursalService _sucursalService;
        private readonly IValidator<GnSucursalRequest> _validator;

        public SucursalController(IGnSucursalService sucursalService, IValidator<GnSucursalRequest> validator)
        {
            _sucursalService = sucursalService;
            _validator = validator;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [SwaggerOperation(Summary = "Obtener sucursales", Description = "Obtiene una lista de todas las sucursales o una sucursal específica si se proporciona un ID.")]
        public async Task<IActionResult> Get([FromQuery] long? id, long? companyId)
        {
            try
            {
                if (id.HasValue)
                {
                    var sucursal = await _sucursalService.GetByIdResponse(id);
                    if (sucursal == null)
                    {
                        return NotFound(Response<GnSucursalResponse>.NotFound("Sucursal no encontrada."));
                    }
                    return Ok(Response<List<GnSucursalResponse>>.Success(new List<GnSucursalResponse> { sucursal }, "Sucursal encontrada."));
                }
                else
                {
                    var sucursales = await _sucursalService.GetAllDto();
                    if (sucursales == null || !sucursales.Any())
                    {
                        return StatusCode(204, Response<IEnumerable<GnSucursalResponse>>.NoContent("No hay sucursales disponibles."));
                    }
                    return Ok(Response<IEnumerable<GnSucursalResponse>>.Success(companyId == null ? sucursales : sucursales.Where(x => x.CompanyId == companyId), "Sucursales obtenidas correctamente."));
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, Response<string>.ServerError("Ocurrió un error al obtener las sucursales. Por favor, intente nuevamente."));
            }
        }

        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [SwaggerOperation(Summary = "Crear una nueva sucursal", Description = "Crea una nueva sucursal en el sistema.")]
        public async Task<IActionResult> Add([FromBody] GnSucursalRequest request)
        {
            var validationResult = await _validator.ValidateAsync(request);
          
            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return BadRequest(Response<string>.BadRequest(errors, 400));
            }

            try
            {
                var sucursal = await _sucursalService.Add(request);
                return Ok(Response<GnSucursalResponse>.Created(sucursal, "Sucursal creada correctamente."));
            }
            catch (Exception ex)
            {
                return StatusCode(500, Response<string>.ServerError("Ocurrió un error al crear la sucursal. Por favor, intente nuevamente."));
            }
        }

        [HttpPut("{id}")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Actualizar una sucursal", Description = "Actualiza la información de una sucursal existente.")]
        public async Task<IActionResult> Update(long id, [FromBody] GnSucursalRequest saveDto)
        {

            saveDto.CodigoSuc = id;
            var validationResult = await _validator.ValidateAsync(saveDto);

            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return BadRequest(Response<string>.BadRequest(errors, 400));
            }

            try
            {
                var existingSucursal = await _sucursalService.GetByIdResponse(id);
                if (existingSucursal == null)
                {
                    return NotFound(Response<GnEmpresaResponse>.NotFound("Sucursal no encontrada."));
                }
                await _sucursalService.Update(saveDto, id);
                return Ok(Response<string>.Success(null, "Sucursal actualizada correctamente"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, Response<string>.ServerError("Ocurrió un error al actualizar la sucursal. Por favor, intente nuevamente."));
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Eliminar una sucursal", Description = "Elimina una sucursal de manera lógica.")]
        public async Task<IActionResult> Delete(long id)
        {
            try
            {
                var sucursal = await _sucursalService.GetByIdResponse(id);
                if (sucursal == null)
                {
                    return NotFound(Response<string>.NotFound("Sucursal no encontrada."));
                }

                await _sucursalService.Delete(id);
                return Ok(Response<string>.Success(null, "Sucursal eliminada correctamente"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, Response<string>.ServerError("Ocurrió un error al eliminar la Sucursal. Por favor, intente nuevamente."));
            }
        }
    }
}
