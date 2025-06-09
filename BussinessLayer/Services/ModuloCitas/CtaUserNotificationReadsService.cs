using AutoMapper;
using BussinessLayer.DTOs.ModuloCitas.CtaUserNotificationReads;
using BussinessLayer.Interfaces.Repositories;
using BussinessLayer.Interfaces.Repository.ModuloCitas;
using BussinessLayer.Interfaces.Services.ModuloCitas;
using DataLayer.Models.ModuloCitas;

namespace BussinessLayer.Services.ModuloCitas
{
    public class CtaUserNotificationReadsService : GenericService<CtaUserNotificationReadsRequest, CtaUserNotificationReadsResponse, CtaUserNotificationReads>, ICtaUserNotificationReadsService
    {
        private readonly ICtaUserNotificationReadsRepository _notificationRepository;
        private readonly IMapper _mapper;

        public CtaUserNotificationReadsService(
            ICtaUserNotificationReadsRepository notificationRepository,
            IMapper mapper) : base(notificationRepository, mapper)
        {
            _notificationRepository = notificationRepository;
            _mapper = mapper;
        }

        public async Task<UserNotificationsPagedResponse> GetUserNotificationsPaginatedAsync(UserNotificationsPagedRequest request)
        {
            return await _notificationRepository.GetUserNotificationsPaginatedAsync(request);
        }

        public async Task<int> GetUnreadCountAsync(int userId, long companyId)
        {
            return await _notificationRepository.GetUnreadCountAsync(userId, companyId);
        }

        public async Task<bool> MarkNotificationAsReadAsync(MarkNotificationAsReadRequest request)
        {
            return await _notificationRepository.MarkAsReadAsync(request.NotificationId, request.UserId);
        }

        public async Task<bool> MarkMultipleNotificationsAsReadAsync(MarkMultipleNotificationsAsReadRequest request)
        {
            return await _notificationRepository.MarkMultipleAsReadAsync(request.NotificationIds, request.UserId);
        }

        public async Task<bool> MarkAllNotificationsAsReadAsync(int userId, long companyId)
        {
            return await _notificationRepository.MarkAllAsReadAsync(userId, companyId);
        }

        public async Task<List<CtaUserNotificationReadsResponse>> GetNotificationsByUserAsync(int userId, long companyId, bool? isRead = null)
        {
            var notifications = await _notificationRepository.GetNotificationsByUserAsync(userId, companyId, isRead);
            return _mapper.Map<List<CtaUserNotificationReadsResponse>>(notifications);
        }

        public async Task<CtaUserNotificationReadsResponse> CreateNotificationAsync(CtaUserNotificationReadsRequest request)
        {
            var notification = await _notificationRepository.CreateNotificationAsync(request);
            return _mapper.Map<CtaUserNotificationReadsResponse>(notification);
        }
    }
}
