using DataLayer.Models.Otros;

namespace DataLayer.Models.MessagingModule
{
    public class MessagingLogs : AuditableEntities
    {
        public string ToPhoneNumber { get; set; } = string.Empty!;
        public int WhatsAppConfigurationId { get; set; }
        public MessagingTemplate? CmpWhatsAppConfiguration { get; set; }
        public string MessageContent { get; set; } = string.Empty!;
        public string MessageReponse { get; set; } = string.Empty!;
        public int BussinesId { get; set; }
    }
}
