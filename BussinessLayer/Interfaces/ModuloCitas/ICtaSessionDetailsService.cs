using BussinessLayer.DTOs.ModuloCitas.CtaSessionDetails;
using BussinessLayer.Interfaces.IOtros;
using DataLayer.Models.Modulo_Citas;

namespace BussinessLayer.Interface.Modulo_Citas
{
    public interface ICtaSessionDetailsService : IGenericService<CtaSessionDetailsRequest,CtaSessionDetailsResponse,CtaSessionDetails>
    {
    }
}
