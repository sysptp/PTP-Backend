using DataLayer.Models.ModuloVentas.Bancos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BussinessLayer.Interfaces.ModuloVentas.IBancos
{
    public interface IMovimientoBancoesService
    {
        Task Add(MovimientoBanco model);

        Task Delete(MovimientoBanco model);

        Task<List<MovimientoBanco>> GetAll();

        Task<List<MovimientoBanco>> GetAllIndex();

        Task<MovimientoBanco> GetById(int id);

        Task Update(MovimientoBanco model);
    }
}
