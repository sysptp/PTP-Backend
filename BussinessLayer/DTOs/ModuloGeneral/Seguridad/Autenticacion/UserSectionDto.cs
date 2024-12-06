using DataLayer.Models;
using DataLayer.Models.ModuloGeneral.Empresa;
using DataLayer.Models.ModuloGeneral.Seguridad;

namespace BussinessLayer.DTOs.ModuloGeneral.Seguridad.Autenticacion
{
    public class UserSectionDto
    {
        public SC_USUAR001 DatosUsuario { get; set; }

        public GnEmpresa DatosEmpresa { get; set; }

        public GnSucursal DatosSucursal { get; set; }

        public string Ip { get; set; }
    }
}