using BussinessLayer.Interfaces.RealTimeContracts;
using Microsoft.Extensions.DependencyInjection;
using SignalR.Services;

namespace SignalR
{
    public static class ServiceRegistration
    {
        public static void AddSignalRLayer(this IServiceCollection services)
        {
            services.AddSingleton<INotificationService, NotificationService>();

        }
    }
}
