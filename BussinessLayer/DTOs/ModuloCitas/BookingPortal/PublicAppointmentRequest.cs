
namespace BussinessLayer.DTOs.ModuloCitas.BookingPortal
{
    public class PublicAppointmentRequest
    {
        public string PortalSlug { get; set; } = null!;
        public DateTime AppointmentDate { get; set; }
        public TimeSpan AppointmentTime { get; set; }
        public string? Description { get; set; }
        public string? AuthToken { get; set; }
        public int? AreaId { get; set; }
        public int? AssignedUser { get; set; }

        // Información del cliente (si no está autenticado)
        public string? ClientName { get; set; }
        public string? ClientPhone { get; set; }
        public string? ClientEmail { get; set; }
        public string? ClientNickName { get; set; }
    }
}