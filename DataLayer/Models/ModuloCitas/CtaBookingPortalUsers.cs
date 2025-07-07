using DataLayer.Models.ModuloGeneral.Seguridad;
using DataLayer.Models.Otros;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Models.ModuloCitas
{
    public class CtaBookingPortalUsers : AuditableEntities
    {
        [Key]
        public int Id { get; set; }
        public int PortalId { get; set; }
        public int UserId { get; set; }
        public bool IsMainAssignee { get; set; } = false;

        [ForeignKey("PortalId")]
        public virtual CtaBookingPortalConfig? Portal { get; set; }

        [ForeignKey("UserId")]
        public virtual Usuario? User { get; set; }
    }
}