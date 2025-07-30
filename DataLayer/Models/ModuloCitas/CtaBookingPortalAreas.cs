using DataLayer.Models.Otros;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Models.ModuloCitas
{
    public class CtaBookingPortalAreas : AuditableEntities
    {
        [Key]
        public int Id { get; set; }
        public int PortalId { get; set; }
        public int AreaId { get; set; }
        public bool IsDefault { get; set; } = false; // Área por defecto del portal

        // Navigation Properties
        [ForeignKey("PortalId")]
        public virtual CtaBookingPortalConfig? Portal { get; set; }

        [ForeignKey("AreaId")]
        public virtual CtaAppointmentArea? Area { get; set; }
    }
}