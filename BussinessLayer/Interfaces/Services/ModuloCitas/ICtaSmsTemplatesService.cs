using BussinessLayer.DTOs.ModuloCitas.CtaSmsTemplates;
using BussinessLayer.Interfaces.Services.IOtros;
using DataLayer.Models.ModuloCitas;

namespace BussinessLayer.Interfaces.Services.ModuloCitas
{
    public interface ICtaSmsTemplatesService : IGenericService<CtaSmsTemplatesRequest, CtaSmsTemplatesResponse, CtaSmsTemplates>
    {
    }
}
