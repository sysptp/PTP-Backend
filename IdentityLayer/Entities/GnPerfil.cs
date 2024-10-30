
using Microsoft.AspNetCore.Identity;

namespace IdentityLayer.Entities
{
    public class GnPerfil : IdentityRole<int>
    {
        public string? Descripcion { get; set; }
        public int? Bloquear { get; set; }
        public long? IDEmpresa { get; set; }
        public DateTime FechaCreada { get; set; } = DateTime.Now;
        public DateTime? UltimaFechaModificacion { get; set; }
    }
}
