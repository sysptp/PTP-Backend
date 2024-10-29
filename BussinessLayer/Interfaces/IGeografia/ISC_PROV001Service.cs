using DataLayer.Models.Geografia;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BussinessLayer.Interfaces.IGeografia
{
    public interface ISC_PROV001Service
    {
        Task Add(SC_PROV001 model);

        Task Delete(SC_PROV001 model);

        Task<List<SC_PROV001>> GetAll();

        Task<List<SC_PROV001>> GetAllIndex();

        Task<SC_PROV001> GetById(int id);

        Task Update(SC_PROV001 model);
    }
}
