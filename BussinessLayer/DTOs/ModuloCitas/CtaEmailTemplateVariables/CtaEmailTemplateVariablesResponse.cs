
namespace BussinessLayer.DTOs.ModuloCitas.CtaEmailTemplateVariables
{
    public class CtaEmailTemplateVariablesResponse
    {
        public long VariableId { get; set; }
        public int TemplateTypeId { get; set; }
        public string VariableName { get; set; } = null!;
        public string VariableDescription { get; set; } = null!;
    }
}
