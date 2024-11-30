using Microsoft.AspNetCore.Identity;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using IdentityLayer.Entities;
using AutoMapper;
using BussinessLayer.Settings;
using System.Security.Cryptography;
using BussinessLayer.Interface.IAccount;
using BussinessLayer.Interfaces.Repository.Empresa;
using BussinessLayer.DTOs.Configuracion.Account;
using BussinessLayer.DTOs.Configuracion.Seguridad.Usuario;

namespace IdentityLayer.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<Usuario> _userManager;
        private readonly SignInManager<Usuario> _signInManager;
        private readonly JWTSettings _jwtSettings;
        private readonly IMapper _mapper;
        private readonly RoleManager<GnPerfil> _roleManager;
        private readonly IGnSucursalRepository _sucursalRepository;
        private readonly IGnEmpresaRepository _empresaRepository;

        public AccountService(
              UserManager<Usuario> userManager,
              SignInManager<Usuario> signInManager,
              RoleManager<GnPerfil> roleManager,
              IOptions<JWTSettings> jwtSettings,
              IMapper mapper,
              IGnSucursalRepository sucursalRepository,
              IGnEmpresaRepository empresaRepository)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _jwtSettings = jwtSettings.Value;
            _mapper = mapper;
            _sucursalRepository = sucursalRepository;
            _empresaRepository = empresaRepository;
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

            var user = await _userManager.FindByEmailAsync(request.UserCredential) ??
                       await _userManager.FindByNameAsync(request.UserCredential);

            if (!user.IsActive) 
            {
                response.HasError = true;
                response.Error = $"{request.UserCredential} se encuentra inactivo";
                return response;
            }

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
            var role = await _roleManager.FindByIdAsync(response.RoleId.ToString());
            var sucursal = await _sucursalRepository.GetById(response.SucursalId);
            var company = await _empresaRepository.GetById((long)response.CompanyId);
            response.RoleName = role.Name;
            response.CompanyName = company.NOMBRE_EMP;
            response.SucursalName = sucursal.NombreSuc;

            JwtSecurityToken jwtToken = GenerateJWToken(user);
            response.JWToken = new JwtSecurityTokenHandler().WriteToken(jwtToken);

            response.RefreshToken = GenerateRefreshToken().Token;
            response.TokenDurationInMinutes = _jwtSettings.ExpirationInMinutes;
            response.RequestDate = DateTime.Now;

            return response;
        }

        public async Task<List<UserResponse>> GetAllUsers()
        {
            var users = _userManager.Users.ToList();
            var userResponseList = new List<UserResponse>();

            foreach (var user in users)
            {
                var role = user.IdPerfil.HasValue
                     ? await _roleManager.FindByIdAsync(user.IdPerfil.Value.ToString())
                     : null;

                var sucursal = user.CodigoSuc.HasValue
                    ? await _sucursalRepository.GetById(user.CodigoSuc.Value)
                    : null;

                var company = user.CodigoEmp.HasValue
                    ? await _empresaRepository.GetById(user.CodigoEmp.Value)
                    : null;

                var userResponse = new UserResponse
                {
                    Id = user.Id,
                    CompanyId = user.CodigoEmp ?? 0,
                    ScheduleId = user.IdHorario,
                    RoleId = user.IdPerfil ?? 0,
                    FirstName = user.Nombre,
                    LastName = user.Apellido,
                    UserImage = user.ImagenUsuario,
                    PersonalPhone = user.TelefonoPersonal,
                    IsUserOnline = user.OnlineUsuario,
                    SucursalId = user.CodigoSuc ?? 0,
                    UserName = user.UserName,
                    Email = user.Email,
                    PhoneNumber = user.PhoneNumber,
                    SucursalName = sucursal?.NombreSuc,
                    RoleName = role?.Name,
                    CompanyName = company?.NOMBRE_EMP,
                    IsActive = user.IsActive,
                    LanguageCode = user.LanguageCode
                };

                userResponseList.Add(userResponse);
            }

            return userResponseList;
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
