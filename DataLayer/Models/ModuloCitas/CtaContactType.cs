using DataLayer.Models.Otros;

namespace DataLayer.Models.ModuloCitas
{
    public class CtaContactType : AuditableEntities
    {
        public int Id { get; set; }
        public string Descripcion { get; set; } = null!;
        public long CompanyId { get; set; }
    }
}
