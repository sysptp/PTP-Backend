using BussinessLayer.Interfaces.Repositories;
using DataLayer.Models.Geografia;

namespace BussinessLayer.Interfaces.Repository.Geografia
{
    public interface IProvinciaRepository : IGenericRepository<Provincia>
    {
        Task<IEnumerable<Provincia>> GetProvinciasByRegionId(int regionId);
        Task<List<Provincia>> GetAllIndex();
    }
}
