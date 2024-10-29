using DataLayer.Models.Geografia;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BussinessLayer.Interfaces.IGeografia
{
    public interface IRegionService
    {
        Task Add(Region model);

        Task Delete(Region model);

        Task<List<Region>> GetAll();

        Task<List<Region>> GetAllIndex();

        Task<IList<Region>> GetAllByEmp(long idEMpresa);

        Task<Region> GetById(int id);

        Task Update(Region model);

    }
}