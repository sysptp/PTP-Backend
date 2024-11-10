using DataLayer.Models.Otros;

namespace DataLayer.Models.Seguridad
{
    public class Usuario : AuditableEntities
    {
        public int Id { get; set; }
        public long CodigoEmp { get; set; }
        public int? IdHorario { get; set; }
        public int IdPerfil { get; set; }
        public string Nombre { get; set; } = null!;
        public string Apellido { get; set; } = null!;
        public string? ImagenUsuario { get; set; }
        public string? TelefonoPersonal { get; set; }
        public string? Telefono { get; set; } 
        public bool OnlineUsuario { get; set; }
        public long CodigoSuc { get; set; }
        public string? IpAdiccion { get; set; }
        public string? IpModificacion { get; set; }
        public int UsuarioAdiccion { get; set; }
        public string? Longitud { get; set; }
        public string? Latitud { get; set; }
        public string? UserName { get; set; }
        public string? NormalizedUserName { get; set; }
        public string? Email { get; set; }
        public string? NormalizedEmail { get; set; }
        public bool EmailConfirmed { get; set; }
        public string? PasswordHash { get; set; }
        public string? SecurityStamp { get; set; }
        public string? ConcurrencyStamp { get; set; }
        public string? PhoneNumber { get; set; }
        public bool PhoneNumberConfirmed { get; set; }
        public bool TwoFactorEnabled { get; set; }
        public bool LockoutEnabled { get; set; }
        public int AccessFailedCount { get; set; }

    }
}
