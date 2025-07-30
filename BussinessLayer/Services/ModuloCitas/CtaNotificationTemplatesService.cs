using AutoMapper;
using BussinessLayer.DTOs.ModuloCitas.CtaNotificationTemplates;
using BussinessLayer.Interfaces.Repositories;
using BussinessLayer.Interfaces.Services.ModuloCitas;
using DataLayer.Models.ModuloCitas;

namespace BussinessLayer.Services.ModuloCitas
{
    public class CtaNotificationTemplatesService : GenericService<CtaNotificationTemplatesRequest, CtaNotificationTemplatesResponse, CtaNotificationTemplates>, ICtaNotificationTemplatesService
    {
        public CtaNotificationTemplatesService(IGenericRepository<CtaNotificationTemplates> repository, IMapper mapper) : base(repository, mapper)
        {
        }
    }
}
