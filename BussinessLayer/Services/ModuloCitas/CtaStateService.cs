using AutoMapper;
using BussinessLayer.DTOs.ModuloCitas.CtaState;
using BussinessLayer.Interfaces.Repositories;
using BussinessLayer.Interfaces.Services.ModuloCitas;
using BussinessLayer.Services;

namespace DataLayer.Models.Modulo_Citas
{
    public class CtaStateService : GenericService<CtaStateRequest, CtaStateResponse, CtaState>, ICtaStateService
    {
        public CtaStateService(IGenericRepository<CtaState> repository, IMapper mapper) : base(repository, mapper)
        {
        }
    }
}
