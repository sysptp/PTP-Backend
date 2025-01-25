using DataLayer.Models.Otros;
using System.ComponentModel.DataAnnotations;

namespace DataLayer.Models.ModuloCitas
{
    public class CtaAreaXUser : AuditableEntities
    {
        [Key]
        public int AreaXUserId { get; set; }
        public int UserId { get; set; }
        public int AreaId { get; set; }
        public long CompanyId { get; set; }
    }
}
