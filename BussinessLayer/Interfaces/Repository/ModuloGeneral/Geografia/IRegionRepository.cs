using BussinessLayer.Interfaces.Repositories;
using DataLayer.Models.ModuloGeneral.Geografia;

namespace BussinessLayer.Interfaces.Repository.ModuloGeneral.Geografia
{
    public interface IRegionRepository : IGenericRepository<Region>
    {
        Task<List<Region>> GetAllIndex();
    }
}
