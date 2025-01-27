
using System.Text.Json.Serialization;

namespace BussinessLayer.DTOs.ModuloCitas
{
    public class CtaAppointmentContactsRequest
    {
        [JsonIgnore]
        public int Id { get; set; }
        public int ContactTypeId { get; set; }
        public int ContactId { get; set; }
        [JsonIgnore]
        public int AppointmentId { get; set; }
        [JsonIgnore]
        public long CompanyId { get; set; }
    }
}
