using System;

namespace DataLayer.Models.Otros
{
    public class AuditableEntities
    {
        public DateTime FECHA_ADICION { get; set; } = DateTime.Now;
        public string USUARIO_ADICCIONUSUARIO_ADICCION { get; set; } = null!;
        public DateTime? FECHA_MODIFICACION { get; set; }
        public string? USUARIO_MODIFICACION { get; set; }
        public bool Borrado { get; set; }

    }
}
