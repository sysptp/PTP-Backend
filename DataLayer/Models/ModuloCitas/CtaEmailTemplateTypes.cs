using System.ComponentModel.DataAnnotations;
using DataLayer.Models.Otros;

namespace DataLayer.Models.ModuloCitas
{
    public class CtaEmailTemplateTypes : AuditableEntities
    {
        [Key]
        public int TemplateTypeId { get; set; }
        public string TemplateType { get; set; } = null!;
        public string Description { get; set; } = null!;
    }
}
