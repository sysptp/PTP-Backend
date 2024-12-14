using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using BussinessLayer.Wrappers;
using System.Net.Mime;
using Microsoft.AspNetCore.Authorization;
using FluentValidation;
<<<<<<<< HEAD:PTP_API/Controllers/ModuloGeneral/Configuracion/Seguridad/RolesController.cs
using BussinessLayer.Atributes;
using BussinessLayer.DTOs.ModuloGeneral.Configuracion.Seguridad;
========
using BussinessLayer.Interfaces.ModuloGeneral.Seguridad;
using BussinessLayer.DTOs.ModuloGeneral.Seguridad.Perfil;
>>>>>>>> REFACTOR:PTP_API/Controllers/ModuloGeneral/Seguridad/RolesController.cs

namespace PTP_API.Controllers.ModuloGeneral.Seguridad
{
    [ApiController]
    [Route("api/v1/[controller]")]
    [SwaggerTag("Servicio de manejo de roles")]
    [Authorize]
    [EnableAuditing]
    public class RolesController : ControllerBase
    {
        private readonly IGnPerfilService _rolesService;
        private readonly IValidator<GnPerfilRequest> _validator;

        public RolesController(IGnPerfilService rolesService, IValidator<GnPerfilRequest> validator)
        {
            _rolesService = rolesService;
            _validator = validator;
        }

        [HttpGet]
        [ProducesResponseType(typeof(Response<IEnumerable<GnPerfilResponse>>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Obtener perfiles", Description = "Devuelve una lista de perfiles o un perfil específico si se proporciona un ID")]
        [DisableAuditing]
        public async Task<IActionResult> GetAllRoles([FromQuery] int? id, int? companyId)
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
                    return Ok(Response<List<GnPerfilResponse>>.Success(new List<GnPerfilResponse> { perfil }, "Perfil encontrado."));
                }
                else
                {
                    var perfiles = await _rolesService.GetAllDto();
                    if (perfiles == null || !perfiles.Any())
                    {
                        return NoContent();
                    }
                    return Ok(Response<IEnumerable<GnPerfilResponse>>.Success(companyId == null ? perfiles : perfiles.Where(x => x.CompanyId == companyId), "Lista de perfiles obtenida correctamente."));
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
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Crear un nuevo rol", Description = "Endpoint para crear un rol nuevo")]
        public async Task<IActionResult> CreateRole([FromBody] GnPerfilRequest roleDto)
        {
            try
            {
                var validationResult = await _validator.ValidateAsync(roleDto);

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

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Actualizar rol", Description = "Endpoint para actualizar los datos de un rol")]
        public async Task<IActionResult> UpdateRole(int id, [FromBody] GnPerfilRequest roleDto)
        {
            var validationResult = await _validator.ValidateAsync(roleDto);

            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return BadRequest(Response<string>.BadRequest(errors, 400));
            }

            try
            {
                var existingRole = await _rolesService.GetByIdRequest(id);
                if (existingRole == null)
                    return StatusCode(404, Response<string>.NotFound("Rol no encontrado"));

                roleDto.Id = id;
                await _rolesService.Update(roleDto, id);
                return Ok(Response<string>.Success(null, "Rol actualizado correctamente"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, Response<string>.ServerError("Ocurrió un error al actualizar el rol. Por favor, intente más tarde."));
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Eliminar rol", Description = "Endpoint para eliminar un rol")]
        public async Task<IActionResult> DeleteRole(int id)
        {
            try
            {
                var existingRole = await _rolesService.GetByIdRequest(id);
                if (existingRole == null)
                    return NotFound(Response<string>.NotFound("Rol no encontrado"));

                await _rolesService.Delete(id);
                return Ok(Response<string>.Success(null, "Empresa eliminada correctamente"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, Response<string>.ServerError("Ocurrió un error al eliminar el rol. Por favor, intente más tarde."));
            }
        }
    }
}
