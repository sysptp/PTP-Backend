using DataLayer.Models.Geografia;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BussinessLayer.Interfaces.IGeografia
{
    public interface ISC_MUNIC001Service
    {
        Task Add(SC_MUNIC001 model);

        Task Delete(SC_MUNIC001 model);

        Task<List<SC_MUNIC001>> GetAll();

        Task<List<SC_MUNIC001>> GetAllIndex();

        Task<SC_MUNIC001> GetById(int id);

        Task Update(SC_MUNIC001 model);
    }
}
