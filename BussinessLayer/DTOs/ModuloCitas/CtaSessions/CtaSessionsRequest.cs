using System.Text.Json.Serialization;

namespace BussinessLayer.DTOs.ModuloCitas.CtaSessions
{
    public class CtaSessionsRequest
    {
        [JsonIgnore]
        public int IdSession { get; set; }
        public string? Description { get; set; }
        public int? IdClient { get; set; }
        public int? IdUser { get; set; }
        public DateTime FirstSessionDate { get; set; }
        public int? IdReason { get; set; }
        public int TotalAppointments { get; set; }
        public DateTime SessionEndDate { get; set; }
        [JsonIgnore]
        public int CompletedAppointments { get; set; } = 0;
        public int FrequencyInDays { get; set; }
        public string AppointmentDescription { get; set; } = null!;
        public long CompanyId { get; set; }
        public int IdAppointment { get; set; }
        public int IdReasonAppointment { get; set; }
        public DateTime AppointmentDate { get; set; }
        public TimeSpan AppointmentTime { get; set; }
        public int IdPlaceAppointment { get; set; }
        public int IdState { get; set; }
        public bool IsConditionedTime { get; set; }
        public DateTime EndAppointmentDate { get; set; }
        public TimeSpan EndAppointmentTime { get; set; }
        public bool SendEmail { get; set; } = false;
        public bool SendSms { get; set; } = false;
        public bool SendEmailReminder { get; set; } = false;
        public bool SendSmsReminder { get; set; } = false;
        public int? DaysInAdvance { get; set; }
        public TimeSpan NotificationTime { get; set; }
        public bool NotifyClosure { get; set; } = false;
        public string? AssignedUserAppointment { get; set; }
        public bool NotifyAssignedUserEmail { get; set; } = false;
        public bool NotifyAssignedUserSms { get; set; } = false;
        public bool IsClient { get; set; } = false;
        public string FullName { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public string Email { get; set; } = null!;
    }
}
