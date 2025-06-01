using System.Text.Json.Serialization;

namespace BussinessLayer.DTOs.ModuloCitas.CtaAppointments
{
    public class CtaAppointmentsRequest
    {
        [JsonIgnore]
        public int AppointmentId { get; set; }
        [JsonIgnore]
        public string? AppointmentCode { get; set; } 
        public string? Description { get; set; }
        public int IdReasonAppointment { get; set; }
        public DateTime AppointmentDate { get; set; }
        public TimeSpan AppointmentTime { get; set; }
        public int IdPlaceAppointment { get; set; }
        public int IdState { get; set; }
        public bool IsConditionedTime { get; set; }
        public TimeSpan EndAppointmentTime { get; set; }
        public TimeSpan NotificationTime { get; set; }
        public int? AreaId { get; set; }
        public int AssignedUser { get; set; }
        public long CompanyId { get; set; }
        [JsonPropertyName("Participants")]
        public List<AppointmentParticipantsRequest> AppointmentParticipants { get; set; } = new();
    }
}
