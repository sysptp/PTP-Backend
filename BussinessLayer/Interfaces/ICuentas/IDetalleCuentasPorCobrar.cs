using System.Collections.Generic;
using System.Threading.Tasks;
using BussinessLayer.Interface.IOtros;
using DataLayer.Models.Cuentas;

namespace BussinessLayer.Interfaces.ICuentas
{
    public interface IDetalleCuentasPorCobrar : IBaseService<DetalleCuentasPorCobrar>
    {
        Task<IEnumerable<DetalleCuentasPorCobrar>> GetAllByCuentaPorCobrarId(int id);
    }
}
