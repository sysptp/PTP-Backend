using BussinessLayer.DTOs.ModuloCitas.CtaSessions;
using BussinessLayer.Interfaces.IOtros;
using DataLayer.Models.Modulo_Citas;

namespace BussinessLayer.Interface.Modulo_Citas
{
    public interface ICtaSessionsService : IGenericService<CtaSessionsRequest, CtaSessionsResponse, CtaSessions>
    {
    }
}
