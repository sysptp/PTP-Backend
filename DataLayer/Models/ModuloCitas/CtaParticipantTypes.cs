
using System.ComponentModel.DataAnnotations;
using DataLayer.Models.Otros;

namespace DataLayer.Models.ModuloCitas
{
    public class CtaParticipantTypes : AuditableEntities
    {
        [Key]
        public int ParticipantTypeId { get; set; }
        public string ParticipantType { get; set; } = null!;
        public string? Description { get; set; }
    }
}
