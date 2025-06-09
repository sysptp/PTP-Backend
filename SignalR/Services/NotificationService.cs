using BussinessLayer.DTOs.ModuloCitas.CtaAppointments;
using BussinessLayer.DTOs.ModuloCitas.CtaUserNotificationReads;
using BussinessLayer.Interfaces.RealTimeContracts;
using BussinessLayer.Interfaces.Services.ModuloCitas;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using SignalR.Hubs;

namespace SignalR.Services
{
    public class NotificationService : INotificationService
    {
        private readonly IHubContext<NotificationHub> _hubContext;
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public NotificationService(IHubContext<NotificationHub> hubContext, IServiceScopeFactory serviceScopeFactory)
        {
            _hubContext = hubContext;
            _serviceScopeFactory = serviceScopeFactory;
        }

        public async Task NotifyUserAsync(string userId, string title, string message, string type, object data = null)
        {
            var notification = new
            {
                Title = title,
                Message = message,
                Type = type,
                Data = data,
                CreatedDate = DateTime.UtcNow
            };

            // Enviar notificación en tiempo real por SignalR
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
                CreatedDate = DateTime.UtcNow
            };

            await _hubContext.Clients.Group($"Appointment_{appointmentId}").SendAsync("ReceiveNotification", notification);
        }

        public async Task NotifyAboutAppointmentCreationAsync(CtaAppointmentsRequest appointment, string userName)
        {
            var title = "Nueva Cita Creada";
            var message = $"Se ha creado una nueva cita con código {appointment.AppointmentCode}";
            var data = new
            {
                appointment.AppointmentId,
                appointment.AppointmentCode,
                appointment.Description,
                appointment.AppointmentDate,
                appointment.AppointmentTime,
                appointment.EndAppointmentTime
            };

            await NotifyUserAsync(appointment.AssignedUser.ToString(), title, message, "Nueva Cita Creada", data);
            await NotifyAppointmentParticipantsAsync(appointment.AppointmentId, title, message, "Nueva Cita Creada", data);
        }

        public async Task NotifyAboutAppointmentUpdateAsync(CtaAppointmentsRequest appointment, string previousState, string newState)
        {
            var title = "Actualización de Cita";
            string message;
            string notificationType;

            if (previousState != null && newState != null)
            {
                message = $"La cita {appointment.AppointmentCode} ha cambiado de estado: {previousState} → {newState}";
                notificationType = "appointment_state_changed";
            }
            else
            {
                message = $"La cita {appointment.AppointmentCode} ha sido actualizada";
                notificationType = "appointment_updated";
            }

            var data = new
            {
                appointment.AppointmentId,
                appointment.AppointmentCode,
                PreviousState = previousState,
                NewState = newState
            };

            await NotifyUserAsync(appointment.AssignedUser.ToString(), title, message, notificationType, data);
            await NotifyAppointmentParticipantsAsync(appointment.AppointmentId, title, message, notificationType, data);
        }

        public async Task PersistNotificationAsync(int userId, string title, string message, string type, object data, int? appointmentId)
        {
            try
            {
                using var scope = _serviceScopeFactory.CreateScope();
                var notificationService = scope.ServiceProvider.GetRequiredService<ICtaUserNotificationReadsService>();

                long companyId = 0;
                if (data != null)
                {
                    var dataJson = JsonConvert.SerializeObject(data);
                    var dataDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(dataJson);
                    if (dataDict != null && dataDict.ContainsKey("CompanyId"))
                    {
                        if (long.TryParse(dataDict["CompanyId"].ToString(), out long extractedCompanyId))
                        {
                             companyId = extractedCompanyId;
                        }
                    }
                }

                var notificationRequest = new CtaUserNotificationReadsRequest
                {
                    UserId = userId,
                    Type = type,
                    IsRead = false,
                    Title = title,
                    Message = message,
                    Data = data != null ? JsonConvert.SerializeObject(data) : null,
                    CreatedDate = DateTime.UtcNow,
                    AppointmentId = appointmentId,
                    CompanyId = companyId
                };

                await notificationService.CreateNotificationAsync(notificationRequest);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error persisting notification: {ex.Message}");
            }
        }

    }
}