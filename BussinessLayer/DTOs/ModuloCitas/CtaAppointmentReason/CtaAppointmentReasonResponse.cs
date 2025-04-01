
namespace BussinessLayer.DTOs.ModuloCitas.CtaAppointmentReason
{
    public class CtaAppointmentReasonResponse 
    {
        public int IdReason { get; set; }
        public string? Description { get; set; }
        public long CompanyId { get; set; }
        public bool IsDefault { get; set; } = false;
    }
}

