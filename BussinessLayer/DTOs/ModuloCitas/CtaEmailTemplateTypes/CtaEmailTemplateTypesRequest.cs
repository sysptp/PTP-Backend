using System.Text.Json.Serialization;

namespace BussinessLayer.DTOs.ModuloCitas.CtaEmailTemplateTypes
{
    public class CtaEmailTemplateTypesRequest
    {
        [JsonIgnore]
        public int TemplateTypeId { get; set; }
        public string TemplateType { get; set; } = null!;
        public string Description { get; set; } = null!;
    }
}
