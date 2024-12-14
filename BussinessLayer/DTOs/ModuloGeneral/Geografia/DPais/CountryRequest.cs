using System.Text.Json.Serialization;

<<<<<<<< HEAD:BussinessLayer/DTOs/ModuloGeneral/Configuracion/Geografia/DPais/CountryRequest.cs
namespace BussinessLayer.DTOs.ModuloGeneral.Configuracion.Geografia.DPais
========
namespace BussinessLayer.DTOs.ModuloGeneral.Geografia.DPais
>>>>>>>> REFACTOR:BussinessLayer/DTOs/ModuloGeneral/Geografia/DPais/CountryRequest.cs
{
    public class CountryRequest
    {
        [JsonIgnore]
        public int Id { get; set; }
        public string Name { get; set; } = null!;
    }
}
