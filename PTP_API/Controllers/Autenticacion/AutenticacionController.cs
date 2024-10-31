using Microsoft.AspNetCore.Mvc;
using BussinessLayer.DTOs.Autenticacion;
using Swashbuckle.AspNetCore.Annotations;
using BussinessLayer.Dtos.Account;
using BussinessLayer.Interface.IAccount;
using System.Net.Mime;
using BussinessLayer.Wrappers;
using BussinessLayer.FluentValidations.Account;

namespace PTP_API.Controllers.Autenticacion
{
    [ApiController]
    [Route("api/v1/[controller]")]
    [SwaggerTag("Servicio de manejo de usuarios")]
    public class AutenticacionController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AutenticacionController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost("Login")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [SwaggerOperation(Summary = "Logear empresa", Description = "Devuelve el token de seguridad")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDTO request)
        {
            var validator = new LoginRequestValidator();
            var validationResult = await validator.ValidateAsync(request);

            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return BadRequest(Response<string>.BadRequest(errors, 400));
            }

            try
            {
                var response = await _accountService.AuthenticateAsync(new AuthenticationRequest
                {
                    UserCredential = request.Usuario,
                    Password = request.Password
                });

                if (response.HasError)
                {
                    return Unauthorized(Response<string>.Unauthorized(response.Error));
                }

                return Ok(Response<object>.Success(response, "Autenticación exitosa"));
            }
            catch (Exception ex)
            {
                return Ok(Response<string>.ServerError("Ocurrió un error inesperado durante el proceso de autenticación. Detalle: " + ex.Message));
            }
        }

        [HttpPost("Register")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [SwaggerOperation(Summary = "Registro de Usuarios", Description = "Endpoint para registrar los usuarios")]
        public async Task<IActionResult> RegisterAsync([FromBody] RegisterRequest request)
        {
            try
            {
                var validator = new RegisterRequestValidator();
                var validationResult = await validator.ValidateAsync(request);

                if (!validationResult.IsValid)
                {
                    var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                    return BadRequest(Response<string>.BadRequest(errors, 400));
                }

                var origin = Request?.Headers["origin"].ToString() ?? string.Empty;
                var registrationResponse = await _accountService.RegisterUserAsync(request, origin, "Developer");

                return Ok(Response<object>.Created(registrationResponse, "Registro de usuario exitoso"));
            }
            catch (Exception ex)
            {
                return Ok(Response<string>.ServerError("Ocurrió un error inesperado durante el proceso de registro. Detalle: " + ex.Message));
            }
        }
    }
}
