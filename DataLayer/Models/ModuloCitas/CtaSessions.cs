
using DataLayer.Models.Otros;
using System.ComponentModel.DataAnnotations;

namespace DataLayer.Models.Modulo_Citas
{
    public class CtaSessions : AuditableEntities
    {
        [Key]
        public int IdSession { get; set; }
        public string? Description { get; set; }
        public string? IdClient { get; set; }
        public string? IdUser { get; set; }
        public DateTime FirstSessionDate { get; set; }
        public int? IdReason { get; set; }
        public int IdState { get; set; }
        public bool IsActive { get; set; } = true;
        
    }
}
