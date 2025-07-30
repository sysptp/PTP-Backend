using DataLayer.Models.ModuloCitas;
using DataLayer.Models.Otros;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Models.Modulo_Citas
{
    public class CtaState : AuditableEntities
    {
        [Key]
        public int IdStateAppointment { get; set; }
        public string? Description { get; set; }
        public bool IsClosure { get; set; } = false;
        public DateTime CreationDate { get; set; } = DateTime.Now;
        public long CompanyId { get; set; }
        public int AreaId { get; set; }
        public bool IsDefault { get; set; }
        public long TemplateIdIn { get; set; }
        public long TemplateIdOut { get; set; }
        public long TemplateIdUpdate { get; set; }
        public long TemplateIdUpdateParticipant { get; set; }

    }
}
