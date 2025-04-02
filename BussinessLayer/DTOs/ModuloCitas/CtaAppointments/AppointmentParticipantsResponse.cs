
namespace BussinessLayer.DTOs.ModuloCitas.CtaAppointments
{
    public class AppointmentParticipantsResponse
    {
        public int ParticipantId { get; set; }
        public int ParticipantTypeId { get; set; }
        public string ParticipantName { get; set; } = null!;
        public string ParticipantEmail { get; set; } = null!;
        public string ParticipantPhone { get; set; } = null!;
        public int AppointmentId { get; set; }
        public long CompanyId { get; set; }
    }
}
