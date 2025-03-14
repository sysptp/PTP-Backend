
namespace BussinessLayer.DTOs.ModuloCitas.CtaNotificationSettings
{
    public class CtaNotificationSettingsResponse
    {
        public long Id { get; set; }
        public long CompanyId { get; set; }
        public bool SendEmail { get; set; }
        public bool SendSMS { get; set; }
        public bool SendWhatsApp { get; set; }
        public int ParticipantTypeId { get; set; }
    }
}
