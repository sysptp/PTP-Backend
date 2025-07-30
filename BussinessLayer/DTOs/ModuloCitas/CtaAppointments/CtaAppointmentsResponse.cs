using System.Text.Json.Serialization;

namespace BussinessLayer.DTOs.ModuloCitas.CtaAppointments
{
    public class CtaAppointmentsResponse 
    {
        public int AppointmentId { get; set; }
        public string AppointmentCode { get; set; } = null!;
        public string? Description { get; set; }
        public int IdReasonAppointment { get; set; }
        public string? ReasonDescription { get; set; }
        public DateTime AppointmentDate { get; set; }
        public TimeSpan AppointmentTime { get; set; }
        public int IdPlaceAppointment { get; set; }
        public string? MeetingPlaceDescription { get; set; }
        public int IdState { get; set; }
        public string? StateDescription { get; set; }
        public bool IsConditionedTime { get; set; }
        public TimeSpan EndAppointmentTime { get; set; }
        public bool SendEmail { get; set; } = false;
        public bool SendSms { get; set; } = false;
        public bool SendEmailReminder { get; set; } = false;
        public bool SendSmsReminder { get; set; } = false;
        public int? DaysInAdvance { get; set; }
        public TimeSpan NotificationTime { get; set; }
        public bool NotifyClosure { get; set; } = false;
        public bool NotifyAssignedUserEmail { get; set; } = false;
        public bool NotifyAssignedUserSms { get; set; } = false;
        public int? AreaId { get; set; }
        public string? Area { get; set; } 
        public int UserId { get; set; }
        public string? AssignedUser { get; set; }
        public long CompanyId { get; set; }
        public string? CompanyName { get; set;}
         [JsonPropertyName("Participants")]
        public List<AppointmentParticipantsResponse>? Participants { get; set; } = new();

    }
}
