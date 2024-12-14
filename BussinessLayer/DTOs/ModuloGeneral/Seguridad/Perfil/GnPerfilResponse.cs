using System;

<<<<<<<< HEAD:BussinessLayer/DTOs/ModuloGeneral/Configuracion/Seguridad/GnPerfilResponse.cs
namespace BussinessLayer.DTOs.ModuloGeneral.Configuracion.Seguridad
========
namespace BussinessLayer.DTOs.ModuloGeneral.Seguridad.Perfil
>>>>>>>> REFACTOR:BussinessLayer/DTOs/ModuloGeneral/Seguridad/Perfil/GnPerfilResponse.cs
{
    public class GnPerfilResponse
    {
        public int IdRole { get; set; }
        public string Name { get; set; } = null!;
        public string? Descripcion { get; set; }
        public long CompanyId { get; set; }
    }
}
