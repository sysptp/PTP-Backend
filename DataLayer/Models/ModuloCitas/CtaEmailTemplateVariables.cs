using System.ComponentModel.DataAnnotations;
using DataLayer.Models.Otros;

namespace DataLayer.Models.ModuloCitas
{
    public class CtaEmailTemplateVariables : AuditableEntities
    {
        [Key]
        public long VariableId { get; set; }
        public string VariableName { get; set; } = null!;
        public string VariableDescription { get; set; } = null!;
    }
}
