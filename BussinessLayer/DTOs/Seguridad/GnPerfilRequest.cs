
namespace BussinessLayer.DTOs.Seguridad
{
    public class GnPerfilRequest
    {
        public string Perfil { get; set; } = null!;
        public string? Descripcion { get; set; }
        public long IDEmpresa { get; set; }
    }
}
