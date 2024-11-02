using DataLayer.Models;
using DataLayer.Models.Empresa;
using DataLayer.Models.Seguridad;

namespace BussinessLayer.DTOs.Autenticacion
{
    public class UserSectionDto
    {
        public SC_USUAR001 DatosUsuario { get; set; }

        public GnEmpresa DatosEmpresa { get; set; }

        public GnSucursal DatosSucursal { get; set; }

        public string Ip { get; set; }
    }
}