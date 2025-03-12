using System.Text.Json.Serialization;

namespace BussinessLayer.DTOs.ModuloCitas.CtaEmailTemplateVariables
{
    public class CtaEmailTemplateVariablesRequest 
    {
        [JsonIgnore]
        public long VariableId { get; set; }
        public int TemplateTypeId { get; set; }
        public string VariableName { get; set; } = null!;
        public string VariableDescription { get; set; } = null!;
    }
}
