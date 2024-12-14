using System.Text.Json.Serialization;

<<<<<<<< HEAD:BussinessLayer/DTOs/ModuloGeneral/Configuracion/Seguridad/Permiso/GnPermisoRequest.cs
namespace BussinessLayer.DTOs.ModuloGeneral.Configuracion.Seguridad.Permiso
========
namespace BussinessLayer.DTOs.ModuloGeneral.Seguridad.Permiso
>>>>>>>> REFACTOR:BussinessLayer/DTOs/ModuloGeneral/Seguridad/Permiso/GnPermisoRequest.cs
{
    public class GnPermisoRequest
    {

        public int RoleId { get; set; }
        public int MenuId { get; set; }
        public long CompanyId { get; set; }
        public bool Create { get; set; }
        public bool Delete { get; set; }
        public bool Edit { get; set; }
        public bool Query { get; set; }
        [JsonIgnore]
        public long IDPermiso { get; set; }
    }
}
