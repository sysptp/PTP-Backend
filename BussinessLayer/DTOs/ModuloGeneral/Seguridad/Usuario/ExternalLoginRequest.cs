
namespace BussinessLayer.DTOs.ModuloGeneral.Seguridad.Usuario
{
    public class ExternalLoginRequest
    {
        public string Provider { get; set; } = null!; // "Google", "Microsoft", "Facebook"
        public string Token { get; set; } = null!; // Token de autenticación externa
    }
}
