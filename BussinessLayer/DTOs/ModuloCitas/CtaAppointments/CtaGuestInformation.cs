
namespace BussinessLayer.DTOs.ModuloCitas.CtaAppointments
{
    public class CtaGuestInformation
    {
        public int Id { get; set; }
        public string Names { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string? NickName { get; set; }
    }
}
