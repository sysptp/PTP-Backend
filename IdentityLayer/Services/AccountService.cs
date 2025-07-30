using Microsoft.AspNetCore.Identity;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using IdentityLayer.Entities;
using BussinessLayer.Settings;
using System.Security.Cryptography;
using BussinessLayer.DTOs.Account;
using BussinessLayer.DTOs.ModuloGeneral.Configuracion.Account;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.WebUtilities;
using BussinessLayer.Interfaces.Services.IAccount;
using BussinessLayer.Interfaces.Services.ModuloGeneral.Email;
using BussinessLayer.Interfaces.Services.ModuloGeneral.Seguridad;
using BussinessLayer.DTOs.ModuloGeneral.Email;
using Usuario = IdentityLayer.Entities.Usuario;
using BussinessLayer.DTOs.ModuloGeneral.Seguridad.GnSecurityParameters;
using GnPerfil = IdentityLayer.Entities.GnPerfil;

namespace IdentityLayer.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<Usuario> _userManager;
        private readonly JWTSettings _jwtSettings;
        private readonly RoleManager<GnPerfil> _roleManager;
        private readonly IGnEmailService _emailService;
        //private readonly TokenVerificationFactory _tokenVerificationFactory;
        private readonly IGnPermisoService _gnmisoService;
        private readonly IGnSecurityParametersService _securityParametersService;

        public AccountService(
              UserManager<Usuario> userManager,
              RoleManager<GnPerfil> roleManager,
              IOptions<JWTSettings> jwtSettings,
              IGnEmailService emailService
,
              IGnPermisoService gnmisoService
,
              IGnSecurityParametersService securityParametersService
/*TokenVerificationFactory tokenVerificationFactory*/)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _jwtSettings = jwtSettings.Value;
            _emailService = emailService;
            _gnmisoService = gnmisoService;
            _securityParametersService = securityParametersService;
            //_tokenVerificationFactory = tokenVerificationFactory;
        }

        public async Task<bool> VerifyUser(string UserName)
        {
            var user = await _userManager.FindByNameAsync(UserName);
            if (user == null)
            {
                return false;
            }
            return true;
        }

        public async Task<bool> VerifyUserById(int userId)
        {
            var user = await _userManager.FindByIdAsync(Convert.ToString(userId));

            return user != null;
        }
        public async Task<AuthenticationResponse> AuthenticateAsync(AuthenticationRequest request)
        {
            var response = new AuthenticationResponse();

            var result = await ValidateUserCredentialsAsync(request.UserCredential, request.Password);
            if (result.HasError)
            {
                response.HasError = result.HasError;
                response.Error = result.Error;
                return response;
            }

            var user = result.User;

            var companySecurityParameters = await _securityParametersService.GetByIdResponse(user.CodigoEmp);

            await UpdateUserTwoFactorStatusAsync(user, companySecurityParameters);

            bool requires2FA = await _userManager.GetTwoFactorEnabledAsync(user);

            if (requires2FA)
            {
                return await SendTwoFactorCodeAsync(user);
            }

            await FillAuthenticationResponse(response, user);
            return response;
        }

        private async Task UpdateUserTwoFactorStatusAsync(Usuario user, GnSecurityParametersResponse companySecurityParameters)
        {
            bool userHas2FAEnabled = await _userManager.GetTwoFactorEnabledAsync(user);
            bool companyRequires2FA = companySecurityParameters?.Requires2FA == true;
            bool companyAllowsOptional2FA = companySecurityParameters?.AllowsOptional2FA == true;

            if (companyRequires2FA)
            {
                if (!userHas2FAEnabled)
                {
                    await _userManager.SetTwoFactorEnabledAsync(user, true);
                }
            }
            else if (!companyAllowsOptional2FA && userHas2FAEnabled)
            {
                await _userManager.SetTwoFactorEnabledAsync(user, false);
            }

        }

        public async Task<AuthenticationResponse> VerifyTwoFactorCodeAsync(string userId, string code)
        {
            var response = new AuthenticationResponse();

            var result = await GetAndValidateUserAsync(userId);
            if (result.HasError)
            {
                response.HasError = result.HasError;
                response.Error = result.Error;
                return response;
            }

            var user = result.User;

            var isValid = await _userManager.VerifyTwoFactorTokenAsync(user, "Email", code);
            if (!isValid)
            {
                response.HasError = true;
                response.Error = "Código de verificación inválido o expirado";
                return response;
            }

            await _userManager.UpdateSecurityStampAsync(user);

            await FillAuthenticationResponse(response, user);

            return response;
        }

        #region PrivateMethods


        private async Task<(bool HasError, string Error, Usuario User)> ValidateUserCredentialsAsync(string userCredential, string password)
        {
            var user = await _userManager.Users
                .Include(u => u.GnPerfil)
                .Include(u => u.GnEmpresa)
                .Include(u => u.GnSucursal)
                .FirstOrDefaultAsync(u => u.Email == userCredential || u.UserName == userCredential);

            if (user == null)
            {
                return (true, $"{userCredential} no tiene cuenta registrada", null);
            }

            if (!user.IsActive)
            {
                return (true, $"{userCredential} se encuentra inactivo", null);
            }

            if (!await _userManager.CheckPasswordAsync(user, password))
            {
                return (true, $"Credenciales incorrectas {userCredential}", null);
            }

            return (false, null, user);
        }

        private async Task<(bool HasError, string Error, Usuario User)> GetAndValidateUserAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return (true, "Usuario no encontrado", null);
            }

            if (!user.IsActive)
            {
                return (true, "La cuenta se encuentra inactiva", null);
            }

            user = await _userManager.Users
                .Include(u => u.GnPerfil)
                .Include(u => u.GnEmpresa)
                .Include(u => u.GnSucursal)
                .FirstOrDefaultAsync(u => u.Id == user.Id);

            if (user == null)
            {
                return (true, "Error al cargar la información del usuario", null);
            }

            return (false, null, user);
        }

        private async Task<AuthenticationResponse> SendTwoFactorCodeAsync(Usuario user)
        {
            var response = new AuthenticationResponse();

            var providers = await _userManager.GetValidTwoFactorProvidersAsync(user);
            if (providers.Contains("Email"))
            {
                try
                {
                    var token = await _userManager.GenerateTwoFactorTokenAsync(user, "Email");

                    var emailMessage = new GnEmailMessageDto
                    {
                        To = new List<string> { user.Email },
                        Subject = "Tu código de verificación en dos pasos",
                        Body = $@"
                    <h1>Verificación de dos factores</h1>
                    <p>Tu código de verificación es: <strong>{token}</strong></p>
                    <p>Este código expirará en 10 minutos.</p>
                    <p>Gracias,<br/>El equipo de PTP</p>
                ",
                        IsHtml = true,
                        EmpresaId = user.CodigoEmp ?? 0
                    };

                    await _emailService.SendAsync(emailMessage, user.CodigoEmp ?? 0);

                    response.Requires2FA = true;
                    response.Id = user.Id; 

                    response.Email = user.Email;
                    response.UserName = user.UserName;
                    response.FullName = $"{user.Nombre} {user.Apellido}";

                    return response;
                }
                catch (Exception ex)
                {
                    response.HasError = true;
                    response.Error = "Error al enviar el código de verificación. Por favor, intente nuevamente.";
                   
                    return response;
                }
            }
            else
            {
                response.HasError = true;
                response.Error = "Error de configuración: No se puede enviar el código de verificación.";
                return response;
            }
        }


        private JwtSecurityToken GenerateJWToken(Usuario user)
        {
            var claims = new List<Claim>
    {
        new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
    };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.UtcNow.AddMinutes(_jwtSettings.ExpirationInMinutes);


            return new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                expires: expires,
                signingCredentials: creds);
        }

        public async Task<RegisterResponse> RegisterUserAsync(BussinessLayer.DTOs.ModuloGeneral.Configuracion.Account.RegisterRequest request, string origin)
        {
            try
            {
                RegisterResponse response = new()
                {
                    HasError = false
                };

                var userWithSameUserName = await _userManager.FindByNameAsync(request.UserName);
                if (userWithSameUserName != null)
                {
                    response.HasError = true;
                    response.Error = $"El nombre de usuario '{request.UserName}' ya está en uso.";
                    return response;
                }

                var userWithSameEmail = await _userManager.FindByEmailAsync(request.Email);
                if (userWithSameEmail != null)
                {
                    response.HasError = true;
                    response.Error = $"El correo electrónico '{request.Email}' ya está registrado.";
                    return response;
                }

                var perfil = await _roleManager.FindByIdAsync(request.RoleId.ToString());
                if (perfil == null)
                {
                    response.HasError = true;
                    response.Error = "El ID de rol especificado no existe.";
                    return response;
                }

                var user = MapRegisterRequestToUsuario(request);
                var result = await _userManager.CreateAsync(user, request.Password);
                if (!result.Succeeded)
                {
                    response.HasError = true;
                    response.Error = string.Join(", ", result.Errors.Select(e => e.Description));
                    return response;
                }

                return response;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

        }

        private RefreshToken GenerateRefreshToken()
        {
            return new RefreshToken
            {
                Token = RandomTokenString(),
                Expires = DateTime.UtcNow.AddDays(7),
                Created = DateTime.UtcNow
            };
        }

        private string RandomTokenString()
        {
            using var rngCryptoServiceProvider = new RNGCryptoServiceProvider();
            var ramdomBytes = new byte[40];
            rngCryptoServiceProvider.GetBytes(ramdomBytes);

            return BitConverter.ToString(ramdomBytes).Replace("-", "");
        }
        private Usuario MapRegisterRequestToUsuario(BussinessLayer.DTOs.ModuloGeneral.Configuracion.Account.RegisterRequest request)
        {
            return new Usuario
            {
                Nombre = request.FirstName,
                Apellido = request.LastName,
                Email = request.Email,
                UserName = request.UserName,
                PhoneNumber = request.Phone,
                CodigoEmp = request.CompanyId,
                CodigoSuc = request.SucursalId,
                IdPerfil = request.RoleId,
                EmailConfirmed = true,
                IpAdiccion = request.UserIP,
                IsActive = request.IsActive,
                 ImagenUsuario = request.UserImage,
                 LanguageCode = request.LanguageCode,
                 DefaultUrl = request.DefaultUrl,
                 TwoFactorEnabled = request.TwoFactorEnabled
            };
        }

        // Método auxiliar para evitar duplicación de código
        private async Task FillAuthenticationResponse(AuthenticationResponse response, Usuario user)
        {
            response.Id = user.Id;
            response.Email = user.Email;
            response.UserName = user.UserName;
            response.FullName = $"{user.Nombre} {user.Apellido}";
            response.RoleId = user.IdPerfil;
            response.IsVerified = user.EmailConfirmed;

            response.IPUser = user.IpAdiccion ?? string.Empty;
            response.CompanyId = user.CodigoEmp;
            response.UserName = user.Nombre ?? string.Empty;
            response.Email = user.Email ?? string.Empty;
            response.PhoneNumber = user.PhoneNumber ?? string.Empty;
            response.SucursalId = user.CodigoSuc;
            response.RoleName = user.GnPerfil.Name;
            response.CompanyName = user.GnEmpresa.NOMBRE_EMP;
            response.SucursalName = user.GnSucursal.NombreSuc;
            response.GnPermisoResponses = await _gnmisoService.GetAllPermisosForLogin(user.CodigoEmp, user.IdPerfil);

            JwtSecurityToken jwtToken = GenerateJWToken(user);
            response.JWToken = new JwtSecurityTokenHandler().WriteToken(jwtToken);

            response.RefreshToken = GenerateRefreshToken().Token;
            response.TokenDurationInMinutes = _jwtSettings.ExpirationInMinutes;
            response.RequestDate = DateTime.Now;
        }
        #endregion

        #region New AddOns
        public async Task<string> SendConfirmationEmailAsync(string email, string origin)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return $"No accounts registered with {email}.";
            }

            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
            var route = "api/account/confirm-email";
            var verificationUri = QueryHelpers.AddQueryString($"{origin}/{route}", "userId", user.Id.ToString());
            verificationUri = QueryHelpers.AddQueryString(verificationUri, "token", code);

            //await _emailService.SendAsync();

            //await _emailService.SendAsync(new EmailRequest
            //{
            //    To = user.Email,
            //    Subject = "Confirm your email",
            //    Body = $"Please confirm your account by visiting this URL: {verificationUri}"
            //});

            return "Confirmation email sent. Please check your email.";
        }

        public async Task<string> SendPasswordResetEmailAsync(string email, string origin)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return $"No accounts registered with {email}.";
            }

            var code = await _userManager.GeneratePasswordResetTokenAsync(user);
            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
            var route = "api/account/reset-password";
            var resetUri = QueryHelpers.AddQueryString($"{origin}/{route}", "token", code);

            //await _emailService.SendAsync();

            //await _emailService.SendAsync(new EmailRequest
            //{
            //    To = user.Email,
            //    Subject = "Confirm your email",
            //    Body = $"Please confirm your account by visiting this URL: {verificationUri}"
            //});

            return "Password reset email sent. Please check your email.";
        }

        public async Task<string> EnableTwoFactorAuthenticationAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return "User not found.";
            }

            await _userManager.SetTwoFactorEnabledAsync(user, true);
            return "Two-factor authentication has been enabled.";
        }

        public async Task<RegisterResponse> RegisterExternalUserAsync(ExternalLoginInfo info, string origin)
        {
            var response = new RegisterResponse { HasError = false };

            var user = new Usuario
            {
                UserName = info.Principal.FindFirstValue(ClaimTypes.Email),
                Email = info.Principal.FindFirstValue(ClaimTypes.Email),
                EmailConfirmed = true,
                Nombre = info.Principal.FindFirstValue(ClaimTypes.GivenName),
                Apellido = info.Principal.FindFirstValue(ClaimTypes.Surname)
            };

            var result = await _userManager.CreateAsync(user);
            if (!result.Succeeded)
            {
                response.HasError = true;
                response.Error = string.Join(", ", result.Errors.Select(e => e.Description));
                return response;
            }

            await _userManager.AddLoginAsync(user, info);
            return response;
        }

        public async Task<ForgotPasswordResponse> ForgotPasswordAsync(ForgotPasswordRequest request, string origin)
        {
            ForgotPasswordResponse response = new()
            {
                HasError = false
            };

            // Buscar al usuario por su correo electrónico
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null)
            {
                response.HasError = true;
                response.Error = $"No hay ninguna cuenta registrada con el correo {request.Email}.";
                return response;
            }

            var resetToken = await _userManager.GeneratePasswordResetTokenAsync(user);
            var encodedToken = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(resetToken));

            var resetUrl = $"{origin}/reset-password/token={encodedToken}";
            resetUrl = QueryHelpers.AddQueryString(resetUrl, "email", user.Email);

            var emailMessage = new GnEmailMessageDto
            {
                To = new List<string> { user.Email },
                Subject = "Restablecimiento de contraseña",
                Body = $@"
            <h1>Solicitud de restablecimiento de contraseña</h1>
            <p>Hemos recibido una solicitud para restablecer tu contraseña.</p>
            <p>Para continuar con el proceso, haz clic en el siguiente enlace:</p>
            <p><a href='{resetUrl}'>Restablecer contraseña</a></p>
            <p>Este enlace caducará en 30 minutos por seguridad.</p>
            <p>Si no solicitaste este cambio, puedes ignorar este correo.</p>
            <p>Gracias,<br/>El equipo de PTP</p>
        ",
                IsHtml = true,
                EmpresaId = user.CodigoEmp ?? 0
            };

            await _emailService.SendAsync(emailMessage, user.CodigoEmp ?? 0);

            return response;
        }

        public async Task<ResetPasswordResponse> ResetPasswordAsync(ResetPasswordRequest request)
        {
            ResetPasswordResponse response = new()
            {
                HasError = false
            };

            // Buscar al usuario por su correo electrónico
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null)
            {
                response.HasError = true;
                response.Error = $"No hay ninguna cuenta registrada con el correo {request.Email}.";
                return response;
            }

            // Decodificar el token de reseteo
            string decodedToken;
            try
            {
                decodedToken = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(request.Token));
            }
            catch (Exception)
            {
                response.HasError = true;
                response.Error = "El token no es válido.";
                return response;
            }

            // Validar que la contraseña cumpla con los requisitos
            var passwordValidator = new PasswordValidator<Usuario>();
            var passwordValidationResult = await passwordValidator.ValidateAsync(_userManager, null, request.NewPassword);

            if (!passwordValidationResult.Succeeded)
            {
                response.HasError = true;
                response.Error = "La contraseña no cumple con los requisitos mínimos de seguridad.";
                return response;
            }

            // Verificar que las contraseñas coincidan
            if (request.NewPassword != request.ConfirmPassword)
            {
                response.HasError = true;
                response.Error = "Las contraseñas no coinciden.";
                return response;
            }

            // Intentar restablecer la contraseña
            var result = await _userManager.ResetPasswordAsync(user, decodedToken, request.NewPassword);
            if (!result.Succeeded)
            {
                response.HasError = true;

                // Verificar si el error está relacionado con un token vencido
                if (result.Errors.Any(e => e.Code == "InvalidToken"))
                {
                    response.Error = "El enlace de restablecimiento ha caducado. Por favor, solicita uno nuevo.";
                }
                else
                {
                    response.Error = "Error al restablecer la contraseña. Verifica que el enlace no haya caducado e intenta nuevamente.";
                }

                // Opcional: Registrar los errores específicos para depuración
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                // _logger.LogError($"Errores al restablecer contraseña: {errors}");

                return response;
            }

            // Enviar correo de confirmación
            var confirmationEmail = new GnEmailMessageDto
            {
                To = new List<string> { user.Email },
                Subject = "Contraseña restablecida con éxito",
                Body = $@"
            <h1>Tu contraseña ha sido restablecida</h1>
            <p>La contraseña de tu cuenta ha sido cambiada exitosamente.</p>
            <p>Si no realizaste este cambio, contacta inmediatamente a nuestro soporte técnico.</p>
            <p>Gracias,<br/>El equipo de PTP</p>
        ",
                IsHtml = true,
                EmpresaId = user.CodigoEmp ?? 0
            };

            await _emailService.SendAsync(confirmationEmail, user.CodigoEmp ?? 0);

            return response;
        }

        #region External Register Logic
        //public async Task<RegisterResponse> RegisterExternalUserAsync(ExternalRegisterRequest request)
        //{
        //    var response = new RegisterResponse { HasError = false };

        //    try
        //    {
        //        // 1. Verificación de token usando Strategy
        //        //var strategy = _tokenVerificationFactory.CreateStrategy(request.Provider);
        //        var userInfo = await strategy.VerifyTokenAsync(request.Token);

        //        // 2. Creación/Actualización de usuario
        //        var user = await FindOrCreateUserAsync(userInfo);

        //        // 3. Asignación de roles y compañía
        //        await AssignEnterpriseDataAsync(user, request);

        //        response.UserId = user.Id;
        //        return response;
        //    }
        //    catch (Exception ex)
        //    {
        //        response.HasError = true;
        //        response.Error = ex.Message;
        //        return response;
        //    }
        //}

        private async Task<Usuario> FindOrCreateUserAsync(ExternalUserInfo userInfo)
        {
            var user = await _userManager.FindByEmailAsync(userInfo.Email) ?? new Usuario
            {
                UserName = userInfo.Email,
                Email = userInfo.Email,
                Nombre = userInfo.FirstName,
                Apellido = userInfo.LastName,
                EmailConfirmed = true,
                PhoneNumber = userInfo.PhoneNumber
            };

            if (user.Id == 0)
            {
                var result = await _userManager.CreateAsync(user);
                if (!result.Succeeded) throw new Exception(string.Join(", ", result.Errors));
            }

            return user;
        }

        private async Task AssignEnterpriseDataAsync(Usuario user, ExternalRegisterRequest request)
        {
            var role = await _roleManager.FindByIdAsync(request.RoleId.ToString());
            if (role == null) throw new Exception("Rol no encontrado");

            await _userManager.AddToRoleAsync(user, role.Name);

            user.CodigoEmp = request.CompanyId;
            user.CodigoSuc = request.SucursalId;
            await _userManager.UpdateAsync(user);
        }

        #endregion
        #endregion


    }
}
