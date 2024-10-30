using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BussinessLayer.Interfaces.IAutenticacion;
using BussinessLayer.DTOs.Autenticacion;
using Swashbuckle.AspNetCore.Annotations;

namespace PTP_API.Controllers.Autenticacion
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutenticacionController : ControllerBase
    {
        private readonly IAutenticacionService _autenticacionServices;
        private readonly IRepositorySection _repositorySection;
        private readonly IConfiguration _configuration;

        public AutenticacionController(IAutenticacionService autenticacionServices,
            IRepositorySection repositorySection,
            IConfiguration configuration)
        {
            _autenticacionServices = autenticacionServices;
            _repositorySection = repositorySection;
            _configuration = configuration;
        }

        [HttpPost("Login")]
        [SwaggerOperation(Summary = "Logear empresa", Description = "Devuelve el token de seguridad")]
        public IActionResult Login([FromBody] LoginRequestDTO request)
        {
            try
            {
                if (string.IsNullOrEmpty(request.Usuario))
                {
                    return BadRequest("El campo usuario no puede estar vacio.");
                }

                if (string.IsNullOrEmpty(request.Password))
                {
                    return BadRequest("El campo contraseña no puede estar vacio.");
                }

                var isValid = _repositorySection.TextCaractersValidation(request.Usuario);
                if (!isValid)
                {
                    return BadRequest("Usuario no valido");
                }

                var result = _autenticacionServices.IniciarSesion(request.Usuario, request.Password);
                if (result == null)
                {
                    return Unauthorized("Credenciales incorrectas.");
                }

                var claims = new List<Claim>
                {
                    new("IpUsuario", result.Ip),
                    new("IdEmpresa", result.DatosEmpresa.CODIGO_EMP.ToString()),
                    new("NombreEmpresa", result.DatosEmpresa.NOMBRE_EMP),
                    new("TelefonoEmpresa", result.DatosEmpresa.TELEFONO1),
                    new("CodigoUsuario", result.DatosUsuario.CODIGO_USUARIO.ToString()),
                    new("NombreUsuario", result.DatosUsuario.NOMBRE_USUARIO),
                    new("IdPerfilUsuario", result.DatosUsuario.ID_PERFIL.ToString()),
                    new("EmailUsuario", result.DatosUsuario.CORREO),
                    new("TelefonoUsuario", result.DatosUsuario.TELEFONO),
                    new("FechaAdiccion", result.DatosUsuario.FECHA_ADICION.ToString()),
                    new("IdSucursal", result.DatosSucursal.CODIGO_SUC.ToString()),
                    new("Sucursal", result.DatosSucursal.NOMBRE_SUC.ToString()),
                    new("TelefonSucursal1", result.DatosSucursal.TELEFONO1.ToString()),
                };

                var token = GenerateToken(claims);

                return Ok(new { Token = token });
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex.ToString());
                return BadRequest(ex.Message);
            }
        }

        private string GenerateToken(IEnumerable<Claim> claims)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSettings:SecretKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddMinutes(30); 

            var token = new JwtSecurityToken(
                issuer: null,
                audience: null,
                claims: claims,
                expires: expires,
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
