using DataLayer.Models.ModuloGeneral.Empresa;
using DataLayer.Models.Otros;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Models.ModuloGeneral.Seguridad
{
    public class GnScheduleUser : AuditableEntities
    {
        public int Id { get; set; }
        public long CompanyId { get; set; }
        public int UserId { get; set; }
        public int ScheduleId { get; set; }
        [ForeignKey("CompanyId")]
        public GnEmpresa? GnEmpresa { get; set; }
        [ForeignKey("UserId")]
        public Usuario? Usuario { get; set; }
        [ForeignKey("ScheduleId")]
        public GnSchedule? GnSchedule { get; set; }

    }
}
