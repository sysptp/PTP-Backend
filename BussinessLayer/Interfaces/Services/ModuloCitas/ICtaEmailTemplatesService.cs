using BussinessLayer.DTOs.ModuloCitas.CtaEmailTemplates;
using BussinessLayer.Interfaces.Services.IOtros;
using DataLayer.Models.ModuloCitas;

namespace BussinessLayer.Interfaces.Services.ModuloCitas
{
    public interface ICtaEmailTemplatesService : IGenericService<CtaEmailTemplatesRequest, CtaEmailTemplatesResponse, CtaEmailTemplates>
    {
    }
}
