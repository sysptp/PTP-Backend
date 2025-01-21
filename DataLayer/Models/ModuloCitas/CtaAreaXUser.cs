using DataLayer.Models.Otros;

namespace DataLayer.Models.ModuloCitas
{
    public class CtaAreaXUser : AuditableEntities
    {
        public int AreaXUserId { get; set; }
        public int UserId { get; set; }
        public int AreaId { get; set; }
        public long CompanyId { get; set; }
    }
}
