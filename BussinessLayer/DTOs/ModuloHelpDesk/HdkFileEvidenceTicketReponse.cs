using BussinessLayer.DTOs.Otros;


namespace BussinessLayer.DTOs.ModuloHelpDesk
{
    public class HdkFileEvidenceTicketReponse : AuditableEntitiesReponse
    {
        public int IdFileEvidence { get; set; }
        public int IdTicket { get; set; }
        public string UrlFile { get; set; } = null!;
        public string FileName { get; set; } = null!;
        public string FileExtencion { get; set; } = null!;
        public long IdEmpresa { get; set; }
        public string? NombreEmpresa { get; set; }
    }
}
