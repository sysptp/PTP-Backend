using DataLayer.Models.Modulo_Citas;
using DataLayer.Models.ModuloGeneral.Seguridad;
using DataLayer.Models.Otros;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Models.ModuloCitas
{
    public class CtaAppointmentUsers : AuditableEntities
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public Usuario? Usuario { get; set; }
        public int AppointmentId { get; set; }
        [ForeignKey("AppointmentId")]
        public CtaAppointments? Appointments { get; set; }
        public long CompanyId { get; set; }

    }
}
