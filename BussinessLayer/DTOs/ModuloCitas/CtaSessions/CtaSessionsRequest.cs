using System.Text.Json.Serialization;

namespace BussinessLayer.DTOs.ModuloCitas.CtaSessions
{
    public class CtaSessionsRequest
    {
        [JsonIgnore]
        public int IdSession { get; set; }
        public DateTime FirstSessionDate { get; set; }
        public DateTime SessionEndDate { get; set; }
        public int AssignedUser { get; set; }
        [JsonIgnore]
        public int CompletedAppointments { get; set; } = 0;
        public int RepeatEvery { get; set; }
        public int RepeatUnitId { get; set; }
        [JsonPropertyName("AppointmentsInformation")]
        public AppointmentInformation AppointmentInformation { get; set; } = new();
    }
}
