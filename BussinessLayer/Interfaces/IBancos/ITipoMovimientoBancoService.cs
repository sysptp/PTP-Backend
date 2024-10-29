using DataLayer.Models.Bancos;
using DataLayer.Models.Caja;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BussinessLayer.Interfaces.IBancos
{
    public interface ITipoMovimientoBancoService
    {
        Task Add(TipoMovimientoBanco model);

        Task Delete(TipoMovimientoBanco model);

        Task<List<TipoMovimientoBanco>> GetAll();

        Task<List<TipoMovimientoBanco>> GetAllIndex();

        Task<TipoMovimientoBanco> GetById(int id);

        Task Update(TipoMovimientoBanco model);
    }
}
