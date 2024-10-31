using BussinessLayer.DTOs.Seguridad;
using BussinessLayer.Interfaces.Repositories;
using DataLayer.Models.Entities;

namespace BussinessLayer.Interfaces.Repository.Seguridad
{
    public interface IGnPerfilRepository : IGenericRepository<GnPerfil>
    {
        Task PatchUpdate(int id, Dictionary<string, object> updatedProperties);
        Task<IList<GnPerfilDto>> GetAll(int? idPerfil = null, long? idEmpresa = null);
    }
}
