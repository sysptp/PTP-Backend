using DataLayer.Models.ModuloGeneral.Empresa;
using DataLayer.Models.Otros;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Models.ModuloCitas
{
    public class CtaSmsTemplates : AuditableEntities
    {
        [Key]
        public long Id { get; set; }
        public long CompanyId { get; set; }
        public string? MessageTitle { get; set; }
        public string MessageContent { get; set; } = null!;

        [ForeignKey("CompanyId")]
        public virtual GnEmpresa? Company { get; set; }
    }
}
