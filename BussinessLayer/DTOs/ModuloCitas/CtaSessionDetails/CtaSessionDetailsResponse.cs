using BussinessLayer.DTOs.ModuloCitas.CtaAppointments;
using BussinessLayer.DTOs.ModuloCitas.CtaSessions;

namespace BussinessLayer.DTOs.ModuloCitas.CtaSessionDetails
{
    public class CtaSessionDetailsResponse
    {
        public int IdSessionDetail { get; set; }
        public int IdAppointment { get; set; }
        public CtaAppointmentsResponse? Appointment { get; set; }
        public int IdSession { get; set; }
        public CtaSessionsResponse? Session { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
