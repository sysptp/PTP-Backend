using System.Text.Json.Serialization;

namespace BussinessLayer.DTOs.ModuloCitas.CtaSmsTemplates
{
    public class CtaSmsTemplatesRequest
    {
        [JsonIgnore]
        public long Id { get; set; }
        public long CompanyId { get; set; }
        public string? MessageTitle { get; set; }
        public string MessageContent { get; set; } = null!;
        public int CharacterLimit { get; set; }
    }
}
