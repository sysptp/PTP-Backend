using BussinessLayer.DTOs.ModuloGeneral.Seguridad.Perfil;
using BussinessLayer.Interface.IOtros;
using BussinessLayer.Interfaces.IOtros;
using BussinessLayer.Services;
using DataLayer.Models.ModuloGeneral.Seguridad;


namespace BussinessLayer.Interfaces.ModuloGeneral.Seguridad
{
    public interface IGnPerfilService : IGenericService<GnPerfilRequest, GnPerfilResponse, GnPerfil>
    {
    }

}
