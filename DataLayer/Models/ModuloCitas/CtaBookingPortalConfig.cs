using DataLayer.Models.Modulo_Citas;
using DataLayer.Models.ModuloGeneral.Empresa;
using DataLayer.Models.Otros;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DataLayer.Models.ModuloCitas
{
    public class CtaBookingPortalConfig : AuditableEntities
    {
        [Key]
        public int Id { get; set; }
        public long CompanyId { get; set; }
        public string PortalName { get; set; } = null!;
        public string? Description { get; set; }
        public bool RequireAuthentication { get; set; } = true;
        public bool IsActive { get; set; } = true;
        public string? CustomSlug { get; set; }
        public int? DefaultReasonId { get; set; }
        public int? DefaultPlaceId { get; set; }
        public int? DefaultStateId { get; set; }
        public TimeSpan? DefaultAppointmentDuration { get; set; }
        public string? AvailableDaysJson { get; set; } // JSON array de días disponibles
        public TimeSpan? StartTime { get; set; }
        public TimeSpan? EndTime { get; set; }
        public int? MaxAdvanceDays { get; set; } = 30;

        // Navigation Properties (Many-to-One)
        [ForeignKey("CompanyId")]
        public virtual GnEmpresa? Company { get; set; }

        [ForeignKey("DefaultReasonId")]
        public virtual CtaAppointmentReason? DefaultReason { get; set; }

        [ForeignKey("DefaultPlaceId")]
        public virtual CtaMeetingPlace? DefaultPlace { get; set; }

        [ForeignKey("DefaultStateId")]
        public virtual CtaState? DefaultState { get; set; }

        // Navigation Properties (Many-to-Many)
        public virtual ICollection<CtaBookingPortalUsers> PortalUsers { get; set; } = new List<CtaBookingPortalUsers>();
        public virtual ICollection<CtaBookingPortalAreas> PortalAreas { get; set; } = new List<CtaBookingPortalAreas>();
    }
}