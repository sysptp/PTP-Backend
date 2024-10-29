using DataLayer.Models;
using DataLayer.Models.Cotizaciones;

namespace BussinessLayer.ViewModels
{
    public class CotizacionViewModel
    {
        public Cotizacion Cotizacion { get; set; }

        public DetalleCotizacion[] DetalleCotizacion { get; set; }
    }
}
