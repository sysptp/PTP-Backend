using DataLayer.Models.Otros;
using System.ComponentModel.DataAnnotations;

namespace DataLayer.Models.ModuloCitas
{
    public class CtaContactType : AuditableEntities
    {
        [Key]
        public int Id { get; set; }
        public string Descripcion { get; set; } = null!;
        public long CompanyId { get; set; }
    }
}
