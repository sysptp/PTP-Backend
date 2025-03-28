using BussinessLayer.DTOs.ModuloCitas.CtaAppointments;
using BussinessLayer.DTOs.ModuloCitas.CtaSessions;

namespace BussinessLayer.Interfaces.Services.ModuloCitas
{
    public interface ICtaSessionEmailService
    {
        Task SendSessionEmailsAsync(CtaSessionsRequest sessionRequest, List<CtaAppointmentsResponse> appointments);
    }
}