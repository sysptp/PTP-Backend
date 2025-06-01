using BussinessLayer.DTOs.ModuloCitas.CtaNotificationTemplates;
using BussinessLayer.Interfaces.Services.IOtros;
using DataLayer.Models.ModuloCitas;

namespace BussinessLayer.Interfaces.Services.ModuloCitas
{
    public interface ICtaNotificationTemplatesService : IGenericService<CtaNotificationTemplatesRequest, CtaNotificationTemplatesResponse, CtaNotificationTemplates>
    {
    }
}
