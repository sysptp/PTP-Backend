using DataLayer.Models.Otros;

namespace DataLayer.Models.SendGridModule
{
    public class SendGridTemplate : AuditableEntities
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Content { get; set; }
        public bool IsHtml { get; set; }
        public bool IsActive { get; set; }
        public int BussinesId { get; set; }

    }
}