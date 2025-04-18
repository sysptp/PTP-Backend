using BussinessLayer.DTOs.ModuloCitas.CtaAppointments;
using System.Text.Json.Serialization;

namespace BussinessLayer.DTOs.ModuloCitas.CtaSessions
{
    public class CtaSessionsResponse
    {
        public int IdSession { get; set; }
        public string? Description { get; set; }
        public int? AssignedUserId { get; set; }
        public string? AssignedUser { get; set; }
        public string? UserEmail { get; set; }
        public string? UserPhoneNumber { get; set; }
        public DateTime FirstSessionDate { get; set; }
        public DateTime LastSessionDate { get; set; }
        public DateTime SessionEndDate { get; set; }
        public int CompletedAppointments { get; set; }
        public int TotalAppointments { get; set; }
        public int RepeatEvery { get; set; }
        public int RepeatUnitId { get; set; }
        public string RepeatUnitDescription { get; set; } = null!;
        public long CompanyId { get; set; }
        public string? AppointmentDescription { get; set; }
        public int IdReasonAppointment { get; set; }
        public TimeSpan AppointmentTime { get; set; }
        public int IdPlaceAppointment { get; set; }
        public int IdState { get; set; }
        public bool IsConditionedTime { get; set; }
        public TimeSpan EndAppointmentTime { get; set; }
        public TimeSpan NotificationTime { get; set; }
        public int? AreaId { get; set; }

        [JsonPropertyName("Participants")]
        public List<AppointmentParticipantsResponse>? Participants { get; set; } = new();
    }
}
