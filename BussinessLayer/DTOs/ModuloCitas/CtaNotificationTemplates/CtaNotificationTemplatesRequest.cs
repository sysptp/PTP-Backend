using System.Text.Json.Serialization;

namespace BussinessLayer.DTOs.ModuloCitas.CtaNotificationTemplates
{
    public class CtaNotificationTemplatesRequest
    {
        [JsonIgnore]
        public long NotificationTemplateId { get; set; }
        public long CompanyId { get; set; }
        public int NotificationType { get; set; }
        public long? SmsTemplateId { get; set; }
        public long? WhatsAppTemplateId { get; set; }
        public bool IsActive { get; set; }
    }
}