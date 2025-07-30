using Microsoft.AspNetCore.SignalR;

namespace SignalR.Hubs
{
    public class NotificationHub : Hub
    {
        public async Task JoinUserGroup(string userId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, $"User_{userId}");
        }

        public async Task JoinAppointmentGroup(int appointmentId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, $"Appointment_{appointmentId}");
        }

        public async Task LeaveAppointmentGroup(int appointmentId)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, $"Appointment_{appointmentId}");
        }

        public override async Task OnConnectedAsync()
        {
            // Opcional: Puedes registrar conexiones aquí
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            // Opcional: Limpieza cuando un cliente se desconecta
            await base.OnDisconnectedAsync(exception);
        }
    }
}