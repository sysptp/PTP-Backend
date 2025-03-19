using DataLayer.Models.Otros;

namespace DataLayer.Models.WhatsAppFeature
{
    public class CmpWhatsAppLog : AuditableEntities
    {
        public string ToPhoneNumber { get; set; } = string.Empty!;
        public int WhatsAppConfigurationId { get; set; }
        public CmpWhatsAppConfiguration? CmpWhatsAppConfiguration { get; set; }
        public string MessageContent { get; set; } = string.Empty!;
        public string MessageReponse { get; set; } = string.Empty!;
        public int BussinesId { get; set; }
    }
}
