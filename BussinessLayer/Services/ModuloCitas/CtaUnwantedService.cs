using AutoMapper;
using BussinessLayer.DTOs.ModuloCitas.CtaUnwanted;
using BussinessLayer.Interface.Modulo_Citas;
using BussinessLayer.Interfaces.Repositories;
using BussinessLayer.Services;

namespace DataLayer.Models.Modulo_Citas
{
    public class CtaUnwantedService : GenericService<CtaUnwantedRequest, CtaUnwantedResponse, CtaUnwanted>, ICtaUnwantedService
    {
        public CtaUnwantedService(IGenericRepository<CtaUnwanted> repository, IMapper mapper) : base(repository, mapper)
        {
        }
    }
}
