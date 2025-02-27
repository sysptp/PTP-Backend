using AutoMapper;
using BussinessLayer.DTOs.ModuloCitas.CtaNotificationSettings;
using BussinessLayer.Interfaces.Repositories;
using BussinessLayer.Interfaces.Services.ModuloCitas;
using DataLayer.Models.ModuloCitas;

namespace BussinessLayer.Services.ModuloCitas
{
    public class CtaNotificationSettingsService : GenericService<CtaNotificationSettingsRequest, CtaNotificationSettingsResponse, CtaNotificationSettings>, ICtaNotificationSettingsService
    {
        public CtaNotificationSettingsService(IGenericRepository<CtaNotificationSettings> repository, IMapper mapper) : base(repository, mapper)
        {
        }
    }
}
