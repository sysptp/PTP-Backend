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
using DataLayer.Enums;
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

        public AccountService(
              UserManager<Usuario> userManager,
              SignInManager<Usuario> signInManager,
              IOptions<JWTSettings> jwtSettings,
              IMapper mapper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
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
                response.Error = $"No Accounts registered with {request.UserCredential}";
                return response;
            }

            var signInResult = await _signInManager.PasswordSignInAsync(user.UserName, request.Password, false, lockoutOnFailure: false);
            if (!signInResult.Succeeded)
            {
                response.HasError = true;
                response.Error = $"Invalid credentials for {request.UserCredential}";
                return response;
            }

            response.Id = user.Id;
            response.Email = user.Email;
            response.UserName = user.UserName;
            //response.Roles = (await _userManager.GetRolesAsync(user)).ToList();
            response.IsVerified = user.EmailConfirmed;

            // Incluye los claims adicionales
            var additionalClaims = new List<Claim>
            {
                new Claim("IpUsuario", user.IpAdiccion ?? string.Empty),
                new Claim("IdEmpresa", user.CodigoEmp.ToString() ?? string.Empty),
                new Claim("CodigoUsuario", user.Id.ToString() ?? string.Empty),
                new Claim("NombreUsuario", user.Nombre ?? string.Empty),
                new Claim("IdPerfilUsuario", user.IdPerfil?.ToString() ?? string.Empty),
                new Claim("EmailUsuario", user.Email ?? string.Empty),
                new Claim("PhoneNumber", user.PhoneNumber ?? string.Empty),
                new Claim("IdSucursal", user.CodigoSuc?.ToString() ?? string.Empty),
            };

            JwtSecurityToken jwtToken = await GenerateJWToken(user, additionalClaims);
            response.JWToken = new JwtSecurityTokenHandler().WriteToken(jwtToken);
            var refreshToken = GenerateRefreshToken();
            response.RefreshToken = refreshToken.Token;

            return response;
        }

        #region PrivateMethods

        private async Task<JwtSecurityToken> GenerateJWToken(Usuario user, IEnumerable<Claim> additionalClaims)
        {
            var userClaims = await _userManager.GetClaimsAsync(user);
            var roles = await _userManager.GetRolesAsync(user);

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("uid", user.Id.ToString())
            }
            .Union(userClaims)
            //.Union(roles.Select(role => new Claim("roles", role)))
            .Union(additionalClaims);

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.UtcNow.AddMinutes(_jwtSettings.DurationInMinutes);

            return new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                expires: expires,
                signingCredentials: creds);
        }

        public async Task<RegisterResponse> RegisterUserAsync(RegisterRequest request, string origin, string Role)
        {
            RegisterResponse response = new()
            {
                HasError = false
            };

            var userWithSameUserName = await _userManager.FindByNameAsync(request.UserName);
            if (userWithSameUserName != null)
            {
                response.HasError = true;
                response.Error = $"username '{request.UserName}' is already taken.";
                return response;
            }

            var userWithSameEmail = await _userManager.FindByEmailAsync(request.Email);
            if (userWithSameEmail != null)
            {
                response.HasError = true;
                response.Error = $"Email '{request.Email}' is already registered.";
                return response;
            }

            var user = new Usuario
            {
                Email = request.Email,
                Nombre = request.FirstName,
                Apellido = request.LastName,
                UserName = request.UserName,
                PhoneNumber = request.Phone,
                EmailConfirmed = true
            };

            var result = await _userManager.CreateAsync(user, request.Password);
            if (!result.Succeeded)
            {
                response.HasError = true;
                response.Error = $"An error occurred trying to register the user.";
                return response;
            }

            //await _userManager.AddToRoleAsync(user, Roles.Client.ToString());

            return response;
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

        #endregion
    }
}
