using System.ComponentModel.DataAnnotations;
using DataLayer.Models.Otros;

namespace DataLayer.Models.ModuloCitas
{
    public class CtaNotificationSettings : AuditableEntities
    {
        [Key]
        public long Id { get; set; }
        public long CompanyId { get; set; }
        public int TemplateTypeId { get; set; }
        public bool SendEmail { get; set; }
        public bool SendSMS { get; set; }
        public bool SendWhatsApp { get; set; }
        public int ParticipantTypeId { get; set; }
    }
}
