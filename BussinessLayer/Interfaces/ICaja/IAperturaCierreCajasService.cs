using DataLayer.Models.Caja;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BussinessLayer.Interfaces.ICaja
{
    public interface IAperturaCierreCajasService
    {
        Task Add(AperturaCierreCaja model);

        Task Delete(AperturaCierreCaja model);

        Task<List<AperturaCierreCaja>> GetAll();

        Task<List<AperturaCierreCaja>> GetAllIndex();

        Task<AperturaCierreCaja> GetById(int id);

        Task Update(AperturaCierreCaja model);
    }
}
