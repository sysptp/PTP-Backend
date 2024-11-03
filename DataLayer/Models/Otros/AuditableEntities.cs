

namespace DataLayer.Models.Otros
{
    public class AuditableEntities
    {
        public DateTime FechaAdicion { get; set; } = DateTime.Now;
        public string UsuarioAdicion { get; set; } = null!;
        public DateTime? FechaModificacion { get; set; }
        public string? UsuarioModificacion { get; set; }
        public bool Borrado { get; set; }

    }
}
