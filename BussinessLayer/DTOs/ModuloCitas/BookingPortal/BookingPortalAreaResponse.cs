namespace BussinessLayer.DTOs.ModuloCitas.BookingPortal
{
    public class BookingPortalAreaResponse
    {
        public int Id { get; set; }
        public int PortalId { get; set; }
        public int AreaId { get; set; }
        public string? AreaName { get; set; }
        public string? AreaDescription { get; set; }
        public bool IsDefault { get; set; }
    }
}