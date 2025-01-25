using DataLayer.Models.Otros;

namespace DataLayer.Models.ModuloCitas
{
    public class CtaAppointmentContacts : AuditableEntities
    {
        public int Id { get; set; }
        public int ContactTypeId { get; set; }
        public int ContactId { get; set; }
        public int AppointmentId { get; set; }
        public long CompanyId { get; set; }

    }
}
