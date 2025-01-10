using AutoMapper;
using BussinessLayer.DTOs.ModuloCitas.CtaSessionDetails;
using BussinessLayer.Interface.Modulo_Citas;
using BussinessLayer.Interfaces.Repositories;
using BussinessLayer.Services;

namespace DataLayer.Models.Modulo_Citas
{
    public class CtaSessionDetailsService : GenericService<CtaSessionDetailsRequest, CtaSessionDetailsResponse, CtaSessionDetails>, ICtaSessionDetailsService
    {
        public CtaSessionDetailsService(IGenericRepository<CtaSessionDetails> repository, IMapper mapper) : base(repository, mapper)
        {
        }
    }
}
