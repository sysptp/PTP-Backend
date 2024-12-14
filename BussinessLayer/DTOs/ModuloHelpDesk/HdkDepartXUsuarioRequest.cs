
using System.Text.Json.Serialization;

namespace BussinessLayer.DTOs.ModuloHelpDesk
{
    public class HdkDepartXUsuarioRequest
    {
        [JsonIgnore]
        public int IdDepartXUsuario { get; set; }
        public string IdUsuarioDepto { get; set; }
        public int IdDepartamento { get; set; }
        public long IdEmpresa { get; set; }
    }
}
