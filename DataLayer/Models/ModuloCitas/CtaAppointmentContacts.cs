using DataLayer.Models.Otros;
using System.ComponentModel.DataAnnotations;

namespace DataLayer.Models.ModuloCitas
{
    public class CtaAppointmentContacts : AuditableEntities
    {
        [Key]
        public int Id { get; set; }
        public int ContactId { get; set; }
        public int AppointmentId { get; set; }
        public long CompanyId { get; set; }

    }
}
