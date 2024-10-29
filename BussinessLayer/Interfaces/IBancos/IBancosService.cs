using DataLayer.Models.Bancos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BussinessLayer.Interfaces.IBancos
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
