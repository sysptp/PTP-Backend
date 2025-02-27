using BussinessLayer.DTOs.ModuloCitas.CtaNotificationSettings;
using BussinessLayer.Interfaces.Services.IOtros;
using DataLayer.Models.ModuloCitas;

namespace BussinessLayer.Interfaces.Services.ModuloCitas
{
    public interface ICtaNotificationSettingsService : IGenericService<CtaNotificationSettingsRequest, CtaNotificationSettingsResponse, CtaNotificationSettings>
    {
    }
}
