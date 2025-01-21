using BussinessLayer.DTOs.ModuloCitas;
using BussinessLayer.Interfaces.Services.IOtros;
using DataLayer.Models.ModuloCitas;

namespace BussinessLayer.Interfaces.Services.ModuloCitas
{
    public interface ICtaAppointmentSequenceService : IGenericService<CtaAppointmentSequenceRequest, CtaAppointmentSequenceResponse, CtaAppointmentSequence>
    {
        Task<string> GetFormattedSequenceAsync(long companyId);
        Task UpdateSequenceAsync(long companyId);
    }
}
