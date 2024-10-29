using DataLayer.Models;
using DataLayer.Models.Empresa;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BussinessLayer.Interfaces.IEmpresa
{
    public interface ISC_EMP001service
    {
        Task Add(SC_EMP001 entity);

        Task<SC_EMP001> GetById(int id);

        Task<IList<SC_EMP001>> GetAll();
        
        Task Delete(int id);

        Task Edit(SC_EMP001 entity);
    }
}
