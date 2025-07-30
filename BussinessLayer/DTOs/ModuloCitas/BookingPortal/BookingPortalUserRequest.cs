using System.Text.Json.Serialization;

namespace BussinessLayer.DTOs.ModuloCitas.BookingPortal
{
    public class BookingPortalUserRequest
    {
        [JsonIgnore]
        public int Id { get; set; }
        public int PortalId { get; set; }
        public int UserId { get; set; }
        public bool IsMainAssignee { get; set; } = false;
    }
}