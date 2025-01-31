using System.Text.Json.Serialization;

namespace BussinessLayer.DTOs.ModuloCitas.CtaAppointmentGuest
{
    public class CtaAppointmentGuestRequest
    {
        [JsonIgnore]
        public int Id { get; set; }
        public int GuestId { get; set; }
        [JsonIgnore]
        public int AppointmentId { get; set; }
        [JsonIgnore]
        public long CompanyId { get; set; }
    }
}
