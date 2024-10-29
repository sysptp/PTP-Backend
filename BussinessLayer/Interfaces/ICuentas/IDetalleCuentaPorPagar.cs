using System.Collections.Generic;
using System.Threading.Tasks;
using BussinessLayer.Interface.IOtros;
using DataLayer.Models;
using DataLayer.Models.Cuentas;

namespace BussinessLayer.Interfaces.ICuentas
{
    public interface IDetalleCuentaPorPagar : IBaseService<DetalleCuentaPorPagar>
    {
        Task<IEnumerable<DetalleCuentaPorPagar>> GetAllByIdCtaPorPagar(int idcta);
    }
}
