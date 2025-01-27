using DataLayer.Models.Otros;

namespace DataLayer.Models.ModuloCitas
{
    public class CtaAppointmentSequence : AuditableEntities
    {
        public int Id { get; set; }
        public string? SequenceIdentifier { get; set; }
        public long CompanyId { get; set; }
        public long SequenceNumber { get; set; }
        public string? Prefix { get; set; }
        public string? Suffix { get; set; }
        public DateTime LastUsed { get; set; }
        public int IncrementBy { get; set; }
        public bool IsActive { get; set; }
        public long MaxValue { get; set; }
        public long MinValue { get; set; }
        public int? AreaId { get; set; }
    }
}
