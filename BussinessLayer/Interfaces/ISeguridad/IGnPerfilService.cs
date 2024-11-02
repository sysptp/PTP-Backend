using BussinessLayer.DTOs.Seguridad;
using BussinessLayer.Interface.IOtros;
using BussinessLayer.Interfaces.IOtros;
using BussinessLayer.Services;
using DataLayer.Models.Entities;


namespace BussinessLayer.Interfaces.ISeguridad
{
    public interface IGnPerfilService : IGenericService<GnPerfilRequest,GnPerfilResponse,GnPerfil>
    {
    }

}
