using BussinessLayer.DTOs.ModuloCitas.CtaUnwanted;
using BussinessLayer.Interfaces.IOtros;
using DataLayer.Models.Modulo_Citas;

namespace BussinessLayer.Interface.Modulo_Citas
{
    public interface ICtaUnwantedService : IGenericService<CtaUnwantedRequest, CtaUnwantedResponse, CtaUnwanted>
    {
    }
}
