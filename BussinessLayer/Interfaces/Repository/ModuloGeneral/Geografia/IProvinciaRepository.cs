using BussinessLayer.Interfaces.Repositories;
using DataLayer.Models.ModuloGeneral.Geografia;

namespace BussinessLayer.Interfaces.Repository.ModuloGeneral.Geografia
{
    public interface IProvinciaRepository : IGenericRepository<Provincia>
    {
        Task<IEnumerable<Provincia>> GetProvinciasByRegionId(int regionId);
        Task<List<Provincia>> GetAllIndex();
    }
}
