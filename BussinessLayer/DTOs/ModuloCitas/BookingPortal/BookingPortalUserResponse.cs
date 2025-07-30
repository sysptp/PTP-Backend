namespace BussinessLayer.DTOs.ModuloCitas.BookingPortal
{
    public class BookingPortalUserResponse
    {
        public int Id { get; set; }
        public int PortalId { get; set; }
        public int UserId { get; set; }
        public string? UserName { get; set; }
        public string? UserEmail { get; set; }
        public bool IsMainAssignee { get; set; }
    }
}