using AutoMapper;
using BussinessLayer.DTOs.ModuloCitas.CtaMessageTemplates;
using BussinessLayer.Interfaces.Repositories;
using BussinessLayer.Interfaces.Services.ModuloCitas;
using DataLayer.Models.ModuloCitas;

namespace BussinessLayer.Services.ModuloCitas
{
    public class CtaMessageTemplatesService : GenericService<CtaMessageTemplatesRequest, CtaMessageTemplatesResponse, CtaMessageTemplates>, ICtaMessageTemplatesService
    {
        public CtaMessageTemplatesService(IGenericRepository<CtaMessageTemplates> repository, IMapper mapper) : base(repository, mapper)
        {
        }
    }
}
