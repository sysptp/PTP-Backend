using BussinessLayer.Interface.IOtros;
using BussinessLayer.ViewModels;
using DataLayer.Models.Cotizaciones;

public interface ICotizacionService : IBaseService<Cotizacion>
{
    Task Create(CotizacionViewModel cotizacion);


}
