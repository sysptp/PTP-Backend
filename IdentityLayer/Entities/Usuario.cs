using Microsoft.AspNetCore.Identity;

namespace IdentityLayer.Entities
{
    public class Usuario : IdentityUser<int> 
    {
        public long? CodigoEmp { get; set; } 
        public int? IdHorario { get; set; }
        public int? IdPerfil { get; set; }
        public string Nombre { get; set; } = null!;
        public string Apellido { get; set; } = null!;
        public string? ImagenUsuario { get; set; }
        public string? TelefonoPersonal { get; set; }
        public string? Telefono { get; set; }
        public bool OnlineUsuario { get; set; }
        public long? CodigoSuc { get; set; }
        public string? IpAdiccion { get; set; }
        public string? IpModificacion { get; set; }
        public string? UsuarioAdicion { get; set; }
        public DateTime? FechaAdicion { get; set; } = DateTime.Now;
        public string? UsuarioModificacion { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public string Longitud { get; set; } = "0";
        public string Latitud { get; set; } = "0";
        public bool IsActive = true;
        public bool Borrado { get; set; }
    }
}
