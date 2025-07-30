
namespace BussinessLayer.DTOs.ModuloCitas.BookingPortal
{
    public class ClientAuthenticationRequest
    {
        public string PhoneNumber { get; set; } = null!;
        public string PortalSlug { get; set; } = null!;
        public long CompanyId { get; set; }
    }
}
