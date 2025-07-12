using System.Text.Json.Serialization;

namespace BussinessLayer.DTOs.ModuloCitas.BookingPortal
{
    public class BookingPortalConfigRequest
    {
        [JsonIgnore]
        public int Id { get; set; }
        public long CompanyId { get; set; }
        public string PortalName { get; set; } = null!;
        public string? Description { get; set; }
        public bool RequireAuthentication { get; set; } = true;
        public bool IsActive { get; set; } = true;
        public string? CustomSlug { get; set; }
        public int? DefaultReasonId { get; set; }
        public int? DefaultPlaceId { get; set; }
        public int? DefaultStateId { get; set; }
        public TimeSpan? DefaultAppointmentDuration { get; set; }
        public List<int>? AvailableDays { get; set; }
        public TimeSpan? StartTime { get; set; }
        public TimeSpan? EndTime { get; set; }
        public int? MaxAdvanceDays { get; set; } = 30;

        // Relaciones Many-to-Many
        public List<int> AssignedUserIds { get; set; } = new();
        public List<int> AreaIds { get; set; } = new();
        public int? MainAssigneeUserId { get; set; } // Usuario principal
        public int? DefaultAreaId { get; set; } // Área por defecto
    }
}
