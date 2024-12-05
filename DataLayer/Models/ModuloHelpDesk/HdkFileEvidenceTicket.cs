using DataLayer.Models.Otros;
using System.ComponentModel.DataAnnotations;


namespace DataLayer.Models.ModuloHelpDesk
{
    public class HdkFileEvidenceTicket : AuditableEntities
    {
        [Key]
        public int IdFileEvidence { get; set; }
        public int IdTicket { get; set; }
        public string UrlFile { get; set; }
        public string FileName { get; set; }
        public string FileExtencion { get; set; }
        public long IdEmpresa { get; set; }

    }
}
