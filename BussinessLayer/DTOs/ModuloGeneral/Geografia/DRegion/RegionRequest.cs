using System.Text.Json.Serialization;

namespace BussinessLayer.DTOs.ModuloGeneral.Geografia.DRegion
{
    public class RegionRequest
    {
        [JsonIgnore]
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int CountryId { get; set; }
    }
}
