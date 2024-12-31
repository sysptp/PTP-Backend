using BussinessLayer.DTOs.ModuloCitas.CtaAppointments;
using BussinessLayer.DTOs.ModuloCitas.CtaState;

namespace BussinessLayer.DTOs.ModuloCitas.CtaAppointmentMovements
{
    public class CtaAppointmentMovementsResponse
    {
        public int IdMovement { get; set; }
        public string? Description { get; set; }
        public int? EmailSmsType { get; set; }
        public string FromEmail { get; set; } = null!;
        public string ToEmail { get; set; } = null!;
        public int? IdState { get; set; }
        public CtaStateResponse? State { get; set; }
        public int? IdMessage { get; set; }
        public int? IdAppointment { get; set; }
        public CtaAppointmentsResponse? Appointments { get; set; }
        public bool Sent { get; set; } = true;
    }
}
