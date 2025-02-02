
using DataLayer.Models.Otros;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Models.Modulo_Citas
{
    public class CtaAppointmentManagement : AuditableEntities
    {
        [Key]
        public int IdManagementAppointment { get; set; }
        public int AppointmentId { get; set; }
        [ForeignKey("AppointmentId")]
        public CtaAppointments? Appointments { get; set; }
        public string? Comment { get; set; }
      
    }
}
