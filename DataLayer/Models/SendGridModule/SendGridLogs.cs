using DataLayer.Models.Otros;

namespace DataLayer.Models.SendGridModule
{
    public class SendGridLogs : AuditableEntities
    {
        public int Id { get; set; }
        public int BussinesId { get; set; }
        public int TemplateId { get; set; }
        public string? RecipientEmail { get; set; }
        public string? Subject { get; set; }
        public string? Status { get; set; }
        public string? ErrorMessage { get; set; }
    }
}