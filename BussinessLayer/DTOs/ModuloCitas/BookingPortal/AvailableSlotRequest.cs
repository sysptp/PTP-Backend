
namespace BussinessLayer.DTOs.ModuloCitas.BookingPortal
{
    public class AvailableSlotRequest
    {
        public string PortalSlug { get; set; } = null!;
        public DateTime Date { get; set; }
        public string? AuthToken { get; set; }
    }
}
