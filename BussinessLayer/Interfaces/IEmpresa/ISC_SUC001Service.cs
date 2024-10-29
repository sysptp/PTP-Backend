using DataLayer.Models;
using DataLayer.Models.Empresa;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BussinessLayer.Interfaces.IEmpresa
{
    public interface ISC_SUC001Service
    {
        Task Add(SC_SUC001 model);

        Task Delete(SC_SUC001 model);

        Task<List<SC_SUC001>> GetAll();

        Task<SC_SUC001> GetById(int id);

        Task Update(SC_SUC001 model);

        Task<List<SC_SUC001>> GetAllIndex();
    }
}
