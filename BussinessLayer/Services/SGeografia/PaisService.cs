using AutoMapper;
using BussinessLayer.DTOs.Geografia.DPais;
using BussinessLayer.Interfaces.IGeografia;
using BussinessLayer.Interfaces.Repositories;
using DataLayer.Models.Geografia;

namespace BussinessLayer.Services.SGeografia
{
    public class PaisService : GenericService<CountryRequest, CountryResponse, Pais>, IPaisService
    {
        public PaisService(IGenericRepository<Pais> repository, IMapper mapper) : base(repository, mapper)
        {
        }
    }
}
