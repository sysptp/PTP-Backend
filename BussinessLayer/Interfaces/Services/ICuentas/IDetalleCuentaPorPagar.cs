using System.Collections.Generic;
using System.Threading.Tasks;
using BussinessLayer.Interfaces.Services.IOtros;
using DataLayer.Models;
using DataLayer.Models.Cuentas;

namespace BussinessLayer.Interfaces.Services.ICuentas
{
    public interface IDetalleCuentaPorPagar : IBaseService<DetalleCuentaPorPagar>
    {
        Task<IEnumerable<DetalleCuentaPorPagar>> GetAllByIdCtaPorPagar(int idcta);
    }
}
