using DataLayer.Models.Modulo_Citas;
using DataLayer.Models.ModuloGeneral.Empresa;
using DataLayer.Models.Otros;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Models.ModuloCitas
{
    public class CtaAppointmentContacts : AuditableEntities
    {
        [Key]
        public int Id { get; set; }
        public int ContactId { get; set; }
        [ForeignKey("ContactId")]
        public CtaContacts? Contact { get; set; }
        public int AppointmentId { get; set; }
        [ForeignKey("AppointmentId")]
        public CtaAppointments? Appointment { get; set; }
        public long CompanyId { get; set; }

    }
}
