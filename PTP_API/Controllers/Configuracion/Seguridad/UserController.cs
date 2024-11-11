using BussinessLayer.DTOs.Configuracion.Seguridad.Usuario;
using BussinessLayer.Interfaces.ISeguridad;
using BussinessLayer.Wrappers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace PTP_API.Controllers.Configuracion.Seguridad
{
    [Route("api/v1/[controller]")]
    [Authorize]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;

        public UserController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(Response<IEnumerable<UserResponse>>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Obtener Usuarios", Description = "Devuelve una lista de usuarios o un usuario específico si se proporciona un ID")]
        public async Task<IActionResult> GetAllUsers([FromQuery] int? id, long? companyId, long? sucursalId, int roleId)
        {
            try
            {
                if (id.HasValue)
                {
                    var user = await _usuarioService.GetByIdResponse(id.Value);
                    if (user == null)
                    {
                        return NotFound(Response<UserResponse>.NotFound("Perfil no encontrado."));
                    }
                    return Ok(Response<List<UserResponse>>.Success(new List<UserResponse> { user }, "usuario encontrado."));
                }
                else
                {
                    var users = await _usuarioService.GetAllWithFilters(companyId,sucursalId,roleId);
                    if (users == null || !users.Any())
                    {
                        return NoContent();
                    }
                    return Ok(Response<IEnumerable<UserResponse>>.Success(users, "Lista de usuarios obtenida correctamente."));
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, Response<string>.ServerError("Ocurrió un error al obtener los usuarios. Por favor, intente nuevamente."));
            }
        }
    }
}
