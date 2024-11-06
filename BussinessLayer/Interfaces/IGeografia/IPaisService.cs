using BussinessLayer.DTOs.Configuracion.Geografia.DPais;
using BussinessLayer.Interfaces.IOtros;
using DataLayer.Models.Geografia;

namespace BussinessLayer.Interfaces.IGeografia
{
    public interface IPaisService :IGenericService<CountryRequest, CountryResponse, Pais>
    {
    }
}