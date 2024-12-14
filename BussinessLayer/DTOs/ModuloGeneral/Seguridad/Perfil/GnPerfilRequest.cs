
using System.Text.Json.Serialization;

<<<<<<<< HEAD:BussinessLayer/DTOs/ModuloGeneral/Configuracion/Seguridad/GnPerfilRequest.cs
namespace BussinessLayer.DTOs.ModuloGeneral.Configuracion.Seguridad
========
namespace BussinessLayer.DTOs.ModuloGeneral.Seguridad.Perfil
>>>>>>>> REFACTOR:BussinessLayer/DTOs/ModuloGeneral/Seguridad/Perfil/GnPerfilRequest.cs
{
    public class GnPerfilRequest
    {
        public string Name { get; set; } = null!;
        public string? Descripcion { get; set; }
        public long CompanyId { get; set; }
        [JsonIgnore]
        public int Id { get; set; }
    }
}
