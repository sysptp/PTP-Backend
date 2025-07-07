using System.Text.Json.Serialization;

namespace BussinessLayer.DTOs.ModuloCitas.BookingPortal
{
    public class BookingPortalAreaRequest
    {
        [JsonIgnore]
        public int Id { get; set; }
        public int PortalId { get; set; }
        public int AreaId { get; set; }
        public bool IsDefault { get; set; } = false;
    }
}