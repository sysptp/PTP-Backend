using DataLayer.Models.Otros;

namespace DataLayer.Models.ModuloCitas
{
    public class CtaAppointmentArea : AuditableEntities
    {
        public int AreaId { get; set; }
        public string Description { get; set; } = null!;
        public long CompanyId { get; set; }
        public bool IsPrincipal { get; set; }

    }
}
