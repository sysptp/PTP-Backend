
namespace BussinessLayer.DTOs.HelpDesk
{
    public class HdkFileEvidenceTicketRequest
    {
        public int IdTicket { get; set; }
        public string UrlFile { get; set; }
        public string FileName { get; set; }
        public string FileExtencion { get; set; }
        public long IdEmpresa { get; set; }
    }
}
