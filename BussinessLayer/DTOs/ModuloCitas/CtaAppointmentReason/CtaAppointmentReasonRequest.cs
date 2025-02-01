using System.Text.Json.Serialization;

namespace BussinessLayer.DTOs.ModuloCitas.CtaAppointmentReason
{
    public class CtaAppointmentReasonRequest
    {
        [JsonIgnore]
        public int IdReason { get; set; }
        public string? Description { get; set; }
        public DateTime CreationDate { get; set; } = DateTime.Now;
        public long CompanyId { get; set; }
        public bool IsDefault { get; set; } = false;
    }
}
