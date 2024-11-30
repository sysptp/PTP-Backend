
using DataLayer.Models.Otros;

namespace DataLayer.Models.Seguridad
{
    public class GnScheduleUser : AuditableEntities
    {
        public int Id { get; set; }
        public long CompanyId { get; set; }
        public int UserId { get; set; }
        public int ScheduleId { get; set; }
    }
}
