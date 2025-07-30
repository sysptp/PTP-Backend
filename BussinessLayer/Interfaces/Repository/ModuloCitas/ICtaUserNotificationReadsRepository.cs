using BussinessLayer.DTOs.ModuloCitas.CtaUserNotificationReads;
using BussinessLayer.Interfaces.Repositories;
using DataLayer.Models.ModuloCitas;

namespace BussinessLayer.Interfaces.Repository.ModuloCitas
{
    public interface ICtaUserNotificationReadsRepository : IGenericRepository<CtaUserNotificationReads>
    {
        Task<UserNotificationsPagedResponse> GetUserNotificationsPaginatedAsync(UserNotificationsPagedRequest request);
        Task<int> GetUnreadCountAsync(int userId, long companyId);
        Task<bool> MarkAsReadAsync(int notificationId, int userId);
        Task<bool> MarkMultipleAsReadAsync(List<int> notificationIds, int userId);
        Task<bool> MarkAllAsReadAsync(int userId, long companyId);
        Task<List<CtaUserNotificationReads>> GetNotificationsByUserAsync(int userId, long companyId, bool? isRead = null);
        Task<CtaUserNotificationReads> CreateNotificationAsync(CtaUserNotificationReadsRequest notification);
    }
}
