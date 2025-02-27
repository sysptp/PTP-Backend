using BussinessLayer.DTOs.ModuloCitas.CtaSessions;
using BussinessLayer.Interfaces.Services.IOtros;
using BussinessLayer.Wrappers;
using DataLayer.Models.Modulo_Citas;

namespace BussinessLayer.Interfaces.Services.ModuloCitas
{
    public interface ICtaSessionsService : IGenericService<CtaSessionsRequest, CtaSessionsResponse, CtaSessions>
    {
        Task<CtaSessionsRequest> CreateSessionAndGenerateAppointments(CtaSessionsRequest sessionRequest);
        Task DeleteAppointmentsInSessionRange(CtaSessionsRequest sessionDto);
        Task<DetailMessage> GetConflictingAppointmentsInSessionRange(CtaSessionsRequest sessionDto);
    }
}
