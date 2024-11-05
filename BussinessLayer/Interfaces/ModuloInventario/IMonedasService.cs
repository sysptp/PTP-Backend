using DataLayer.Models.ModuloInventario;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BussinessLayer.Interfaces.ModuloInventario
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
