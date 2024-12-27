using AutoMapper;
using BussinessLayer.DTOs.ModuloGeneral.Geografia.DPais;
using BussinessLayer.Interfaces.Repositories;
using BussinessLayer.Interfaces.Services.ModuloGeneral.Geografia;
using DataLayer.Models.ModuloGeneral.Geografia;

namespace BussinessLayer.Services.ModuloGeneral.Geografia
{
    public class PaisService : GenericService<CountryRequest, CountryResponse, Pais>, IPaisService
    {
        public PaisService(IGenericRepository<Pais> repository, IMapper mapper) : base(repository, mapper)
        {
        }
    }
}
