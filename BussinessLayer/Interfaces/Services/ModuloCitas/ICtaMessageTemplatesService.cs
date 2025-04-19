using BussinessLayer.DTOs.ModuloCitas.CtaMessageTemplates;
using BussinessLayer.Interfaces.Services.IOtros;
using DataLayer.Models.ModuloCitas;

namespace BussinessLayer.Interfaces.Services.ModuloCitas
{
    public interface ICtaMessageTemplatesService : IGenericService<CtaMessageTemplatesRequest, CtaMessageTemplatesResponse, CtaMessageTemplates>
    {
    }
}
