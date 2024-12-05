using BussinessLayer.DTOs.ModuloCitas.CtaSessions;
using BussinessLayer.Interfaces.IOtros;

namespace DataLayer.Models.Modulo_Citas
{
    public interface ICtaSessionsService : IGenericService<CtaSessionsRequest, CtaSessionsResponse, CtaSessions>
    {
    }
}
