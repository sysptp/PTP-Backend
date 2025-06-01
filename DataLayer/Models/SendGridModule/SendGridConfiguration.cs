using DataLayer.Models.Otros;

namespace DataLayer.Models.SendGridModule
{
    public class SendGridConfiguration : AuditableEntities
    {
        public int Id { get; set; }
        public string? ApiKey { get; set; }
        public string? FromEmail { get; set; }
        public bool IsActive { get; set; }
        public int BussinesId { get; set; }
    }
}