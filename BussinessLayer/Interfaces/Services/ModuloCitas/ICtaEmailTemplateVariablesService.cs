using BussinessLayer.DTOs.ModuloCitas.CtaEmailTemplateVariables;
using BussinessLayer.Interfaces.Services.IOtros;
using DataLayer.Models.ModuloCitas;

namespace BussinessLayer.Interfaces.Services.ModuloCitas
{
    public interface ICtaEmailTemplateVariablesService : IGenericService<CtaEmailTemplateVariablesRequest, CtaEmailTemplateVariablesResponse, CtaEmailTemplateVariables>
    {
    }
}
