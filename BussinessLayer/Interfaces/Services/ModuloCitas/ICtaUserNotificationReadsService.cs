using BussinessLayer.DTOs.ModuloCitas.CtaUserNotificationReads;
using BussinessLayer.Interfaces.Services.IOtros;
using DataLayer.Models.ModuloCitas;

namespace BussinessLayer.Interfaces.Services.ModuloCitas
{
    public interface ICtaUserNotificationReadsService : IGenericService<CtaUserNotificationReadsRequest, CtaUserNotificationReadsResponse, CtaUserNotificationReads>
    {
        Task<UserNotificationsPagedResponse> GetUserNotificationsPaginatedAsync(UserNotificationsPagedRequest request);
        Task<int> GetUnreadCountAsync(int userId, long companyId);
        Task<bool> MarkNotificationAsReadAsync(MarkNotificationAsReadRequest request);
        Task<bool> MarkMultipleNotificationsAsReadAsync(MarkMultipleNotificationsAsReadRequest request);
        Task<bool> MarkAllNotificationsAsReadAsync(int userId, long companyId);
        Task<List<CtaUserNotificationReadsResponse>> GetNotificationsByUserAsync(int userId, long companyId, bool? isRead = null);
        Task<CtaUserNotificationReadsResponse> CreateNotificationAsync(CtaUserNotificationReadsRequest request);
    }
}
