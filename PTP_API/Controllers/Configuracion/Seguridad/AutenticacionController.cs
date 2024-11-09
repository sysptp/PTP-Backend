using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using BussinessLayer.Interface.IAccount;
using System.Net.Mime;
using FluentValidation;
using BussinessLayer.DTOs.Configuracion.Seguridad.Autenticacion;
using BussinessLayer.DTOs.Configuracion.Account;
using BussinessLayer.Wrappers;

namespace PTP_API.Controllers.Configuration.Seguridad
{
    [ApiController]
    [Route("api/v1/[controller]")]
    [SwaggerTag("Servicio de manejo de usuarios")]
    public class AutenticacionController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly IValidator<RegisterRequest> _validator;
        private readonly IValidator<LoginRequestDTO> _validatorLogin;

        public AutenticacionController(IAccountService accountService, IValidator<LoginRequestDTO> validatorLogin, IValidator<RegisterRequest> validator)
        {
            _accountService = accountService;
            _validatorLogin = validatorLogin;
            _validator = validator;
        }

        [HttpPost("Login")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [SwaggerOperation(Summary = "Logear empresa", Description = "Devuelve el token de seguridad")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDTO request)
        {

            var validationResult = await _validatorLogin.ValidateAsync(request);

            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return BadRequest(Response<string>.BadRequest(errors, 400));
            }

            try
            {
                var response = await _accountService.AuthenticateAsync(new AuthenticationRequest
                {
                    UserCredential = request.User,
                    Password = request.Password
                });

                if (response.HasError)
                {
                    return BadRequest(Response<string>.BadRequest(new List<string> { response.Error ?? "Error en el Login" }, 400));
                }

                return Ok(Response<object>.Success(response, "Autenticación exitosa"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, Response<string>.ServerError("Ocurrió un error inesperado durante el proceso de autenticación. Detalle: " + ex.Message));
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
                var validationResult = await _validator.ValidateAsync(request);

                if (!validationResult.IsValid)
                {
                    var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                    return BadRequest(Response<string>.BadRequest(errors, 400));
                }

                var origin = Request?.Headers["origin"].ToString() ?? string.Empty;
                var registrationResponse = await _accountService.RegisterUserAsync(request, origin);

                return Ok(Response<object>.Created(registrationResponse, "Registro de usuario exitoso"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, Response<string>.ServerError("Ocurrió un error inesperado durante el proceso de registro. Detalle: " + ex.Message));
            }
        }
    }
}
