using System.Collections.Generic;
using System.Threading.Tasks;
using BussinessLayer.Interfaces.Services.IOtros;
using DataLayer.Models.Cuentas;

namespace BussinessLayer.Interfaces.Services.ICuentas
{
    public interface IDetalleCuentasPorCobrar : IBaseService<DetalleCuentasPorCobrar>
    {
        Task<IEnumerable<DetalleCuentasPorCobrar>> GetAllByCuentaPorCobrarId(int id);
    }
}
