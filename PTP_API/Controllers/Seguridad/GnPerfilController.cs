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
    //[Authorize]
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
        [SwaggerOperation(Summary = "Obtener todos los roles", Description = "Devuelve una lista de roles")]
        public async Task<IActionResult> GetAllRoles()
        {
            try
            {
                var rolesList = await _rolesService.GetAllDto();

                if (rolesList == null || !rolesList.Any())
                    return Ok(Response<object>.NoContent("No se encontraron roles."));

                return Ok(Response<object>.Success(rolesList, "Lista de roles obtenida correctamente"));
            }
            catch (Exception ex)
            {
                return Ok(Response<string>.ServerError("Ocurrió un error al obtener los roles. Por favor, intente más tarde."));
            }
        }

        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [SwaggerOperation(Summary = "Crear un nuevo rol", Description = "Endpoint para crear un rol nuevo")]
        public async Task<IActionResult> CreateRole([FromBody] GnPerfilRequest roleDto)
        {
            var validator = new GnPerfilRequestValidator();
            var validationResult = await validator.ValidateAsync(roleDto);

            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return BadRequest(Response<string>.BadRequest(errors, 400));
            }

            try
            {
                var createdRole = await _rolesService.Add(roleDto);
                return CreatedAtAction(nameof(GetAllRoles), new { id = createdRole }, Response<object>.Success(createdRole, "Rol creado exitosamente"));
            }
            catch (Exception ex)
            {
                return Ok(Response<string>.ServerError("Ocurrió un error al crear el rol. Por favor, intente más tarde."));
            }
        }

        [HttpPatch("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [SwaggerOperation(Summary = "Actualizar rol", Description = "Endpoint para actualizar los datos de un rol")]
        public async Task<IActionResult> UpdateRole(int id, [FromBody] GnPerfilRequest roleDto)
        {
            var validator = new GnPerfilRequestValidator();
            var validationResult = await validator.ValidateAsync(roleDto);

            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return BadRequest(Response<string>.BadRequest(errors, 400));
            }

            try
            {
                var existingRole = await _rolesService.GetByIdRequest(id);
                if (existingRole == null)
                    return Ok(Response<string>.NotFound("Rol no encontrado"));

                await _rolesService.Update(roleDto, id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return Ok(Response<string>.ServerError("Ocurrió un error al actualizar el rol. Por favor, intente más tarde."));
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [SwaggerOperation(Summary = "Eliminar rol", Description = "Endpoint para eliminar un rol")]
        public async Task<IActionResult> DeleteRole(int id)
        {
            try
            {
                var existingRole = await _rolesService.GetByIdRequest(id);
                if (existingRole == null)
                    return Ok(Response<string>.NotFound("Rol no encontrado"));

                await _rolesService.Delete(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return Ok(Response<string>.ServerError("Ocurrió un error al eliminar el rol. Por favor, intente más tarde."));
            }
        }
    }
}
