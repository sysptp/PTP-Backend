
namespace BussinessLayer.DTOs.ModuloCitas.CtaUserNotificationReads
{
    public class CtaUserNotificationReadsResponse
    {
        public int? NotificationId { get; set; }
        public int UserId { get; set; }
        public string? Type { get; set; }

        public bool IsRead { get; set; } = false;

        public DateTime? ReadDate { get; set; }

        public string Title { get; set; } = null!;

        public string? Message { get; set; }

        public string? Data { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

        public int? AppointmentId { get; set; }

        public long CompanyId { get; set; }
    }
}
