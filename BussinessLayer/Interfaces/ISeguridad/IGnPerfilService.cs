using BussinessLayer.DTOs.Seguridad;
using BussinessLayer.Interface.IOtros;
using BussinessLayer.Interfaces.IOtros;
using BussinessLayer.Services;
using DataLayer.Models.Entities;


namespace BussinessLayer.Interfaces.ISeguridad
{
    public interface IGnPerfilService : IGenericService<GnPerfilRequest,GnPerfilResponse,GnPerfil>
    {
        //Task PatchUpdate(int id, Dictionary<string, object> updatedProperties);
        //Task<IList<GnPerfilDto>> GetAll(int? idPerfil = null, long? idEmpresa = null);
        Task<GnPerfilRequest> AddTest(GnPerfilRequest vm);
    }

}
