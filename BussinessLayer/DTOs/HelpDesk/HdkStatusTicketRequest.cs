
using System.Text.Json.Serialization;

namespace BussinessLayer.DTOs.HelpDesk
{
    public class HdkStatusTicketRequest
    {
        [JsonIgnore]
        public int IdEstado { get; set; }
        public string Descripcion { get; set; }
        public bool EsCierre { get; set; }
        public long IdEmpresa { get; set; }
        public int IdDepartamento { get; set; }
        public int OrdenStatus { get; set; }
    }
}
