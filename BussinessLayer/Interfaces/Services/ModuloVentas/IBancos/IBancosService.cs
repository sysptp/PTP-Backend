using DataLayer.Models.ModuloVentas.Bancos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BussinessLayer.Interfaces.Services.ModuloVentas.IBancos
{
    public interface IBancosService
    {
        Task Add(Bancos model);

        Task Delete(Bancos model);

        Task<List<Bancos>> GetAll();

        Task<List<Bancos>> GetAllIndex();

        Task<Bancos> GetById(int id);

        Task Update(Bancos model);
    }
}
