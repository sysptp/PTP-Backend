using BussinessLayer.DTOs.ModuloCitas.CtaAppointments;
using BussinessLayer.Interfaces.RealTimeContracts;
using Microsoft.AspNetCore.SignalR;
using SignalR.Hubs;

namespace SignalR.Services
{
    public class NotificationService : INotificationService
    {
        private readonly IHubContext<NotificationHub> _hubContext;

        public NotificationService(IHubContext<NotificationHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public async Task NotifyUserAsync(string userId, string title, string message, string type, object data = null)
        {
            var notification = new
            {
                title = title,
                message = message,
                type = type,
                data = data,
                timestamp = DateTime.UtcNow
            };

            await _hubContext.Clients.Group($"User_{userId}").SendAsync("ReceiveNotification", notification);
        }

        public async Task NotifyAppointmentParticipantsAsync(int appointmentId, string title, string message, string type, object data = null)
        {
            var notification = new
            {
                Title = title,
                Message = message,
                Type = type,
                Data = data,
                AppointmentId = appointmentId,
                Timestamp = DateTime.UtcNow
            };

            await _hubContext.Clients.Group($"Appointment_{appointmentId}").SendAsync("ReceiveNotification", notification);
        }

        public async Task NotifyAboutAppointmentCreationAsync(CtaAppointmentsRequest appointment, string userName)
        {
            var title = "Nueva Cita Creada";
            var message = $"Se ha creado una nueva cita con código {appointment.AppointmentCode}";

            await NotifyUserAsync(appointment.AssignedUser.ToString(), title, message, "Cita Creada", new
            {
                appointment.AppointmentId,
                appointment.AppointmentCode,
                appointment.Description,
                appointment.AppointmentDate,
                appointment.AppointmentTime,
                appointment.EndAppointmentTime
            });

            await NotifyAppointmentParticipantsAsync(appointment.AppointmentId, title, message, "Cita Creada");
        }

        public async Task NotifyAboutAppointmentUpdateAsync(CtaAppointmentsRequest appointment, string previousState, string newState)
        {
            var title = "Actualización de Cita";
            string message;

            if (previousState != null && newState != null)
            {
                message = $"La cita {appointment.AppointmentCode} ha cambiado de estado: {previousState} → {newState}";
            }
            else
            {
                message = $"La cita {appointment.AppointmentCode} ha sido actualizada";
            }

            await NotifyUserAsync(appointment.AssignedUser.ToString(), title, message, "Cita Actualizada", new
            {
                appointment.AppointmentId,
                appointment.AppointmentCode,
                PreviousState = previousState,
                NewState = newState
            });

            await NotifyAppointmentParticipantsAsync(appointment.AppointmentId, title, message, "Cita Actualizada");
        }
    }
}

