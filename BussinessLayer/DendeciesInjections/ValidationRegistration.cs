using BussinessLayer.Dtos.Account;
using BussinessLayer.DTOs.Autenticacion;
using BussinessLayer.DTOs.Empresas;
using BussinessLayer.DTOs.Seguridad;
using BussinessLayer.FluentValidations.Account;
using BussinessLayer.FluentValidations.Empresas.BussinessLayer.FluentValidations.Empresas;
using BussinessLayer.FluentValidations.Seguridad;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace BussinessLayer.DendeciesInjections
{
    public static class ValidationRegistration
    {
        public static void AddValidationInjections(this IServiceCollection services)
        {
            services.AddTransient<IValidator<GnEmpresaRequest>, SaveGnEmpresaRequestValidator>();
            services.AddScoped <IValidator<GnPerfilRequest>, GnPerfilRequestValidator>();
            services.AddScoped <IValidator<RegisterRequest>, RegisterRequestValidator>();
            services.AddScoped <IValidator<LoginRequestDTO>, LoginRequestValidator>();
        }
    }
}
