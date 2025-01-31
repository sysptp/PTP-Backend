using System.Text.Json.Serialization;

namespace BussinessLayer.DTOs.ModuloCitas.CtaGuest
{
    public class CtaGuestRequest
    {
        [JsonIgnore]
        public int Id { get; set; }
        public string Names { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string? NickName { get; set; }
        public long CompanyId { get; set; }
    }
}
