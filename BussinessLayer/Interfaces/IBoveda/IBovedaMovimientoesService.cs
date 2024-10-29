using DataLayer.Models.Boveda;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BussinessLayer.Interfaces.IBoveda
{
    public interface IBovedaMovimientoesService
    {
        Task Add(BovedaMovimiento model);

        Task Delete(BovedaMovimiento model);

        Task<List<BovedaMovimiento>> GetAll();


        Task<BovedaMovimiento> GetById(int id);

        Task Update(BovedaMovimiento model);
    }
}
