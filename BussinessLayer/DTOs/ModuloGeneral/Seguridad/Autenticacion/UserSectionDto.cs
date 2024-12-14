using DataLayer.Models;
using DataLayer.Models.ModuloGeneral.Empresa;
using DataLayer.Models.ModuloGeneral.Seguridad;

<<<<<<<< HEAD:BussinessLayer/DTOs/ModuloGeneral/Configuracion/Seguridad/Autenticacion/UserSectionDto.cs
namespace BussinessLayer.DTOs.ModuloGeneral.Configuracion.Seguridad.Autenticacion
========
namespace BussinessLayer.DTOs.ModuloGeneral.Seguridad.Autenticacion
>>>>>>>> REFACTOR:BussinessLayer/DTOs/ModuloGeneral/Seguridad/Autenticacion/UserSectionDto.cs
{
    public class UserSectionDto
    {
        public SC_USUAR001 DatosUsuario { get; set; }

        public GnEmpresa DatosEmpresa { get; set; }

        public GnSucursal DatosSucursal { get; set; }

        public string Ip { get; set; }
    }
}