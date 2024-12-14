using DataLayer.Models.ModuloVentas.Caja;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BussinessLayer.Interfaces.ModuloVentas.ICaja
{
    public interface ICajaService
    {
        Task Add(Caja model);

        Task Delete(Caja model);

        Task<List<Caja>> GetAll();

        Task<Caja> GetById(int id);

        Task Update(Caja model);
    }
}
