

namespace BussinessLayer.DTOs.ModuloCitas.CtaEmailBackgroundJobData
{
    public class CtaEmailBackgroundJobData
    {
        public List<string>? CreatorEmails { get; set; }
        public List<string>? ContactEmails { get; set; }
        public List<string>? UserEmails { get; set; }
        public List<string>? GuestEmails { get; set; }
        public string? AssignedSubject { get; set; }
        public string? AssignedBody { get; set; }
        public string? ParticipantSubject { get; set; }
        public string? ParticipantBody { get; set; }
        public long CompanyId { get; set; }
        public bool IsUpdate { get; set; } = false;
        public bool IsStateChange { get; set; } = false;
        public string? PreviousState { get; set; }
        public string? NewState { get; set; }
    }
}
