 using AutoMapper;
using BussinessLayer.DTOs.ModuloCitas.CtaEmailTemplateTypes;
using BussinessLayer.Interfaces.Repositories;
using BussinessLayer.Interfaces.Services.ModuloCitas;
using DataLayer.Models.ModuloCitas;

namespace BussinessLayer.Services.ModuloCitas
{
    public class CtaEmailTemplateTypesService : GenericService<CtaEmailTemplateTypesRequest, CtaEmailTemplateTypesResponse, CtaEmailTemplateTypes>, ICtaEmailTemplateTypesService
    {
        public CtaEmailTemplateTypesService(IGenericRepository<CtaEmailTemplateTypes> repository, IMapper mapper) : base(repository, mapper)
        {
        }
    }
}
