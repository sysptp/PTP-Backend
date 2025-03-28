using DataLayer.Models.Otros;

namespace DataLayer.Models.MessagingModule
{
    public class MessagingLogs : AuditableEntities
    {
        public int Id { get; set; }
        public string FromPhoneNumber { get; set; } = string.Empty!;
        public string ToPhoneNumber { get; set; } = string.Empty!;
        public string MessageContent { get; set; } = string.Empty!;
        public string MessageReponse { get; set; } = string.Empty!;
        public int BussinesId { get; set; }
    }
}
