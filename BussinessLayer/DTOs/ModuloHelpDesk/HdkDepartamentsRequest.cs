
using System.Text.Json.Serialization;

namespace BussinessLayer.DTOs.ModuloHelpDesk
{
    public class HdkDepartamentsRequest
    {
        [JsonIgnore]
        public int IdDepartamentos { get; set; }
        public string Descripcion { get; set; }
        public long IdEmpresa { get; set; }
        public bool EsPrincipal { get; set; }
    }
}
