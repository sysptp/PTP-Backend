using System.Text.Json.Serialization;

namespace BussinessLayer.DTOs.ModuloCitas.CtaWhatsAppTemplates
{
    public class CtaWhatsAppTemplatesRequest
    {
        [JsonIgnore]
        public long Id { get; set; }
        public long CompanyId { get; set; }
        public string? MessageTitle { get; set; }
        public string MessageContent { get; set; } = null!;
        public bool IsInteractive { get; set; }
        public string? ButtonConfig { get; set; }
        public int? HeaderType { get; set; }
        public string? FooterText { get; set; }
    }
}
