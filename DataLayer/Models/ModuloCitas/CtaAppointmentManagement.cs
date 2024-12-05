
using DataLayer.Models.Otros;
using System.ComponentModel.DataAnnotations;

namespace DataLayer.Models.Modulo_Citas
{
    public class CtaAppointmentManagement : AuditableEntities
    {
        [Key]
        public int IdManagementAppointment { get; set; }
        public int IdAppointment { get; set; }
        public string? Comment { get; set; }
      
    }
}
