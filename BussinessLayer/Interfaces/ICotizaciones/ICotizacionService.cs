using BussinessLayer.Interface.IOtros;
using BussinessLayer.ViewModels;
using DataLayer.Models;
using DataLayer.Models.Cotizaciones;
using System.Threading.Tasks;

namespace BussinessLayer.Interface.ICotizaciones
{
    public interface ICotizacionService : IBaseService<Cotizacion>
    {
        Task Create(CotizacionViewModel cotizacion);


    }
}