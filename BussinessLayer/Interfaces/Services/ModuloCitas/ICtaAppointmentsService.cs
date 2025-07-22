using BussinessLayer.DTOs.Common;
using BussinessLayer.DTOs.ModuloCitas.CtaAppointments;
using BussinessLayer.Interfaces.Services.IOtros;
using BussinessLayer.Wrappers;
using DataLayer.Models.Modulo_Citas;

namespace BussinessLayer.Interfaces.Services.ModuloCitas
{
    public interface ICtaAppointmentsService : IGenericService<CtaAppointmentsRequest, CtaAppointmentsResponse, CtaAppointments>
    {
        Task<DetailMessage> ExistsAppointmentInTimeRange(CtaAppointmentsRequest appointmentDto);
        Task DeleteExistsAppointmentInTimeRange(CtaAppointmentsRequest appointmentDto);
        Task<List<AppointmentParticipantsResponse>> GetAllParticipants();
        Task<CtaAppointmentsResponse> AddAppointment(CtaAppointmentsRequest vm, bool IsForSession);
        Task<List<AppointmentParticipantsResponse>> GetAllParticipantsByAppointmentId(int appointmentId);
        Task UpdateAppointmentParticipants(CtaAppointmentsRequest vm, int appointmentId, CtaAppointmentsResponse appointmentEntity);
        Task<PaginatedResponse<CtaAppointmentsResponse>> GetAllPaginatedAsync(PaginationRequest pagination, long? companyId = null, int? userId = null, string? appointmentCode = null);

    }
}
