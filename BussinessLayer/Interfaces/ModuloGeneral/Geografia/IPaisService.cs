using BussinessLayer.DTOs.ModuloGeneral.Geografia.DPais;
using BussinessLayer.Interfaces.IOtros;
using DataLayer.Models.ModuloGeneral.Geografia;

namespace BussinessLayer.Interfaces.ModuloGeneral.Geografia
{
    public interface IPaisService : IGenericService<CountryRequest, CountryResponse, Pais>
    {
    }
}