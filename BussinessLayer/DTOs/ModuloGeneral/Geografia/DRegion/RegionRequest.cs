using System.Text.Json.Serialization;

<<<<<<<< HEAD:BussinessLayer/DTOs/ModuloGeneral/Configuracion/Geografia/DRegion/RegionRequest.cs
namespace BussinessLayer.DTOs.ModuloGeneral.Configuracion.Geografia.DRegion
========
namespace BussinessLayer.DTOs.ModuloGeneral.Geografia.DRegion
>>>>>>>> REFACTOR:BussinessLayer/DTOs/ModuloGeneral/Geografia/DRegion/RegionRequest.cs
{
    public class RegionRequest
    {
        [JsonIgnore]
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int CountryId { get; set; }
    }
}
