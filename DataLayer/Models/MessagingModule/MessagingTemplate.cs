using DataLayer.Models.Otros;

namespace DataLayer.Models.MessagingModule
{
    public class MessagingTemplate : AuditableEntities
    {
        public int TemplateId { get; set; }
        public string MessageContent { get; set; } = string.Empty!;
        public int BussinesId { get; set; }
    }
}
