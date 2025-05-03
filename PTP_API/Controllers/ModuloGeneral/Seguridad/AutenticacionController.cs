using BussinessLayer.DTOs.Account;
using BussinessLayer.DTOs.ModuloGeneral.Configuracion.Account;
using BussinessLayer.DTOs.ModuloGeneral.Seguridad.Autenticacion;
using BussinessLayer.Interfaces.Services.IAccount;
using BussinessLayer.Wrappers;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net.Mime;

namespace PTP_API.Controllers.ModuloGeneral.Seguridad
{
    [ApiController]
    [Route("api/v1/[controller]")]
    [SwaggerTag("Servicio de manejo de usuarios")]
    public class AutenticacionController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly IValidator<RegisterRequest> _validator;
        private readonly IValidator<LoginRequestDTO> _validatorLogin;
        private readonly IValidator<ForgotPasswordRequest> _forgotPasswordValidator;
        private readonly IValidator<ResetPasswordRequest> _resetPasswordValidator;

        public AutenticacionController(
            IAccountService accountService,
            IValidator<RegisterRequest> validator,
            IValidator<ForgotPasswordRequest> forgotPasswordValidator,
            IValidator<ResetPasswordRequest> resetPasswordValidator)
        {
            _accountService = accountService;
            _validator = validator;
            _forgotPasswordValidator = forgotPasswordValidator;
            _resetPasswordValidator = resetPasswordValidator;
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
                return StatusCode(500, Response<string>.ServerError(ex.Message));
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

                return registrationResponse.HasError ? BadRequest(Response<string>.BadRequest(new List<string> { registrationResponse?.Error }, 400))
                    : Ok(Response<object>.Created(registrationResponse, "Registro de usuario exitoso"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, Response<string>.ServerError(ex.Message));
            }
        }

        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordRequest request)
        {
            try
            {
                var validationResult = await _forgotPasswordValidator.ValidateAsync(request);
                if (!validationResult.IsValid)
                {
                    var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                    return BadRequest(Response<string>.BadRequest(errors, 400));
                }

                // Obtener el origen desde el encabezado Origin o Referer
                var origin = Request.Headers["Origin"].ToString();
                if (string.IsNullOrEmpty(origin))
                {
                    origin = Request.Headers["Referer"].ToString();
                }
                if (string.IsNullOrEmpty(origin))
                {
                    origin = "https://ptp-frontend-dev.vercel.app";
                }

                var response = await _accountService.ForgotPasswordAsync(request, origin);
                if (response.HasError)
                {
                    return BadRequest(Response<string>.BadRequest(new List<string> { response.Error }, 400));
                }

                return Ok(Response<string>.Success("Se ha enviado un enlace de restablecimiento a tu correo electrónico"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, Response<string>.ServerError("Ha ocurrido un error inesperado. Por favor, inténtalo más tarde."));
            }
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordRequest request)
        {
            try
            {
                var validationResult = await _resetPasswordValidator.ValidateAsync(request);
                if (!validationResult.IsValid)
                {
                    var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                    return BadRequest(Response<string>.BadRequest(errors, 400));
                }

                var response = await _accountService.ResetPasswordAsync(request);
                if (response.HasError)
                {
                    return BadRequest(Response<string>.BadRequest(new List<string> { response.Error }, 400));
                }

                return Ok(Response<string>.Success("Contraseña restablecida con éxito"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, Response<string>.ServerError("Ha ocurrido un error inesperado al restablecer la contraseña. Por favor, inténtalo más tarde."));
            }
        }

        //[HttpGet("ConfirmEmail")]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //[SwaggerOperation(Summary = "Confirmar correo electrónico", Description = "Confirma el correo electrónico del usuario")]
        //public async Task<IActionResult> ConfirmEmail([FromQuery] string userId, [FromQuery] string token)
        //{
        //    try
        //    {
        //        var result = await _accountService.ConfirmAccountAsync(userId, token);

        //        if (result.Contains("error", StringComparison.OrdinalIgnoreCase))
        //        {
        //            return BadRequest(Response<string>.BadRequest(new List<string> { result }, 400));
        //        }

        //        return Ok(Response<string>.Success(result));
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, Response<string>.ServerError(ex.Message));
        //    }
        //}

        //[HttpPost("RegisterExternal")]
        //[Consumes(MediaTypeNames.Application.Json)]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //[SwaggerOperation(Summary = "Registro de usuarios mediante proveedores externos", Description = "Endpoint para registrar usuarios utilizando Google, Microsoft o Facebook")]
        //public async Task<IActionResult> RegisterExternalAsync([FromBody] ExternalRegisterRequest request)
        //{
        //    try
        //    {
        //        var response = await _accountService.RegisterExternalUserAsync(request);

        //        if (response.HasError)
        //        {
        //            return BadRequest(Response<string>.BadRequest(new List<string> { response.Error }, 400));
        //        }

        //        return Ok(Response<object>.Created(response, "Registro de usuario externo exitoso"));
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, Response<string>.ServerError(ex.Message));
        //    }
        //}

        //[HttpPost("ExternalLogin")]
        //[Consumes(MediaTypeNames.Application.Json)]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //[SwaggerOperation(Summary = "Autenticación externa", Description = "Autentica al usuario mediante proveedores externos (Google, Microsoft, Facebook)")]
        //public async Task<IActionResult> ExternalLogin([FromBody] ExternalLoginRequest request)
        //{
        //    try
        //    {
        //        var response = await _accountService.AuthenticateExternalAsync(request.Provider, request.Token);

        //        if (response.HasError)
        //        {
        //            return BadRequest(Response<string>.BadRequest(new List<string> { response.Error }, 400));
        //        }

        //        return Ok(Response<object>.Success(response, "Autenticación externa exitosa"));
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, Response<string>.ServerError(ex.Message));
        //    }
        //}
    }
}
