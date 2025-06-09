using BussinessLayer.DTOs.ModuloCitas.CtaUserNotificationReads;
using BussinessLayer.Interfaces.Repository.ModuloCitas;
using BussinessLayer.Repository.ROtros;
using DataLayer.Models.ModuloCitas;
using DataLayer.PDbContex;
using Microsoft.EntityFrameworkCore;

namespace BussinessLayer.Repository.ModuloCitas
{
    public class CtaUserNotificationReadsRepository : GenericRepository<CtaUserNotificationReads>, ICtaUserNotificationReadsRepository
    {
        public CtaUserNotificationReadsRepository(PDbContext dbContext, ITokenService tokenService) : base(dbContext, tokenService)
        {
        }

        public async Task<UserNotificationsPagedResponse> GetUserNotificationsPaginatedAsync(UserNotificationsPagedRequest request)
        {
            var query = _context.CtaUserNotificationReads
                .Where(n => n.UserId == request.UserId && n.CompanyId == request.CompanyId);

            if (request.IsRead.HasValue)
                query = query.Where(n => n.IsRead == request.IsRead.Value);

            if (request.FromDate.HasValue)
                query = query.Where(n => n.CreatedDate >= request.FromDate.Value);

            if (request.ToDate.HasValue)
                query = query.Where(n => n.CreatedDate <= request.ToDate.Value);

            var totalCount = await query.CountAsync();
            var unreadCount = await _context.CtaUserNotificationReads
                .Where(n => n.UserId == request.UserId && n.CompanyId == request.CompanyId && !n.IsRead)
                .CountAsync();

            var notifications = await query
                .OrderByDescending(n => n.CreatedDate)
                .Skip((request.Page - 1) * request.PageSize)
                .Take(request.PageSize)
                .Include(n => n.Usuario)
                .ToListAsync();

            var response = notifications.Select(n => new CtaUserNotificationReadsResponse
            {
                UserId = n.UserId,
                NotificationId = n.NotificationId,
                IsRead = n.IsRead,
                ReadDate = n.ReadDate,
                Title = n.Title,
                Message = n.Message,
                Data = n.Data,
                CreatedDate = n.CreatedDate,
                AppointmentId = n.AppointmentId,
                CompanyId = n.CompanyId
            }).ToList();

            return new UserNotificationsPagedResponse
            {
                Notifications = response,
                TotalCount = totalCount,
                Page = request.Page,
                PageSize = request.PageSize,
                TotalPages = (int)Math.Ceiling((double)totalCount / request.PageSize),
                UnreadCount = unreadCount
            };
        }

        public async Task<int> GetUnreadCountAsync(int userId, long companyId)
        {
            return await _context.CtaUserNotificationReads
                .Where(n => n.UserId == userId && n.CompanyId == companyId && !n.IsRead)
                .CountAsync();
        }

        public async Task<bool> MarkAsReadAsync(int notificationId, int userId)
        {
            var notification = await _context.CtaUserNotificationReads
                .FirstOrDefaultAsync(n => n.NotificationId == notificationId && n.UserId == userId);

            if (notification == null) return false;

            notification.IsRead = true;
            notification.ReadDate = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> MarkMultipleAsReadAsync(List<int> notificationIds, int userId)
        {
            var notifications = await _context.CtaUserNotificationReads
                .Where(n => notificationIds.Contains(n.NotificationId) && n.UserId == userId && !n.IsRead)
                .ToListAsync();

            if (!notifications.Any()) return false;

            foreach (var notification in notifications)
            {
                notification.IsRead = true;
                notification.ReadDate = DateTime.UtcNow;
            }

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> MarkAllAsReadAsync(int userId, long companyId)
        {
            var notifications = await _context.CtaUserNotificationReads
                .Where(n => n.UserId == userId && n.CompanyId == companyId && !n.IsRead)
                .ToListAsync();

            if (!notifications.Any()) return false;

            foreach (var notification in notifications)
            {
                notification.IsRead = true;
                notification.ReadDate = DateTime.UtcNow;
            }

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<CtaUserNotificationReads>> GetNotificationsByUserAsync(int userId, long companyId, bool? isRead = null)
        {
            var query = _context.CtaUserNotificationReads
                .Where(n => n.UserId == userId && n.CompanyId == companyId);

            if (isRead.HasValue)
                query = query.Where(n => n.IsRead == isRead.Value);

            return await query
                .OrderByDescending(n => n.CreatedDate)
                .ToListAsync();
        }

        public async Task<CtaUserNotificationReads> CreateNotificationAsync(CtaUserNotificationReadsRequest request)
        {
            var notification = new CtaUserNotificationReads
            {
                UserId = request.UserId,
                IsRead = false,
                Title = request.Title,
                Message = request.Message,
                Data = request.Data,
                CreatedDate = DateTime.UtcNow,
                AppointmentId = request.AppointmentId,
                CompanyId = request.CompanyId
            };

            _context.CtaUserNotificationReads.Add(notification);
            await _context.SaveChangesAsync();
            return notification;
        }
    }
}
