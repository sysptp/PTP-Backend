using System.Text.Json.Serialization;

namespace BussinessLayer.DTOs.ModuloCitas.CtaCitaConfiguracion
{
    public class CtaCitaConfiguracionRequest 
    {
        [JsonIgnore]
        public int IdConfiguration { get; set; }
        public bool SendEmail { get; set; } = false;
        public bool SendSms { get; set; } = false;
        public bool SendEmailReminder { get; set; } = false;
        public bool SendSmsReminder { get; set; } = false;
        public bool SendWhatsapp { get; set; } = false;
        public bool SendWhatsappReminder { get; set; } = false;
    }
}
