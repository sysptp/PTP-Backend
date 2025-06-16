using DataLayer.Models.ModuloGeneral;
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
        public string Description { get; set; } = null!;
        public string? Title { get; set; }
        public int IdUser { get; set; }
        [ForeignKey("IdUser")]
        public Usuario? Usuario { get; set; }
        public DateTime FirstSessionDate { get; set; }
        public int RepeatEvery { get; set; }
        public int RepeatUnitId { get; set; }
        [ForeignKey("RepeatUnitId")]
        public GnRepeatUnit? GnRepeatUnit { get; set; }
        public DateTime? LastSessionDate { get; set; }
        public DateTime SessionEndDate { get; set; }
        public int CompletedAppointments { get; set; }
        public long CompanyId { get; set; }
    }
}
