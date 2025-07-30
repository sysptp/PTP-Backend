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
using MimeKit.Cryptography;
using BussinessLayer.Interfaces.Services.ModuloGeneral.Seguridad;

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

        public AccountService(
              UserManager<Usuario> userManager,
              RoleManager<GnPerfil> roleManager,
              IOptions<JWTSettings> jwtSettings,
              IGnEmailService emailService
,
              IGnPermisoService gnmisoService
/*TokenVerificationFactory tokenVerificationFactory*/)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _jwtSettings = jwtSettings.Value;
            _emailService = emailService;
            _gnmisoService = gnmisoService;
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

            var user = await _userManager.Users
     .Include(u => u.GnPerfil)
     .Include(u => u.GnEmpresa)
     .Include(u => u.GnSucursal)
     .FirstOrDefaultAsync(u => u.Email == request.UserCredential || u.UserName == request.UserCredential);

            if (user == null)
            {
                response.HasError = true;
                response.Error = $"{request.UserCredential} no tiene cuenta registrada";
                return response;
            }
            if (!user.IsActive)
            {
                response.HasError = true;
                response.Error = $"{request.UserCredential} se encuentra inactivo";
                return response;
            }

            if (!await _userManager.CheckPasswordAsync(user, request.Password))
            {
                response.HasError = true;
                response.Error = $"Credenciales incorrectas {request.UserCredential}";
                return response;
            }

            response.Id = user.Id;
            response.Email = user?.Email;
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
            //response.GnPermisoResponses = await _gnmisoService.GetAllPermisosForLogin(user.CodigoEmp, user.IdPerfil);

            JwtSecurityToken jwtToken = GenerateJWToken(user);
            response.JWToken = new JwtSecurityTokenHandler().WriteToken(jwtToken);

            response.RefreshToken = GenerateRefreshToken().Token;
            response.TokenDurationInMinutes = _jwtSettings.ExpirationInMinutes;
            response.RequestDate = DateTime.Now;

            return response;
        }

        #region PrivateMethods

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
                IpAdiccion = request.UserIP
            };
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

            // Generar el token de reseteo de contraseña
            var resetToken = await _userManager.GeneratePasswordResetTokenAsync(user);
            resetToken = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(resetToken)); // Codificar el token para URL

            // Construir la URL de reseteo de contraseña
            var resetUri = QueryHelpers.AddQueryString($"{origin}/reset-password", "token", resetToken);

            // Enviar el correo electrónico con el enlace de reseteo
            //await _emailService.SendAsync(new EmailRequest
            //{
            //    To = user.Email,
            //    Subject = "Restablecer contraseña",
            //    Body = $"Para restablecer tu contraseña, haz clic en el siguiente enlace: {resetUri}"
            //});

            return response;
        }

        public async Task<ResetPasswordResponse> ResetPasswordAsync(BussinessLayer.DTOs.Account.ResetPasswordRequest request)
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
            var resetToken = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(request.Token));

            // Restablecer la contraseña
            var result = await _userManager.ResetPasswordAsync(user, resetToken, request.NewPassword);
            if (!result.Succeeded)
            {
                response.HasError = true;
                response.Error = "Ocurrió un error al restablecer la contraseña. Asegúrate de que el token sea válido y que la nueva contraseña cumpla con los requisitos.";
                return response;
            }

            return response;
        }

        //public async Task<AuthenticationResponse> AuthenticateAsync(AuthenticationRequest request)
        //{
        //    var response = new AuthenticationResponse();

        //    var user = await _userManager.FindByEmailAsync(request.Email) ?? await _userManager.FindByNameAsync(request.Email);
        //    if (user == null)
        //    {
        //        response.HasError = true;
        //        response.Error = $"No accounts registered with {request.Email}.";
        //        return response;
        //    }

        //    if (!await _userManager.CheckPasswordAsync(user, request.Password))
        //    {
        //        response.HasError = true;
        //        response.Error = "Invalid credentials.";
        //        return response;
        //    }

        //    var empresaParametros = await _dbContext.EmpresaParametros.FirstOrDefaultAsync(ep => ep.EmpresaId == user.CodigoEmp);
        //    if (empresaParametros?.Requiere2FA == true && await _userManager.GetTwoFactorEnabledAsync(user))
        //    {
        //        var providers = await _userManager.GetValidTwoFactorProvidersAsync(user);
        //        if (providers.Contains("Email"))
        //        {
        //            var token = await _userManager.GenerateTwoFactorTokenAsync(user, "Email");
        //            await _emailService.SendAsync(new EmailRequest
        //            {
        //                To = user.Email,
        //                Subject = "Your 2FA Code",
        //                Body = $"Your two-factor authentication code is: {token}"
        //            });

        //            response.Requires2FA = true;
        //            response.UserId = user.Id;
        //            return response;
        //        }
        //    }

        //    // Generar JWT y refrescar token si no se requiere 2FA o ya se ha verificado
        //    response.JWToken = new JwtSecurityTokenHandler().WriteToken(GenerateJWToken(user));
        //    response.RefreshToken = GenerateRefreshToken().Token;
        //    return response;
        //}


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
