using DataLayer.Models.Otros;

namespace DataLayer.Models.ModuloGeneral.Seguridad
{
    public class GnSchedule : AuditableEntities
    {
        public int Id { get; set; }
        public long CompanyId { get; set; }
        public decimal StartHour { get; set; }
        public decimal EndHour { get; set; }
        public string StartDay { get; set; } = null!;
        public string EndDay { get; set; } = null!;
    }
}
