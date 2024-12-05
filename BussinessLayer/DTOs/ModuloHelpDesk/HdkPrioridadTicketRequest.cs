
using System.Text.Json.Serialization;

namespace BussinessLayer.DTOs.ModuloHelpDesk
{
    public class HdkPrioridadTicketRequest
    {
        [JsonIgnore]
        public int IdPrioridad { get; set; }
        public string Descripcion { get; set; }
        public long IdEmpresa { get; set; }
    }
}
