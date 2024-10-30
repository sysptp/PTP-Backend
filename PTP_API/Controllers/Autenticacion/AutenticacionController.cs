using Microsoft.AspNetCore.Mvc;
using BussinessLayer.DTOs.Autenticacion;
using Swashbuckle.AspNetCore.Annotations;
using BussinessLayer.Dtos.Account;
using BussinessLayer.Interface.IAccount;
using System.Net.Mime;
using BussinessLayer.FluentValidations;
using BussinessLayer.FluentValidations.Account;

namespace PTP_API.Controllers.Autenticacion
{
   
    [ApiController]
    [ApiVersion("1.0")]
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
        [SwaggerOperation(Summary = "Logear empresa", Description = "Devuelve el token de seguridad")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDTO request)
        {
            if (string.IsNullOrEmpty(request.Usuario))
            {
                return BadRequest("El campo usuario no puede estar vacio.");
            }

            if (string.IsNullOrEmpty(request.Password))
            {
                return BadRequest("El campo contraseña no puede estar vacio.");
            }

            var response = await _accountService.AuthenticateAsync(new AuthenticationRequest
            {
                UserCredential = request.Usuario,
                Password = request.Password
            });

            if (response.HasError)
            {
                return Unauthorized(response.Error);
            }

            return Ok(new
            {
                Token = response.JWToken,
                RefreshToken = response.RefreshToken
            });
        }

        [HttpPost("Register")]
        [Consumes(MediaTypeNames.Application.Json)]
        [SwaggerOperation(
            Summary = "Registro de Usuarios",
            Description = "Endpoint para registrar los usuarios"
        )]
        public async Task<IActionResult> RegisterAsync([FromBody] RegisterRequest request)
        {
            var validator = new RegisterRequestValidator();
            var validationResult = await validator.ValidateAsync(request);

            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return BadRequest(new { code = 400, error = errors });
            }
            var origin = Request?.Headers["origin"].ToString() ?? string.Empty;

            return Ok(await _accountService.RegisterUserAsync(request, origin, "Developer"));
        }
    }
}
