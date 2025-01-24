

namespace DataLayer.Models.Otros
{
    public class AuditableEntities
    {
        public DateTime? FechaAdicion { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public string? UsuarioAdicion { get; set; } = string.Empty;
        public string? UsuarioModificacion { get; set; } = string.Empty;
        public bool Borrado { get; set; }

    }
}
