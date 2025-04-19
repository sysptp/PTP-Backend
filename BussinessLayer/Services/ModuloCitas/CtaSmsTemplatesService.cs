using AutoMapper;
using BussinessLayer.DTOs.ModuloCitas.CtaSmsTemplates;
using BussinessLayer.Interfaces.Repositories;
using BussinessLayer.Interfaces.Services.ModuloCitas;
using DataLayer.Models.ModuloCitas;

namespace BussinessLayer.Services.ModuloCitas
{
    public class CtaSmsTemplatesService : GenericService<CtaSmsTemplatesRequest, CtaSmsTemplatesResponse, CtaSmsTemplates>, ICtaSmsTemplatesService
    {
        public CtaSmsTemplatesService(IGenericRepository<CtaSmsTemplates> repository, IMapper mapper) : base(repository, mapper)
        {
        }
    }
}
