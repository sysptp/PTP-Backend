
namespace BussinessLayer.DTOs.ModuloCitas
{
    public class CtaAppointmentAreaResponse
    {
        public int AreaId { get; set; }
        public string Description { get; set; } = null!;
        public long CompanyId { get; set; }
        public bool IsPrincipal { get; set; }
    }
}
