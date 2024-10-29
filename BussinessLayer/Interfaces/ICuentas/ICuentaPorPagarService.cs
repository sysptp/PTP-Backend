using System.Threading.Tasks;
using BussinessLayer.Interface.IOtros;
using DataLayer.Models;
using DataLayer.Models.Cuentas;

namespace BussinessLayer.Interfaces.ICuentas
{
    public interface ICuentaPorPagarService : IBaseService<CuentasPorPagar>
    {
        Task Create(CuentasPorPagar dcp);
        Task AddWithInicial(CuentasPorPagar entity);
    }
}
