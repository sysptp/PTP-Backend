
using BussinessLayer.DTOs.ModuloCitas.CtaAppointments;

namespace BussinessLayer.Interfaces.RealTimeContracts
{
    public interface INotificationService
    {
        Task NotifyUserAsync(string userId, string title, string message, string type, object data = null);
        Task NotifyAppointmentParticipantsAsync(int appointmentId, string title, string message, string type, object data = null);
        Task NotifyAboutAppointmentCreationAsync(CtaAppointmentsRequest appointment, string userName);
        Task NotifyAboutAppointmentUpdateAsync(CtaAppointmentsRequest appointment, string previousState, string newState);
        Task PersistNotificationAsync(int userId, string title, string message, string type, object data, int? appointmentId);

    }
}
