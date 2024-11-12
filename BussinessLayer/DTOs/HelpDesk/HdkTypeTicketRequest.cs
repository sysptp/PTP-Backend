
using System.Text.Json.Serialization;

namespace BussinessLayer.DTOs.HelpDesk
{
    public class HdkTypeTicketRequest
    {
        [JsonIgnore]
        public int IdTipoTicket { get; set; }
        public string Descripcion { get; set; }
        public long IdEmpresa { get; set; }
    }
}
