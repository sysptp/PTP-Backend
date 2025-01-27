using DataLayer.Models.ModuloCitas;
using DataLayer.Models.ModuloGeneral.Empresa;
using DataLayer.Models.ModuloGeneral.Seguridad;
using DataLayer.Models.Otros;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Models.Modulo_Citas
{
    public class CtaAppointments : AuditableEntities
    {
        [Key]
        public int AppointmentId { get; set; }
        public string AppointmentCode { get; set; } = null!;
        public string? Description { get; set; }
        public int IdReasonAppointment { get; set; }
        [ForeignKey("IdReasonAppointment")]
        public CtaAppointmentReason? CtaAppointmentReason { get; set; }
        public DateTime AppointmentDate { get; set; }
        public TimeSpan AppointmentTime { get; set; }
        public int IdPlaceAppointment { get; set; }
        [ForeignKey("IdPlaceAppointment")]
        public CtaMeetingPlace? CtaMeetingPlace { get; set; }
        public int IdState { get; set; }
        [ForeignKey("IdState")]
        public CtaState? CtaState { get; set; }
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
        public int UserId { get; set; }
        public Usuario? Usuario { get; set; }
        public int? AreaId {  get; set; }
        public long CompanyId { get; set; }
        [ForeignKey("CompanyId")]
        public GnEmpresa? GnEmpresa { get; set; }
        public List<CtaAppointmentManagement>? CtaAppointmentManagement { get; set; }
        public List<CtaAppointmentContacts>? CtaAppointmentContacts { get; set; }
        public List<CtaAppointmentUsers>? CtaAppointmentUsers { get; set; }
    }
}
