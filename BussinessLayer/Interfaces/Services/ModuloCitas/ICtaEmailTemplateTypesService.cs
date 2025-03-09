using BussinessLayer.DTOs.ModuloCitas.CtaEmailTemplateTypes;
using BussinessLayer.Interfaces.Services.IOtros;
using DataLayer.Models.ModuloCitas;

namespace BussinessLayer.Interfaces.Services.ModuloCitas
{
    public interface ICtaEmailTemplateTypesService : IGenericService<CtaEmailTemplateTypesRequest, CtaEmailTemplateTypesResponse, CtaEmailTemplateTypes>
    {
    }
}
