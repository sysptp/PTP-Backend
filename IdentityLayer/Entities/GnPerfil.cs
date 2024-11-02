
using Microsoft.AspNetCore.Identity;

namespace IdentityLayer.Entities
{
    public class GnPerfil : IdentityRole<int>
    {
        public string? Descripcion { get; set; }
        public string Perfil { get; set; } = null!;
        public long? IDEmpresa { get; set; }
        public DateTime FechaAdicion { get; set; } = DateTime.Now;
        public string UsuarioAdicion { get; set; } = null!;
        public DateTime? FechaModificacion { get; set; }
        public string? UsuarioModificacion { get; set; }
        public bool Borrado { get; set; }
    }
}
