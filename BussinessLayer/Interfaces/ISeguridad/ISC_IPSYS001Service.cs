using DataLayer.Models;
using DataLayer.Models.Caja;
using DataLayer.Models.Seguridad;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BussinessLayer.Interfaces.ISeguridad
{
    public interface ISC_IPSYS001Service
    {
        Task Add(SC_IPSYS001 model);

        Task Delete(SC_IPSYS001 model);

        Task<List<SC_IPSYS001>> GetAll();

        Task<List<SC_IPSYS001>> GetAllIndex();

        Task<SC_IPSYS001> GetById(int id);

        Task Update(SC_IPSYS001 model);
    }
}
