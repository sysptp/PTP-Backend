using DataLayer.Models.Otros;

namespace DataLayer.Models.ModuloCitas
{
    public class CtaAppointmentGuest : AuditableEntities
    {
        public int Id { get; set; }
        public int GuestId { get; set; }
        public int AppointmentId { get; set; }
        public long CompanyId { get; set; }

    }
}
