
using DataLayer.Models.Modulo_Citas;
using DataLayer.Models.Otros;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Models.ModuloCitas
{
    public class CtaContacts : AuditableEntities
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string ContactNumber { get; set; } = null!;
        public string ContactEmail { get; set; } = null!;
        public int ContactTypeId { get; set; }
        [ForeignKey("ContactTypeId")]
        public CtaContactType? ContactType { get; set; }
        public int AppointmentId { get; set; }
        [ForeignKey("AppointmentId")]
        public CtaAppointments? Appointments { get; set; }
        public long CompanyId { get; set; }

    }
}
