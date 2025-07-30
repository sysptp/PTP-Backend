
namespace BussinessLayer.DTOs.ModuloCitas
{
    public class NotificationContext
    {
        public string? AssignedUserName { get; set; }
        public string? MeetingPlaceDescription { get; set; }
        public string? ReasonDescription { get; set; }
        public string? AreaDescription { get; set; }
        public string? PreviousState { get; set; }
        public string? NewState { get; set; }
        public List<string> RecipientPhoneNumbers { get; set; } = new();
        public List<string> RecipientEmails { get; set; } = new List<string>();
        public bool IsUpdate { get; set; }
        public bool IsForAssignedUser { get; set; }
    }
}
