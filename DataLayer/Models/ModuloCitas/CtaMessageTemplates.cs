using DataLayer.Models.ModuloGeneral.Utils;
using DataLayer.Models.Otros;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Models.ModuloCitas
{
    public class CtaMessageTemplates : AuditableEntities
    {
        [Key]
        public long Id { get; set; }
        public long? CompanyId { get; set; }
        public int MessageTypeId { get; set; }
        [ForeignKey("MessageTypeId")]
        public virtual GnMessageType? GnMessageType { get; set; }
        public string? MessageTitle { get; set; }
        public string MessageContent { get; set; } = null!;
        public bool IsInteractive { get; set; }
        public string? ButtonConfig { get; set; }
    }
}
