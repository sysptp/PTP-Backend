
namespace BussinessLayer.DTOs.ModuloCitas.BookingPortal
{
    public class CtaClientInfoResponse
    {
        public string ClientName { get; set; } = null!;
        public string ClientPhone { get; set; } = null!;
        public string? ClientEmail { get; set; }
        public string? ClientNickName { get; set; }
        public string ClientType { get; set; } = null!; // "Contact" o "Guest"
        public bool IsExistingClient { get; set; } = true;
    }
}
