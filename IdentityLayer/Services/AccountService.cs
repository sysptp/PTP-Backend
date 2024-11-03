using Microsoft.AspNetCore.Identity;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using IdentityLayer.Entities;
using BussinessLayer.Dtos.Account;
using AutoMapper;
using BussinessLayer.Settings;
using System.Security.Cryptography;
using BussinessLayer.Interface.IAccount;

namespace IdentityLayer.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<Usuario> _userManager;
        private readonly SignInManager<Usuario> _signInManager;
        private readonly JWTSettings _jwtSettings;
        private readonly IMapper _mapper;
        private readonly RoleManager<GnPerfil> _roleManager;

        public AccountService(
              UserManager<Usuario> userManager,
              SignInManager<Usuario> signInManager,
              RoleManager<GnPerfil> roleManager,
              IOptions<JWTSettings> jwtSettings,
              IMapper mapper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _jwtSettings = jwtSettings.Value;
            _mapper = mapper;
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

        public async Task<AuthenticationResponse> AuthenticateAsync(AuthenticationRequest request)
        {
            var response = new AuthenticationResponse();

            var user = await _userManager.FindByEmailAsync(request.UserCredential) ??
                       await _userManager.FindByNameAsync(request.UserCredential);

            if (user == null)
            {
                response.HasError = true;
                response.Error = $"{request.UserCredential} no tiene cuenta registrada";
                return response;
            }

            var signInResult = await _signInManager.PasswordSignInAsync(user.UserName, request.Password, false, lockoutOnFailure: false);
            if (!signInResult.Succeeded)
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

        public async Task<RegisterResponse> RegisterUserAsync(RegisterRequest request, string origin)
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
                    response.Error = "Ocurrió un error al intentar registrar al usuario.";
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
        private Usuario MapRegisterRequestToUsuario(RegisterRequest request)
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
    }
}
