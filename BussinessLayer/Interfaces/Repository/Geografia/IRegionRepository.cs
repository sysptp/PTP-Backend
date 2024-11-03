using BussinessLayer.Interfaces.Repositories;
using DataLayer.Models.Geografia;

namespace BussinessLayer.Interfaces.Repository.Geografia
{
    public interface IRegionRepository : IGenericRepository<Region>
    {
        Task<List<Region>> GetAllIndex();
    }
}
