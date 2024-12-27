using System.Collections.Generic;
using System.Threading.Tasks;
using BussinessLayer.Interfaces.Services.IOtros;
using DataLayer.Models.Cuentas;

namespace BussinessLayer.Interfaces.Services.ICuentas
{
    public interface ICuentasPorCobrar : IBaseService<CuentasPorCobrar>
    {
        Task<IEnumerable<CuentasPorCobrar>> GetAllPaids();
    }
}
