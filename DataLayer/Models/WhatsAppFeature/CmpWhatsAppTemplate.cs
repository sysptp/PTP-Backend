using DataLayer.Models.Otros;

namespace DataLayer.Models.WhatsAppFeature
{
    public class CmpWhatsAppTemplate : AuditableEntities
    {
        public int TemplateId { get; set; }
        public string MessageContent { get; set; } = string.Empty!;
        public int BussinesId { get; set; }
    }
}
