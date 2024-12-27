using System.Threading.Tasks;
using BussinessLayer.Interfaces.Services.IOtros;
using DataLayer.Models;
using DataLayer.Models.Cuentas;

namespace BussinessLayer.Interfaces.Services.ICuentas
{
    public interface ICuentaPorPagarService : IBaseService<CuentasPorPagar>
    {
        Task Create(CuentasPorPagar dcp);
        Task AddWithInicial(CuentasPorPagar entity);
    }
}
