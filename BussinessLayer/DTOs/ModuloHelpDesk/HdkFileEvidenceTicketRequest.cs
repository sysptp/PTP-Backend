
using System.Text.Json.Serialization;

namespace BussinessLayer.DTOs.ModuloHelpDesk
{
    public class HdkFileEvidenceTicketRequest
    {
        [JsonIgnore]
        public int IdFileEvidence { get; set; }
        public int IdTicket { get; set; }
        public string UrlFile { get; set; }
        public string FileName { get; set; }
        public string FileExtencion { get; set; }
        public long IdEmpresa { get; set; }
    }
}
