using Microsoft.AspNetCore.Identity;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using IdentityLayer.Entities;
using BussinessLayer.Dtos.Account;
using AutoMapper;
using TaskMaster.Core.Domain.Settings;
using DataLayer.Enums;

namespace IdentityLayer.Services
{
    public class AccountService 
    {
        private readonly UserManager<SC_USUAR001> _userManager;
        private readonly SignInManager<SC_USUAR001> _signInManager;
        private readonly JWTSettings _jwtSettings;
        private readonly IMapper _mapper;

        public AccountService(
              UserManager<SC_USUAR001> userManager,
              SignInManager<SC_USUAR001> signInManager,
              IOptions<JWTSettings> jwtSettings
,
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

        public async Task<AuthenticationResponse> AuthenticateAsync(AuthenticationRequest request, bool IsForApi)
        {
            AuthenticationResponse response = new();

            var user = await _userManager.FindByEmailAsync(request.UserCredential);
            if (user == null)
            {
                user = await _userManager.FindByNameAsync(request.UserCredential);
                if (user == null)
                {
                    response.HasError = true;
                    response.Error = $"No Accounts registered with {request.UserCredential}";
                    return response;
                }
            }
            var result = await _signInManager.PasswordSignInAsync(user.UserName, request.Password, false, lockoutOnFailure: false);
            if (!result.Succeeded)
            {
                response.HasError = true;
                response.Error = $"Invalid credentials for {request.UserCredential}";
                return response;
            }

            response.Id = user.Id;
            response.Email = user.Email;
            response.UserName = user.UserName;

            var rolesList = await _userManager.GetRolesAsync(user).ConfigureAwait(false);

            response.Roles = rolesList.ToList();
            response.IsVerified = user.EmailConfirmed;

            if (IsForApi)
            {
                JwtSecurityToken jwtSecurityToken = await GenerateJWToken(user);

                response.JWToken = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
                var refreshToken = GenerateRefreshToken();
                response.RefreshToken = refreshToken.Token;

            }

            return response;
        }

        public async Task SignOutAsync()
        {
            await _signInManager.SignOutAsync();
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

            var user = new SC_USUAR001
            {
                Email = request.Email,
                NombreUsuario = request.FirstName,
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
              
            await _userManager.AddToRoleAsync(user, Roles.Client.ToString());
                 
            return response;
        }

        #region PrivateMethods

        private async Task<JwtSecurityToken> GenerateJWToken(SC_USUAR001 user)
        {
            var userClaims = await _userManager.GetClaimsAsync(user);
            var roles = await _userManager.GetRolesAsync(user);

            var roleClaims = new List<Claim>();

            foreach (var role in roles)
            {
                roleClaims.Add(new Claim("roles", role));
            }

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub,user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email,user.Email),
                new Claim("uid", user.Id)
            }
            .Union(userClaims)
            .Union(roleClaims);

            var symmectricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
            var signingCredetials = new SigningCredentials(symmectricSecurityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_jwtSettings.DurationInMinutes),
                signingCredentials: signingCredetials);

            return jwtSecurityToken;
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
