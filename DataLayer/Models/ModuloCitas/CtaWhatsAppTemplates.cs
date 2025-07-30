using DataLayer.Models.ModuloGeneral.Empresa;
using DataLayer.Models.Otros;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Models.ModuloCitas
{
    public class CtaWhatsAppTemplates : AuditableEntities
    {
        [Key]
        public long Id { get; set; }
        public long CompanyId { get; set; }
        public string? MessageTitle { get; set; }
        public string MessageContent { get; set; } = null!;
        public bool IsInteractive { get; set; }
        public string? ButtonConfig { get; set; } 
        public int? HeaderType { get; set; }
        public string? HeaderContent { get; set; }
        public string? FooterText { get; set; }

        [ForeignKey("CompanyId")]
        public virtual GnEmpresa? Company { get; set; }
    }
}
