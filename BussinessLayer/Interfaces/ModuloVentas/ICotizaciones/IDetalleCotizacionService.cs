using System.Collections.Generic;
using System.Threading.Tasks;
using BussinessLayer.Interface.IOtros;
using DataLayer.Models;
using DataLayer.Models.ModuloVentas.Cotizaciones;

namespace BussinessLayer.Interfaces.ModuloVentas.ICotizaciones
{
    public interface IDetalleCotizacionService : IBaseService<DetalleCotizacion>
    {
        Task<IEnumerable<DetalleCotizacion>> GetAllByCotizacionId(int cotizacionId);
    }
}