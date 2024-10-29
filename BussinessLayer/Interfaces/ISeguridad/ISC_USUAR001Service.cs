using DataLayer.Models;
using DataLayer.Models.Seguridad;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BussinessLayer.Interfaces.ISeguridad
{
    public interface ISC_USUAR001Service
    {
        Task Add(SC_USUAR001 model);

        Task Delete(SC_USUAR001 model);

        Task<List<SC_USUAR001>> GetAll();

        Task<SC_USUAR001> GetById(int id);

        Task Update(SC_USUAR001 model);

        Task<List<SC_USUAR001>> GetAllWithEmpId(long id);
    }
}
