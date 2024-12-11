using System.Text.Json.Serialization;

namespace BussinessLayer.DTOs.ModuloGeneral.Configuracion.Geografia.DPais
{
    public class CountryRequest
    {
        [JsonIgnore]
        public int Id { get; set; }
        public string Name { get; set; } = null!;
    }
}
