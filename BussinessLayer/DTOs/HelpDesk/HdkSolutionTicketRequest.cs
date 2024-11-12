
using System.Text.Json.Serialization;

namespace BussinessLayer.DTOs.HelpDesk
{
    public class HdkSolutionTicketRequest
    {
        [JsonIgnore]
        public int IdSolution { get; set; }
        public string Descripcion { get; set; }
        public long IdEmpresa { get; set; }
    }
}
