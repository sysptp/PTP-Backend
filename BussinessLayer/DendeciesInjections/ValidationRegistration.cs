using BussinessLayer.FluentValidations.Account;
using BussinessLayer.FluentValidations.Productos;
using Microsoft.Extensions.DependencyInjection;

namespace BussinessLayer.DendeciesInjections
{
    public static class ValidationRegistration
    {
        public static void AddValidationInjections(this IServiceCollection services)
        {
            services.AddScoped<RegisterRequestValidator>();
            services.AddScoped<ProductosRequestValidator>();
        }
    }
}
