using DataLayer.Models.Modulo_Citas;
using DataLayer.Models.ModuloGeneral.Empresa;
using DataLayer.Models.Otros;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Models.ModuloCitas
{
    public class CtaAppointmentGuest : AuditableEntities
    {
        public int Id { get; set; }
        public int GuestId { get; set; }
        [ForeignKey("GuestId")]
        public CtaGuest? Guest { get; set; }
        public int AppointmentId { get; set; }
        [ForeignKey("AppointmentId")]
        public CtaAppointments? Appointments { get; set; }
        public long CompanyId { get; set; }
        [ForeignKey("CompanyId")]
        public GnEmpresa? GnEmpresa { get; set; }
    }
}
