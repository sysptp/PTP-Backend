
using System.Text.Json.Serialization;

<<<<<<<< HEAD:BussinessLayer/DTOs/ModuloGeneral/Configuracion/Geografia/DMunicipio/MunicipioRequest.cs
namespace BussinessLayer.DTOs.ModuloGeneral.Configuracion.Geografia.DMunicipio
========
namespace BussinessLayer.DTOs.ModuloGeneral.Geografia.DMunicipio
>>>>>>>> REFACTOR:BussinessLayer/DTOs/ModuloGeneral/Geografia/DMunicipio/MunicipioRequest.cs
{
    public class MunicipioRequest
    {
        [JsonIgnore]
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int ProvinceId { get; set; }
    }
}
