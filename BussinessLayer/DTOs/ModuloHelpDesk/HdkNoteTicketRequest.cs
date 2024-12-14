
using System.Text.Json.Serialization;

namespace BussinessLayer.DTOs.ModuloHelpDesk
{
    public class HdkNoteTicketRequest
    {
        [JsonIgnore]
        public int IdNota { get; set; }
        public string Notas { get; set; }
        public int IdTicket { get; set; }
        public long IdEmpresa { get; set; }
    }
}
