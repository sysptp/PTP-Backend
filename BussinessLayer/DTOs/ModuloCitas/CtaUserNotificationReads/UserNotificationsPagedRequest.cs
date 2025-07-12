namespace BussinessLayer.DTOs.ModuloCitas.CtaUserNotificationReads
{
    public class UserNotificationsPagedRequest
    {
        public int UserId { get; set; }
        public long CompanyId { get; set; }
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 20;
        public bool? IsRead { get; set; }
        public string NotificationType { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
    }
}
