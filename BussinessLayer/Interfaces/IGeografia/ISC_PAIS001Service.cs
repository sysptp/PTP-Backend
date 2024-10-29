using DataLayer.Models.Geografia;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BussinessLayer.Interfaces.IGeografia
{
    public interface ISC_PAIS001Service
    {
        Task Add(SC_PAIS001 model);

        Task Delete(SC_PAIS001 model);

        Task<List<SC_PAIS001>> GetAll();

        Task<SC_PAIS001> GetById(int id);

        Task Update(SC_PAIS001 model);
    }
}
