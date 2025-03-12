using AutoMapper;
using BussinessLayer.DTOs.ModuloCitas.CtaEmailTemplateVariables;
using BussinessLayer.Interfaces.Repositories;
using BussinessLayer.Interfaces.Services.ModuloCitas;
using DataLayer.Models.ModuloCitas;

namespace BussinessLayer.Services.ModuloCitas
{
    public class CtaEmailTemplateVariablesService : GenericService<CtaEmailTemplateVariablesRequest, CtaEmailTemplateVariablesResponse, CtaEmailTemplateVariables>, ICtaEmailTemplateVariablesService
    {
        public CtaEmailTemplateVariablesService(IGenericRepository<CtaEmailTemplateVariables> repository, IMapper mapper) : base(repository, mapper)
        {
        }
    }
}
