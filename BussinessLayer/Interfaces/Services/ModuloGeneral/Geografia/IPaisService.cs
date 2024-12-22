using BussinessLayer.DTOs.ModuloGeneral.Geografia.DPais;
using BussinessLayer.Interfaces.Services.IOtros;
using DataLayer.Models.ModuloGeneral.Geografia;

namespace BussinessLayer.Interfaces.Services.ModuloGeneral.Geografia
{
    public interface IPaisService : IGenericService<CountryRequest, CountryResponse, Pais>
    {
    }
}