using System.Text.Json.Serialization;

namespace BussinessLayer.DTOs.ModuloGeneral.Configuracion.Geografia.DProvincia
{
    public class ProvinceRequest
    {
        [JsonIgnore]
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int RegionId { get; set; }
    }
}
