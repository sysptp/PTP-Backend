
using System.Text.Json.Serialization;

namespace BussinessLayer.DTOs.ModuloHelpDesk
{
    public class HdkSolutionTicketRequest
    {
        [JsonIgnore]
        public int IdSolution { get; set; }
        public string Descripcion { get; set; }
        public long IdEmpresa { get; set; }
        public int IdDepartamento { get; set; }
        public int OrdenStatus { get; set; }
    }
}
