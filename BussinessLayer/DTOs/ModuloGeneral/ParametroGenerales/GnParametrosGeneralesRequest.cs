using System.Text.Json.Serialization;

namespace BussinessLayer.DTOs.ModuloGeneral.ParametroGenerales
{
    public class GnParametrosGeneralesRequest
    {
        [JsonIgnore]
        public long IdParametro { get; set; }
        public string VariableParametro { get; set; }
        public string Descripcion { get; set; }
        public string Valor { get; set; }
        public int IdModulo { get; set; }
        public long IdEmpresa { get; set; }
    }
}
