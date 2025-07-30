namespace BussinessLayer.DTOs.ModuloCitas.BookingPortal
{
    public class BookingPortalConfigResponse
    {
        public int Id { get; set; }
        public long CompanyId { get; set; }
        public string? CompanyName { get; set; }
        public string PortalName { get; set; } = null!;
        public string? Description { get; set; }
        public bool RequireAuthentication { get; set; }
        public bool IsActive { get; set; }
        public string? CustomSlug { get; set; }
        public int? DefaultReasonId { get; set; }
        public string? DefaultReasonName { get; set; }
        public int? DefaultPlaceId { get; set; }
        public string? DefaultPlaceName { get; set; }
        public int? DefaultStateId { get; set; }
        public string? DefaultStateName { get; set; }
        public TimeSpan? DefaultAppointmentDuration { get; set; }
        public List<int>? AvailableDays { get; set; }
        public TimeSpan? StartTime { get; set; }
        public TimeSpan? EndTime { get; set; }
        public int? MaxAdvanceDays { get; set; }

        // Relaciones Many-to-Many
        public List<BookingPortalUserResponse> AssignedUsers { get; set; } = new();
        public List<BookingPortalAreaResponse> Areas { get; set; } = new();
    }
}