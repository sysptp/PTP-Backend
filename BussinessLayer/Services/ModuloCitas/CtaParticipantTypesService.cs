using AutoMapper;
using BussinessLayer.DTOs.ModuloCitas.CtaParticipantTypes;
using BussinessLayer.Interfaces.Repositories;
using BussinessLayer.Interfaces.Services.ModuloCitas;
using DataLayer.Models.ModuloCitas;

namespace BussinessLayer.Services.ModuloCitas
{
    public class CtaParticipantTypesService : GenericService<CtaParticipantTypesRequest, CtaParticipantTypesResponse, CtaParticipantTypes>, ICtaParticipantTypesServices
    {
        public CtaParticipantTypesService(IGenericRepository<CtaParticipantTypes> repository, IMapper mapper) : base(repository, mapper)
        {
        }
    }
}
