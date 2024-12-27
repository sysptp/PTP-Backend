using DataLayer.Models.ModuloVentas.Bancos;

namespace BussinessLayer.Interfaces.Services.ModuloVentas.IBancos
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
