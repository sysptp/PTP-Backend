
namespace BussinessLayer.DTOs.ModuloCitas.CtaUserNotificationReads
{
    public class MarkMultipleNotificationsAsReadRequest
    {
        public List<int>? NotificationIds { get; set; }
        public int UserId { get; set; }
    }
}
