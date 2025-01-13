

namespace DataLayer.Models.Otros
{
    public class AuditableEntities
    {
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public string UsuarioCreacion { get; set; } = string.Empty;
        public string UsuarioModificacion { get; set; } = string.Empty;
        public bool Borrado { get; set; }

    }
}
