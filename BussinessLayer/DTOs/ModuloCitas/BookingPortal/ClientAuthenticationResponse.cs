namespace BussinessLayer.DTOs.ModuloCitas.BookingPortal
{
    public class ClientAuthenticationResponse
    {
        public bool IsAuthenticated { get; set; }
        public bool IsNewClient { get; set; }
        public string? ClientName { get; set; }
        public string? ClientEmail { get; set; }
        public int? ClientId { get; set; }
        public string? ClientType { get; set; } // "Contact" o "Guest"
        public string? AuthToken { get; set; }
        public string? Message { get; set; }
    }
}