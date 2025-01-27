using DataLayer.Models.Otros;
using System.ComponentModel.DataAnnotations;

namespace DataLayer.Models.ModuloCitas
{
    public class CtaAppointmentArea : AuditableEntities
    {
        [Key]
        public int AreaId { get; set; }
        public string Description { get; set; } = null!;
        public long CompanyId { get; set; }
        public bool IsPrincipal { get; set; }

    }
}
