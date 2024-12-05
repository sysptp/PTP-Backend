
using DataLayer.Models.Otros;
using System.ComponentModel.DataAnnotations;

namespace DataLayer.Models.Modulo_Citas
{
    public class CtaSessionDetails : AuditableEntities
    {
        [Key]
        public int IdSessionDetail { get; set; }
        public int IdAppointment { get; set; }
        public int IdSession { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
