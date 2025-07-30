using BussinessLayer.DTOs.ModuloCitas.BookingPortal;

namespace BussinessLayer.Interfaces.Services.ModuloCitas
{
    public interface ICtaBookingPortalAreasService
    {
        Task<List<BookingPortalAreaResponse>> GetAreasByPortalIdAsync(int portalId);
        Task<List<BookingPortalAreaResponse>> GetPortalsByAreaIdAsync(int areaId);
        Task<BookingPortalAreaResponse?> GetDefaultAreaByPortalIdAsync(int portalId);
        Task<BookingPortalAreaResponse> AddAreaToPortalAsync(BookingPortalAreaRequest request);
        Task RemoveAreaFromPortalAsync(int portalId, int areaId);
        Task<BookingPortalAreaResponse> SetDefaultAreaAsync(int portalId, int areaId);
        Task<List<BookingPortalAreaResponse>> UpdatePortalAreasAsync(int portalId, List<BookingPortalAreaRequest> areas);
    }
}