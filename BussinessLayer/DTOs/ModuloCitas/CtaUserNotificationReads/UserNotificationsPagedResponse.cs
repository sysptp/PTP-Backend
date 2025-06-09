
namespace BussinessLayer.DTOs.ModuloCitas.CtaUserNotificationReads
{
    public class UserNotificationsPagedResponse
    {
        public List<CtaUserNotificationReadsResponse>? Notifications { get; set; }
        public int TotalCount { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }
        public int UnreadCount { get; set; }
    }
}
