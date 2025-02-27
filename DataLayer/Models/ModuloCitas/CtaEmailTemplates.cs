using System.ComponentModel.DataAnnotations;
using DataLayer.Models.Otros;

namespace DataLayer.Models.ModuloCitas
{
    public class CtaEmailTemplates : AuditableEntities
    {
        [Key]
        public long Id { get; set; }
        public long CompanyId { get; set; }
        public int TemplateTypeId { get; set; }
        public string Subject { get; set; } = null!;
        public string Body { get; set; } = null!;
        public bool AppliesToParticipant { get; set; }
        public bool AppliesToAssignee { get; set; }
    }
}
