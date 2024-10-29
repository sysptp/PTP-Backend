using System.Collections.Generic;
using System.Threading.Tasks;
using BussinessLayer.Interface.IOtros;
using DataLayer.Models;
using DataLayer.Models.Cotizaciones;

namespace BussinessLayer.Interface.ICotizaciones
{
    public interface IDetalleCotizacionService : IBaseService<DetalleCotizacion>
    {
       Task<IEnumerable<DetalleCotizacion>> GetAllByCotizacionId(int cotizacionId);
    }
}