using System.Text.Json.Serialization;

namespace BussinessLayer.DTOs.ModuloCitas.CtaEmailTemplates
{
    public class CtaEmailTemplatesRequest
    {
        [JsonIgnore]
        public long Id { get; set; }
        public long CompanyId { get; set; }
        public int TemplateTypeId { get; set; }
        public string Subject { get; set; } = null!;
        public string Body { get; set; } = null!;
        public bool AppliesToParticipant { get; set; }
        public bool AppliesToAssignee { get; set; }
    }
}
