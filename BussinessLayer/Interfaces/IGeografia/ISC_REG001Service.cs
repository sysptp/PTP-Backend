using DataLayer.Models.Geografia;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BussinessLayer.Interfaces.IGeografia
{
    public interface ISC_REG001Service
    {
        Task Add(SC_REG001 model);

        Task Delete(SC_REG001 model);

        Task<List<SC_REG001>> GetAll();

        Task<SC_REG001> GetById(int id);

        Task Update(SC_REG001 model);

        Task<List<SC_REG001>> GetAllWithCountry();

        Task<List<SC_REG001>> GetAllIndex();
    }
}
