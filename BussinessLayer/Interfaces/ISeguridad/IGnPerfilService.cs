using BussinessLayer.DTOs.Seguridad;
using BussinessLayer.Interface.IOtros;
using DataLayer.Models.Entities;


namespace BussinessLayer.Interfaces.ISeguridad
{
    public interface IGnPerfilService : IGenericService<GnPerfil>
    {
        Task PatchUpdate(int id, Dictionary<string, object> updatedProperties);
        Task<IList<GnPerfilDto>> GetAll(int? idPerfil = null, long? idEmpresa = null);
    }

}
