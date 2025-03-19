using DataLayer.Models.Otros;

namespace DataLayer.Models.WhatsAppFeature
{
    public class CmpWhatsAppConfiguration : AuditableEntities
    {
        public int ConfigurationId { get; set; }
        public int BussinessId { get; set; }
        public string AccountSid { get; set; } = string.Empty!;
        public string AuthToken { get; set; } = string.Empty!;
        public string WhatsAppNumber { get; set; } = string.Empty!;

    }
}
