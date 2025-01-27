using DataLayer.Models.ModuloGeneral.Empresa;
using DataLayer.Models.Otros;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace DataLayer.Models.ModuloHelpDesk
{
    public class HdkFileEvidenceTicket : AuditableEntities
    {
        [Key]
        public int IdFileEvidence { get; set; }
        public int IdTicket { get; set; }
        public string UrlFile { get; set; } = null!;
        public string FileName { get; set; } = null!;
        public string FileExtencion { get; set; } = null!;
        public long IdEmpresa { get; set; }
        [ForeignKey("IdEmpresa")]
        public GnEmpresa? GnEmpresa { get; set; }

    }
}
