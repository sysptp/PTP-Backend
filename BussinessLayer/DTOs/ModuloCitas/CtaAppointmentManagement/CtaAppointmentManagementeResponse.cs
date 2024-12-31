using BussinessLayer.DTOs.ModuloCitas.CtaAppointments;

namespace BussinessLayer.DTOs.ModuloCitas.CtaAppointmentManagement
{
    public class CtaAppointmentManagementResponse 
    {
        public int IdManagementAppointment { get; set; }
        public int IdAppointment { get; set; }
        public CtaAppointmentsResponse? Appointment { get; set; }
        public string? Comment { get; set; }

    }
}
