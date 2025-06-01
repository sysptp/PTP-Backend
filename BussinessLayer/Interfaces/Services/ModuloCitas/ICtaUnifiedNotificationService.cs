using BussinessLayer.DTOs.ModuloCitas;
using BussinessLayer.DTOs.ModuloCitas.CtaAppointments;
using BussinessLayer.Enums;

namespace BussinessLayer.Interfaces.Services.ModuloCitas
{
    public interface ICtaUnifiedNotificationService
    {
        Task SendNotificationsForAppointmentAsync(CtaAppointmentsRequest appointment,
            NotificationType notificationType, NotificationContext context);
    }
}
