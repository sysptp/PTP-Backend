
using DataLayer.Models.Otros;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Models.Modulo_Citas
{
    public class CtaSessionDetails : AuditableEntities
    {
        [Key]
        public int IdSessionDetail { get; set; }
        public int AppointmentId { get; set; } 
        [ForeignKey("AppointmentId")]
        public CtaAppointments? CtaAppointments { get; set; }
        public int IdSession { get; set; }
        [ForeignKey("IdSession")]
        public CtaSessions? CtaSessions { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
