
using System.Text.Json.Serialization;

<<<<<<<< HEAD:BussinessLayer/DTOs/ModuloGeneral/Configuracion/Menu/SaveGnMenuRequest.cs
namespace BussinessLayer.DTOs.ModuloGeneral.Configuracion.Menu
========
namespace BussinessLayer.DTOs.ModuloGeneral.Menu
>>>>>>>> REFACTOR:BussinessLayer/DTOs/ModuloGeneral/Menu/SaveGnMenuRequest.cs
{
    public class SaveGnMenuRequest
    {
        public string Name { get; set; } = null!;
        public int Level { get; set; }
        public int Order { get; set; }
        public string? Url { get; set; }
        public string? Icon { get; set; }
        public int ModuleID { get; set; }
        public int ParentMenuId { get; set; }
        [JsonIgnore]
        public int IDMenu { get; set; }
    }
}
