using BussinessLayer.DTOs.ModuloCitas.CtaUnwanted;
using BussinessLayer.Interfaces.Services.IOtros;
using DataLayer.Models.Modulo_Citas;

namespace BussinessLayer.Interfaces.Services.ModuloCitas
{
    public interface ICtaUnwantedService : IGenericService<CtaUnwantedRequest, CtaUnwantedResponse, CtaUnwanted>
    {
    }
}
