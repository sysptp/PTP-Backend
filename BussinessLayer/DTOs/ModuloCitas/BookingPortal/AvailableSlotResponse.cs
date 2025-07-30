
namespace BussinessLayer.DTOs.ModuloCitas.BookingPortal
{
    public class AvailableSlotResponse
    {
        public DateTime Date { get; set; }
        public List<TimeSlot> AvailableSlots { get; set; } = new();
    }

    public class TimeSlot
    {
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public bool IsAvailable { get; set; }
        public string? ReasonUnavailable { get; set; }
    }
}