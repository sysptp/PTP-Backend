using System.Collections.Generic;
using System.Threading.Tasks;
using BussinessLayer.Interface.IOtros;
using DataLayer.Models.Cuentas;

namespace BussinessLayer.Interfaces.ICuentas
{
    public interface ICuentasPorCobrar : IBaseService<CuentasPorCobrar>
    {
        Task<IEnumerable<CuentasPorCobrar>> GetAllPaids();
    }
}
