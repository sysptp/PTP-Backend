using DataLayer.Models.Otros;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Models.Modulo_Citas
{
    public class CtaAppointmentMovements : AuditableEntities
    {
        [Key]
        public int IdMovement { get; set; }
        public string? Description { get; set; }
        public int? EmailSmsType { get; set; }
        public string FromEmail { get; set; } = null!;
        public string ToEmail { get; set; } = null!;
        public int? IdState { get; set; }
        [ForeignKey("IdState")]
         public CtaState? CtaState { get; set; }
        public int? IdMessage { get; set; }
        public int AppointmentId { get; set; }
        [ForeignKey("AppointmentId")]
        public CtaAppointments? CtaAppointments { get; set; }
        public bool Sent { get; set; } = true;
    }
}
