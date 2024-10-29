using DataLayer.Models;
using DataLayer.Models.Otros;
using System.Collections.Generic;

namespace BussinessLayer.Interface.IOtros
{
    public interface ITipoTransaccionService : IBaseService<TipoTransaccion>
    {
        IList<TipoTransaccion> GetAllE();

    }
}
