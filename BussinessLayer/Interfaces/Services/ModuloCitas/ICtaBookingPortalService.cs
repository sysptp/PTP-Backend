using BussinessLayer.DTOs.ModuloCitas.BookingPortal;

namespace BussinessLayer.Interfaces.Services.ModuloCitas
{
    public interface ICtaBookingPortalService
    {
        Task<BookingPortalConfigResponse> CreatePortalAsync(BookingPortalConfigRequest request);
        Task<BookingPortalConfigResponse> UpdatePortalAsync(int id, BookingPortalConfigRequest request);
        Task DeletePortalAsync(int id);
        Task<BookingPortalConfigResponse?> GetPortalByIdAsync(int id);
        Task<BookingPortalConfigResponse?> GetPortalBySlugAsync(string slug);
        Task<List<BookingPortalConfigResponse>> GetPortalsByCompanyAsync(long companyId);
        Task<ClientAuthenticationResponse> AuthenticateClientAsync(ClientAuthenticationRequest request);
        Task<AvailableSlotResponse> GetAvailableSlotsAsync(AvailableSlotRequest request);
        Task<PublicAppointmentResponse> CreatePublicAppointmentAsync(PublicAppointmentRequest request);
        Task<string> GenerateUniqueSlugAsync(string baseName);
        Task<CtaClientInfoResponse?> GetClientInfoAsync(CtaClientInfoRequest request);
    }
}
