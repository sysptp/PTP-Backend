using AutoMapper;
using BussinessLayer.DTOs.ModuloCitas.CtaSessions;
using BussinessLayer.Interfaces.Repositories;
using BussinessLayer.Interfaces.Services.ModuloCitas;
using BussinessLayer.Services;

namespace DataLayer.Models.Modulo_Citas
{
    public class CtaSessionsService : GenericService<CtaSessionsRequest, CtaSessionsResponse, CtaSessions>, ICtaSessionsService
    {
        public CtaSessionsService(IGenericRepository<CtaSessions> repository, IMapper mapper) : base(repository, mapper)
        {
        }

    }
}
