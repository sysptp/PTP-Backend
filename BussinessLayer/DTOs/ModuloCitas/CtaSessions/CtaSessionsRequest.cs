using BussinessLayer.DTOs.ModuloCitas.CtaAppointments;
using System.Text.Json.Serialization;

namespace BussinessLayer.DTOs.ModuloCitas.CtaSessions
{
    public class CtaSessionsRequest
    {
        [JsonIgnore]
        public int IdSession { get; set; }
        public DateTime FirstSessionDate { get; set; }
        public int TotalAppointments { get; set; }
        public DateTime SessionEndDate { get; set; }
        [JsonIgnore]
        public int CompletedAppointments { get; set; } = 0;
        public int FrequencyInDays { get; set; }
        [JsonPropertyName("AppointmentsInformation")]
        public AppointmentInformation AppointmentInformation { get; set; } = null!;
    }
}
