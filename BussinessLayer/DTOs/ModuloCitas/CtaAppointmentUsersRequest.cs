
using System.Text.Json.Serialization;

namespace BussinessLayer.DTOs.ModuloCitas
{
    public class CtaAppointmentUsersRequest 
    {
        [JsonIgnore]
        public int Id { get; set; }
        public int UserId { get; set; }
        [JsonIgnore]
        public int AppointmentId { get; set; }
        [JsonIgnore]
        public long CompanyId { get; set; }
    }
}
