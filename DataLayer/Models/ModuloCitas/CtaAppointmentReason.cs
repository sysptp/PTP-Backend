
using DataLayer.Models.Otros;
using System.ComponentModel.DataAnnotations;

namespace DataLayer.Models.Modulo_Citas
{
    public class CtaAppointmentReason : AuditableEntities
    {
        [Key]
        public int IdReason { get; set; }
        public string? Description { get; set; }
        public DateTime CreationDate { get; set; } = DateTime.Now;
        public long CompanyId { get; set; }
        public bool IsDefault { get; set; } = false;
    }
}
