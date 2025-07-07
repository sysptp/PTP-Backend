namespace BussinessLayer.DTOs.ModuloCitas.BookingPortal
{
    public class PublicAppointmentResponse
    {
        public bool Success { get; set; }
        public string? AppointmentCode { get; set; }
        public string? Message { get; set; }
        public DateTime? AppointmentDate { get; set; }
        public TimeSpan? AppointmentTime { get; set; }
        public string? ConfirmationDetails { get; set; }
    }
}