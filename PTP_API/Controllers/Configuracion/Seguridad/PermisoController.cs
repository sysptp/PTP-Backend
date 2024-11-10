using BussinessLayer.DTOs.Configuracion.Seguridad.Permiso;
using BussinessLayer.Interfaces.ISeguridad;
using BussinessLayer.Wrappers;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net.Mime;

namespace PTP_API.Controllers.Configuracion.Seguridad
{
    [ApiController]
    [SwaggerTag("Servicio de manejo de permisos")]
    [Route("api/v1/[controller]")]
    [Authorize]

    public class PermisoController : ControllerBase
    {
        private readonly IGnPermisoService _gnPermisoService;
        private readonly IValidator<GnPermisoRequest> _validator;

        public PermisoController(IGnPermisoService gnPermisoService, IValidator<GnPermisoRequest> validator)
        {
            _gnPermisoService = gnPermisoService;
            _validator = validator;
        }

        [HttpGet]
        [ProducesResponseType(typeof(Response<IEnumerable<GnPermisoResponse>>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Obtener permisos", Description = "Devuelve una lista de permisos o un permiso específico si se proporciona un ID")]
        public async Task<IActionResult> GetAllPermissions([FromQuery] int? id)
        {
            try
            {
                if (id.HasValue)
                {
                    var permiso = await _gnPermisoService.GetByIdResponse(id.Value);
                    if (permiso == null)
                    {
                        return NotFound(Response<GnPermisoResponse>.NotFound("Permiso no encontrado."));
                    }
                    return Ok(Response<List<GnPermisoResponse>>.Success(new List<GnPermisoResponse> { permiso }, "Permiso encontrado."));
                }
                else
                {
                    var permisos = await _gnPermisoService.GetAllDto();
                    if (permisos == null || !permisos.Any())
                    {
                        return NoContent();
                    }
                    return Ok(Response<IEnumerable<GnPermisoResponse>>.Success(permisos, "Lista de permisos obtenida correctamente."));
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, Response<string>.ServerError("Ocurrió un error al obtener los permisos. Por favor, intente nuevamente."));
            }
        }

        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Crear un nuevo permiso", Description = "Endpoint para crear un permiso nuevo")]
        public async Task<IActionResult> CreatePermission([FromBody] GnPermisoRequest permisoDto)
        {
            try
            {
                var validationResult = await _validator.ValidateAsync(permisoDto);

                if (!validationResult.IsValid)
                {
                    var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                    return BadRequest(Response<string>.BadRequest(errors, 400));
                }

                var createdPermission = await _gnPermisoService.Add(permisoDto);
                return CreatedAtAction(nameof(GetAllPermissions), new { id = createdPermission }, Response<object>.Success(createdPermission, "Permiso creado exitosamente"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, Response<string>.ServerError("Ocurrió un error al crear el permiso. Por favor, intente más tarde."));
            }
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Actualizar permiso", Description = "Endpoint para actualizar los datos de un permiso")]
        public async Task<IActionResult> UpdatePermission(int id, [FromBody] GnPermisoRequest permisoDto)
        {
            var validationResult = await _validator.ValidateAsync(permisoDto);

            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return BadRequest(Response<string>.BadRequest(errors, 400));
            }

            try
            {
                var existingPermission = await _gnPermisoService.GetByIdRequest(id);
                if (existingPermission == null)
                    return NotFound(Response<string>.NotFound("Permiso no encontrado"));

                permisoDto.IDPermiso = id;
                await _gnPermisoService.Update(permisoDto, id);
                return Ok(Response<string>.Success(null, "Permiso actualizado correctamente"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, Response<string>.ServerError("Ocurrió un error al actualizar el permiso. Por favor, intente más tarde."));
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Eliminar permiso", Description = "Endpoint para eliminar un permiso")]
        public async Task<IActionResult> DeletePermission(int id)
        {
            try
            {
                var existingPermission = await _gnPermisoService.GetByIdRequest(id);
                if (existingPermission == null)
                    return NotFound(Response<string>.NotFound("Permiso no encontrado"));

                await _gnPermisoService.Delete(id);
                return Ok(Response<string>.Success(null, "Permiso eliminado correctamente"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, Response<string>.ServerError("Ocurrió un error al eliminar el permiso. Por favor, intente más tarde."));
            }
        }
    }
}
