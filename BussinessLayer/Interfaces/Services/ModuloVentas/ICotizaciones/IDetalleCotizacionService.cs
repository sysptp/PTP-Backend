using System.Collections.Generic;
using System.Threading.Tasks;
using BussinessLayer.Interfaces.Services.IOtros;
using DataLayer.Models;
using DataLayer.Models.ModuloVentas.Cotizaciones;

namespace BussinessLayer.Interfaces.Services.ModuloVentas.ICotizaciones
{
    public interface IDetalleCotizacionService : IBaseService<DetalleCotizacion>
    {
        Task<IEnumerable<DetalleCotizacion>> GetAllByCotizacionId(int cotizacionId);
    }
}