using System;

namespace DataLayer.Models.Otros
{
    public class AuditableEntities
    {
        public DateTime FechaAdicion { get; set; } = DateTime.Now;
        public string AdicionadoPor { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public string ModificadoPor { get; set; }
    }
}
