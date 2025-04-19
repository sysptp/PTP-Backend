using BussinessLayer.DTOs.ModuloCitas.CtaWhatsAppTemplates;
using BussinessLayer.Interfaces.Services.IOtros;
using DataLayer.Models.ModuloCitas;

namespace BussinessLayer.Interfaces.Services.ModuloCitas
{
    public interface ICtaWhatsAppTemplatesService : IGenericService<CtaWhatsAppTemplatesRequest, CtaWhatsAppTemplatesResponse, CtaWhatsAppTemplates>
    {
    }
}
