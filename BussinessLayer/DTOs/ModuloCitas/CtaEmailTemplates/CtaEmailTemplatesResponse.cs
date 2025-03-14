
namespace BussinessLayer.DTOs.ModuloCitas.CtaEmailTemplates
{
    public class CtaEmailTemplatesResponse
    {
        public long Id { get; set; }
        public long CompanyId { get; set; }
        public string Subject { get; set; } = null!;
        public string Body { get; set; } = null!;
    }
}
