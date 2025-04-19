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
        public long EmailTemplateIdIn { get; set; }
        public long EmailTemplateIdOut { get; set; }
        public long CompanyId { get; set; }
        public int AreaId { get; set; }
        public bool IsDefault { get; set; }
        public long EmailTemplateIdUpdate { get; set; }
        public long EmailTemplateIdUpdateParticipant { get; set; }
        public long EmailTemplateIdStateChange { get; set; }

        [ForeignKey("AssignedUserNotificationTemplateId")]
        public virtual CtaNotificationTemplates? AssignedUserNotificationTemplate { get; set; }

        [ForeignKey("ParticipantNotificationTemplateId")]
        public virtual CtaNotificationTemplates? ParticipantNotificationTemplate { get; set; }

        [ForeignKey("StateChangeNotificationTemplateId")]
        public virtual CtaNotificationTemplates? StateChangeNotificationTemplate { get; set; }

    }
}
