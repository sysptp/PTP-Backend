using BussinessLayer.DTOs.ModuloGeneral.Seguridad.Perfil;
using BussinessLayer.Interfaces.Services.IOtros;
using DataLayer.Models.ModuloGeneral.Seguridad;


namespace BussinessLayer.Interfaces.Services.ModuloGeneral.Seguridad
{
    public interface IGnPerfilService : IGenericService<GnPerfilRequest, GnPerfilResponse, GnPerfil>
    {
    }

}
