using DataLayer.Models.Caja;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BussinessLayer.Interfaces.ICaja
{
    public interface IMonedasService
    {
        Task Add(Moneda model);

        Task Delete(Moneda model);

        Task<List<Moneda>> GetAll();

        Task<Moneda> GetById(int id);

        Task Update(Moneda model);
    }
}
