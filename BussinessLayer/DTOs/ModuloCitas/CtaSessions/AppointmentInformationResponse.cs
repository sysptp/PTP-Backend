
using BussinessLayer.DTOs.ModuloCitas.CtaAppointments;
using System.Text.Json.Serialization;

namespace BussinessLayer.DTOs.ModuloCitas.CtaSessions
{
    public class AppointmentInformationResponse
    {
        [JsonIgnore]
        public int AppointmentId { get; set; }
        [JsonIgnore]
        public string? AppointmentCode { get; set; }
        public string? Description { get; set; }
        public int IdReasonAppointment { get; set; }
        public TimeSpan AppointmentTime { get; set; }
        public int IdPlaceAppointment { get; set; }
        public int IdState { get; set; }
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
        public long CompanyId { get; set; }
    }
    
}
