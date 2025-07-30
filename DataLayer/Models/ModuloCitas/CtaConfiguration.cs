using DataLayer.Models.Otros;
using System.ComponentModel.DataAnnotations;

namespace DataLayer.Models.Modulo_Citas
{
    public class CtaConfiguration : AuditableEntities
    {
        [Key]
        public int IdConfiguration { get; set; }
        public bool SendEmail { get; set; } = false;
        public bool SendSms { get; set; } = false;
        public bool SendEmailReminder { get; set; } = false;
        public bool SendSmsReminder { get; set; } = false;
        public bool SendWhatsapp { get; set; } = false;
        public bool SendWhatsappReminder { get; set; } = false;
        public long CompanyId { get; set; }
        public bool NotifyClosure { get; set; } = false;
        public int? DaysInAdvance { get; set; }
    }
}
