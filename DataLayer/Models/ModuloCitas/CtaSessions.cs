
using DataLayer.Models.ModuloGeneral.Seguridad;
using DataLayer.Models.Otros;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Models.Modulo_Citas
{
    public class CtaSessions : AuditableEntities
    {
        [Key]
        public int IdSession { get; set; }
        public string? Description { get; set; }
        public string? IdClient { get; set; }
        public string? IdUser { get; set; }
        [ForeignKey("IdUser")]
        public Usuario? Usuario { get; set; }
        public DateTime FirstSessionDate { get; set; }
        public int? IdReason { get; set; }
        [ForeignKey("IdReason")]
        public CtaAppointmentReason? AppointmentReason { get; set; }
        public int IdState { get; set; }
        [ForeignKey("IdState")]
        public CtaState? State { get; set; }
        public bool IsActive { get; set; } = true;
        
    }
}
