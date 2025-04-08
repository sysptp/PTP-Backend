
using System.Net.Mime;
using BussinessLayer.Atributes;
using BussinessLayer.DTOs.ModuloGeneral.Configuracion.Account;
using BussinessLayer.DTOs.ModuloGeneral.Seguridad.Autenticacion;
using BussinessLayer.DTOs.ModuloGeneral.Seguridad.Usuario;
using BussinessLayer.Interfaces.Services.IAccount;
using BussinessLayer.Interfaces.Services.ModuloGeneral.Seguridad;
using BussinessLayer.Wrappers;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace PTP_API.Controllers.ModuloGeneral.Seguridad
{
    [Route("api/v1/[controller]")]
    [Authorize]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;
        private readonly IValidator<RegisterRequest> _validator;
        private readonly IAccountService _accountService;

        public UserController(IUsuarioService usuarioService, IValidator<RegisterRequest> validator, IAccountService accountService)
        {
            _usuarioService = usuarioService;
            _validator = validator;
            _accountService = accountService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(Response<IEnumerable<UserResponse>>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Obtener Usuarios", Description = "Devuelve una lista de usuarios o un usuario específico si se proporciona un ID")]
        public async Task<IActionResult> GetAllUsers([FromQuery] int? id, long? companyId, long? sucursalId, int? roleId, bool? areActive)
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
                    var users = await _usuarioService.GetAllWithFilters(companyId, sucursalId, roleId, areActive);
                    if (users == null || !users.Any())
                    {
                        return NoContent();
                    }
                    return Ok(Response<IEnumerable<UserResponse>>.Success(users, "Lista de usuarios obtenida correctamente."));
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, Response<string>.ServerError(ex.Message));
            }
        }


        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Actualizar Usuario", Description = "Endpoint para actualizar los datos de un usuario")]
        [EnableBitacora]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] UpdateUserRequest userRequest)
        {
            //var validationResult = await _validator.ValidateAsync(permisoDto);

            //if (!validationResult.IsValid)
            //{
            //    var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
            //    return BadRequest(Response<string>.BadRequest(errors, 400));
            //}

            try
            {
                var existingUser = await _usuarioService.GetByIdResponse(id);
                if (existingUser == null)
                    return NotFound(Response<string>.NotFound("usuario no encontrado"));

                userRequest.Id = id;
                await _usuarioService.UpdateUser(userRequest);
                return Ok(Response<string>.Success(null, "usuario actualizado correctamente"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, Response<string>.ServerError(ex.Message));
            }
        }

        [HttpPost("CreateUser")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [SwaggerOperation(Summary = "Creación de Usuarios", Description = "Endpoint para crear los usuarios")]
        public async Task<IActionResult> AddAsync([FromBody] RegisterRequest request)
        {
            try
            {
                var validationResult = await _validator.ValidateAsync(request);

                if (!validationResult.IsValid)
                {
                    var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                    return BadRequest(Response<string>.BadRequest(errors, 400));
                }

                var origin = Request?.Headers["origin"].ToString() ?? string.Empty;
                var registrationResponse = await _accountService.RegisterUserAsync(request, origin);

                return registrationResponse.HasError ? BadRequest(Response<string>.BadRequest(new List<string> { registrationResponse?.Error }, 400))
                    : Ok(Response<object>.Created(registrationResponse, "Registro de usuario exitoso"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, Response<string>.ServerError(ex.Message));
            }
        }


    }
}
