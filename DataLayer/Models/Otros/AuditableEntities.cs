using System;

namespace DataLayer.Models.Otros
{
    public class AuditableEntities
    {
        public DateTime FECHA_ADICION { get; set; } = DateTime.Now;
        public int USUARIO_ADICCION { get; set; } 
        public DateTime? FECHA_MODIFICACION { get; set; }
        public int USUARIO_MODIFICACION { get; set; }
        public bool Borrado { get; set; }

    }
}
