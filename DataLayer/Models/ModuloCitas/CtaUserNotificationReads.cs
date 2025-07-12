using DataLayer.Models.ModuloGeneral.Seguridad;
using DataLayer.Models.Otros;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Models.ModuloCitas
{
    public class CtaUserNotificationReads : AuditableEntities
    {
        [Key]
        public int NotificationId { get; set; }
        public int UserId { get; set; }
        public string? Type { get; set; }

        public bool IsRead { get; set; } = false;

        public DateTime? ReadDate { get; set; }

        public string Title { get; set; } = null!;

        public string? Message { get; set; }

        public string? Data { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

        public int? AppointmentId { get; set; }

        public long CompanyId { get; set; }

        [ForeignKey("UserId")]
        public virtual Usuario? Usuario { get; set; }
    }
}
