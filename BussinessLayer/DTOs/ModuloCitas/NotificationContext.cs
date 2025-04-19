
namespace BussinessLayer.DTOs.ModuloCitas
{
    public class NotificationContext
    {
        public string? AssignedUserName { get; set; }
        public string? ParticipantName { get; set; }
        public string? MeetingPlaceDescription { get; set; }
        public string? ReasonDescription { get; set; }
        public string? AreaDescription { get; set; }
        public string? PreviousState { get; set; }
        public string? NewState { get; set; }
        public List<string> RecipientEmails { get; set; } = new();
        public List<string> RecipientPhoneNumbers { get; set; } = new();
    }
}
