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
        public int? IdClient { get; set; }
        public int? IdUser { get; set; }
        [ForeignKey("IdUser")]
        public Usuario? Usuario { get; set; }
        public DateTime FirstSessionDate { get; set; }
        public int? IdReason { get; set; }
        [ForeignKey("IdReason")]
        public CtaAppointmentReason? AppointmentReason { get; set; }
        public int TotalAppointments { get; set; }
        public DateTime? LastSessionDate { get; set; }
        public DateTime SessionEndDate { get; set; }
        public int CompletedAppointments { get; set; }
        public int FrequencyInDays { get; set; }
        public long CompanyId { get; set; }
    }
}
