using BussinessLayer.DTOs.ModuloCitas.BookingPortal;

namespace BussinessLayer.Interfaces.Services.ModuloCitas
{
    public interface ICtaBookingPortalUsersService
    {
        Task<List<BookingPortalUserResponse>> GetUsersByPortalIdAsync(int portalId);
        Task<List<BookingPortalUserResponse>> GetPortalsByUserIdAsync(int userId);
        Task<BookingPortalUserResponse?> GetMainAssigneeByPortalIdAsync(int portalId);
        Task<BookingPortalUserResponse> AddUserToPortalAsync(BookingPortalUserRequest request);
        Task RemoveUserFromPortalAsync(int portalId, int userId);
        Task<BookingPortalUserResponse> SetMainAssigneeAsync(int portalId, int userId);
        Task<List<BookingPortalUserResponse>> UpdatePortalUsersAsync(int portalId, List<BookingPortalUserRequest> users);
    }
}