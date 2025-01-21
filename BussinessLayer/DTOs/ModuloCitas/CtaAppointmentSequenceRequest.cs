using System.Text.Json.Serialization;

namespace BussinessLayer.DTOs.ModuloCitas
{
    public class CtaAppointmentSequenceRequest
    {
        [JsonIgnore]
        public int Id { get; set; }
        public string? SequenceIdentifier { get; set; }
        public long CompanyId { get; set; }
        [JsonIgnore]
        public long SequenceNumber { get; set; }
        public string? Prefix { get; set; }
        public string? Suffix { get; set; }
        public DateTime LastUsed { get; set; }
        public int IncrementBy { get; set; }
        public bool IsActive { get; set; }
        public long MaxValue { get; set; }
        public long MinValue { get; set; }
    }
}
