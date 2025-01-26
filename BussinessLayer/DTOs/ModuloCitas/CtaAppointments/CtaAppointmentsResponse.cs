
using BussinessLayer.DTOs.ModuloCitas.CtaAppointmentManagement;
using BussinessLayer.DTOs.ModuloCitas.CtaAppointmentReason;
using BussinessLayer.DTOs.ModuloCitas.CtaMeetingPlace;
using BussinessLayer.DTOs.ModuloCitas.CtaState;
using System.Text.Json.Serialization;

namespace BussinessLayer.DTOs.ModuloCitas.CtaAppointments
{
    public class CtaAppointmentsResponse 
    {
        public int AppointmentId { get; set; }
        public string AppointmentCode { get; set; } = null!;
        public string? Description { get; set; }
        public int IdReasonAppointment { get; set; }
        public CtaAppointmentReasonResponse? AppointmentReason { get; set; }
        public DateTime AppointmentDate { get; set; }
        public TimeSpan AppointmentTime { get; set; }
        public int IdPlaceAppointment { get; set; }
        public CtaMeetingPlaceResponse? MeetingPlace { get; set; }
        public int IdState { get; set; }
        public CtaStateResponse? State { get; set; }
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
        public bool NotifyAssignedUserEmail { get; set; } = false;
        public bool NotifyAssignedUserSms { get; set; } = false;
        public int? AreaId { get; set; }
        public int UserId { get; set; }
        public long CompanyId { get; set; }
        public string? CompanyName { get; set;}
        [JsonPropertyName("Management")]
        public List<CtaAppointmentManagementResponse>? CtaAppointmentManagement { get; set; }
        [JsonPropertyName("Contacts")]
        public List<CtaAppointmentContactsResponse>? CtaAppointmentContacts { get; set; }
        [JsonPropertyName("UsersInvited")]
        public List<CtaAppointmentUsersResponse>? CtaAppointmentUsers { get; set; }
    }
}
