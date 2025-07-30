using BussinessLayer.DTOs.ModuloCitas.CtaAppointments;

namespace BussinessLayer.DTOs.ModuloCitas.CtaAppointmentManagement
{
    public class CtaAppointmentManagementResponse 
    {
        public int IdManagementAppointment { get; set; }
        public int? AppointmentId { get; set; }
        public CtaAppointmentsResponse? Appointment { get; set; }
        public string? Comment { get; set; }
        public string? AppointmentSequence { get; set; }
        public string? UsuarioAdicion { get; set; } = string.Empty;
        public DateTime? FechaAdicion { get; set; }
    }
}
