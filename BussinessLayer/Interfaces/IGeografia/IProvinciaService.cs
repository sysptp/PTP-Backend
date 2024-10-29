using System.Collections.Generic;
using System.Threading.Tasks;
using DataLayer.Models;
using DataLayer.Models.Geografia;

namespace BussinessLayer.Interfaces.IGeografia
{
    public interface IProvinciaService
    {
        Task<IEnumerable<Provincia>> GetProvinciasByRegionId(int regionId);

        Task<IList<Provincia>> GetAllByEmp(long idEMpresa);

        Task Add(Provincia model);

        Task<List<Provincia>> GetAllIndex();

        Task Delete(Provincia model);

        Task<List<Provincia>> GetAll();

        Task<Provincia> GetById(int id);

        Task Update(Provincia model);
    }
}