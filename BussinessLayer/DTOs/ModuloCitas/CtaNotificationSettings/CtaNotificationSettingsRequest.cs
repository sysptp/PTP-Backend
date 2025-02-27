using System.Text.Json.Serialization;

namespace BussinessLayer.DTOs.ModuloCitas.CtaNotificationSettings
{
    public class CtaNotificationSettingsRequest
    {
        [JsonIgnore]
        public long Id { get; set; }
        public long CompanyId { get; set; }
        public int TemplateTypeId { get; set; }
        public bool SendEmail { get; set; }
        public bool SendSMS { get; set; }
        public bool SendWhatsApp { get; set; }
        public int ParticipantTypeId { get; set; }
    }
}
