using AutoMapper;
using BussinessLayer.DTOs.ModuloCitas.CtaEmailTemplates;
using BussinessLayer.Interfaces.Repositories;
using BussinessLayer.Interfaces.Services.ModuloCitas;
using DataLayer.Models.ModuloCitas;

namespace BussinessLayer.Services.ModuloCitas
{
    public class CtaEmailTemplatesService : GenericService<CtaEmailTemplatesRequest, CtaEmailTemplatesResponse, CtaEmailTemplates>, ICtaEmailTemplatesService
    {
        public CtaEmailTemplatesService(IGenericRepository<CtaEmailTemplates> repository, IMapper mapper) : base(repository, mapper)
        {
        }
    }
}
