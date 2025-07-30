namespace BussinessLayer.DTOs.ModuloCitas.CtaSmsTemplates
{
    public class CtaSmsTemplatesResponse
    {
        public long Id { get; set; }
        public long CompanyId { get; set; }
        public string? CompanyIdDescription { get; set; }
        public string? MessageTitle { get; set; }
        public string MessageContent { get; set; } = null!;
    }
}
