
namespace BussinessLayer.DTOs.Seguridad
{
    public class GnPerfilRequest
    {
        public string Name { get; set; } = null!;
        public string? Descripcion { get; set; }
        public long IDEmpresa { get; set; }
    }
}
