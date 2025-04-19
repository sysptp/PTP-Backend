using DataLayer.Models.ModuloGeneral.Empresa;
using DataLayer.Models.Otros;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Models.ModuloCitas
{
    public class CtaNotificationTemplates : AuditableEntities
    {
        [Key]
        public long NotificationTemplateId { get; set; }
        public long CompanyId { get; set; }
        public int NotificationType { get; set; }
        public long? EmailTemplateId { get; set; }
        public long? SmsTemplateId { get; set; }
        public long? WhatsAppTemplateId { get; set; }
        public bool IsActive { get; set; }

        [ForeignKey("CompanyId")]
        public virtual GnEmpresa? Company { get; set; }

        [ForeignKey("EmailTemplateId")]
        public virtual CtaEmailTemplates? EmailTemplate { get; set; }

        [ForeignKey("SmsTemplateId")]
        public virtual CtaSmsTemplates? SmsTemplate { get; set; }

        [ForeignKey("WhatsAppTemplateId")]
        public virtual CtaWhatsAppTemplates? WhatsAppTemplate { get; set; }
    }
}
