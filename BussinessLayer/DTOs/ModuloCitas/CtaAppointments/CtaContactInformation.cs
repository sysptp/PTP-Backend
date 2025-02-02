

namespace BussinessLayer.DTOs.ModuloCitas.CtaAppointments
{
    public class CtaContactInformation
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string ContactNumber { get; set; } = null!;
        public string ContactEmail { get; set; } = null!;
        public int ContactTypeId { get; set; }
        public string? ContactTipeDescription { get; set; } 
        public int AppointmentId { get; set; }
        public long CompanyId { get; set; }
    }
}
