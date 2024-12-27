using BussinessLayer.DTOs.ModuloCitas.CtaSessionDetails;
using BussinessLayer.Interfaces.Services.IOtros;
using DataLayer.Models.Modulo_Citas;

namespace BussinessLayer.Interfaces.Services.ModuloCitas
{
    public interface ICtaSessionDetailsService : IGenericService<CtaSessionDetailsRequest, CtaSessionDetailsResponse, CtaSessionDetails>
    {
    }
}
