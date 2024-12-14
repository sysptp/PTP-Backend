using System.Text.Json.Serialization;

<<<<<<<< HEAD:BussinessLayer/DTOs/ModuloGeneral/Configuracion/Geografia/DProvincia/ProvinceRequest.cs
namespace BussinessLayer.DTOs.ModuloGeneral.Configuracion.Geografia.DProvincia
========
namespace BussinessLayer.DTOs.ModuloGeneral.Geografia.DProvincia
>>>>>>>> REFACTOR:BussinessLayer/DTOs/ModuloGeneral/Geografia/DProvincia/ProvinceRequest.cs
{
    public class ProvinceRequest
    {
        [JsonIgnore]
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int RegionId { get; set; }
    }
}
