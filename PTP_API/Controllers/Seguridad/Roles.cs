using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using BussinessLayer.DTOs.Seguridad;
using BussinessLayer.Wrappers;
using System.Net.Mime;
using BussinessLayer.Interfaces.ISeguridad;
using BussinessLayer.FluentValidations.Seguridad;
using Microsoft.AspNetCore.Authorization;

namespace PTP_API.Controllers.Seguridad
{
    [ApiController]
    [Route("api/v1/[controller]")]
    [SwaggerTag("Servicio de manejo de roles")]
    [Authorize]
    public class RolesController : ControllerBase
    {
        private readonly IGnPerfilService _rolesService;

        public RolesController(IGnPerfilService rolesService)
        {
            _rolesService = rolesService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(Response<IEnumerable<GnPerfilResponse>>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Obtener perfiles", Description = "Devuelve una lista de perfiles o un perfil específico si se proporciona un ID")]
        public async Task<IActionResult> GetAllRoles([FromQuery] int? id)
        {
            try
            {
                if (id.HasValue)
                {
                    var perfil = await _rolesService.GetByIdResponse(id.Value);
                    if (perfil == null)
                    {
                        return NotFound(Response<GnPerfilResponse>.NotFound("Perfil no encontrado."));
                    }
                    return Ok(Response<GnPerfilResponse>.Success(perfil, "Perfil encontrado."));
                }
                else
                {
                    var perfiles = await _rolesService.GetAllDto();
                    if (perfiles == null || !perfiles.Any())
                    {
                        return Ok(Response<IEnumerable<GnPerfilResponse>>.NoContent("No se encontraron perfiles disponibles."));
                    }
                    return Ok(Response<IEnumerable<GnPerfilResponse>>.Success(perfiles, "Lista de perfiles obtenida correctamente."));
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, Response<string>.ServerError("Ocurrió un error al obtener los perfiles. Por favor, intente nuevamente."));
            }
        }

        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [SwaggerOperation(Summary = "Crear un nuevo rol", Description = "Endpoint para crear un rol nuevo")]
        public async Task<IActionResult> CreateRole([FromBody] GnPerfilRequest roleDto)
        {
            try
            {
                var validator = new GnPerfilRequestValidator();
                var validationResult = await validator.ValidateAsync(roleDto);

                if (!validationResult.IsValid)
                {
                    var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                    return BadRequest(Response<string>.BadRequest(errors, 400));
                }

                var createdRole = await _rolesService.Add(roleDto);
                return CreatedAtAction(nameof(GetAllRoles), new { id = createdRole }, Response<object>.Success(createdRole, "Rol creado exitosamente"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, Response<string>.ServerError("Ocurrió un error al crear el rol. Por favor, intente más tarde."));
            }
        }

        //[HttpPatch("{id}")]
        //[ProducesResponseType(StatusCodes.Status204NoContent)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //[ProducesResponseType(StatusCodes.Status404NotFound)]
        //[SwaggerOperation(Summary = "Actualizar rol", Description = "Endpoint para actualizar los datos de un rol")]
        //public async Task<IActionResult> UpdateRole(int id, [FromBody] GnPerfilRequest roleDto)
        //{
        //    var validator = new GnPerfilRequestValidator();
        //    var validationResult = await validator.ValidateAsync(roleDto);

        //    if (!validationResult.IsValid)
        //    {
        //        var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
        //        return BadRequest(Response<string>.BadRequest(errors, 400));
        //    }

        //    try
        //    {
        //        var existingRole = await _rolesService.GetByIdRequest(id);
        //        if (existingRole == null)
        //            return Ok(Response<string>.NotFound("Rol no encontrado"));

        //        await _rolesService.Update(roleDto, id);
        //        return NoContent();
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, Response<string>.ServerError("Ocurrió un error al actualizar el rol. Por favor, intente más tarde."));
        //    }
        //}

        //[HttpDelete("{id}")]
        //[ProducesResponseType(StatusCodes.Status204NoContent)]
        //[ProducesResponseType(StatusCodes.Status404NotFound)]
        //[SwaggerOperation(Summary = "Eliminar rol", Description = "Endpoint para eliminar un rol")]
        //public async Task<IActionResult> DeleteRole(int id)
        //{
        //    try
        //    {
        //        var existingRole = await _rolesService.GetByIdRequest(id);
        //        if (existingRole == null)
        //            return Ok(Response<string>.NotFound("Rol no encontrado"));

        //        await _rolesService.Delete(id);
        //        return NoContent();
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500,Response<string>.ServerError("Ocurrió un error al eliminar el rol. Por favor, intente más tarde."));
        //    }
        //}
    }
}
