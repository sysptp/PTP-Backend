using System.Text.Json.Serialization;

namespace BussinessLayer.DTOs.ModuloCitas.CtaMessageTemplates
{
    public class CtaMessageTemplatesRequest
    {
        [JsonIgnore]
        public long Id { get; set; }
        public long? CompanyId { get; set; }
        public int MessageTypeId { get; set; }
        public string? MessageTitle { get; set; }
        public string MessageContent { get; set; } = null!;
        public bool IsInteractive { get; set; }
        public string? ButtonConfig { get; set; }
    }
}
