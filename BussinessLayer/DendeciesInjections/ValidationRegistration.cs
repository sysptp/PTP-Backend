using BussinessLayer.FluentValidations.Productos;
using BussinessLayer.FluentValidations.Empresas.BussinessLayer.FluentValidations.Empresas;
using FluentValidation;
using BussinessLayer.FluentValidations.Account;
using BussinessLayer.FluentValidations.ModuloInventario;
using Microsoft.Extensions.DependencyInjection;
using BussinessLayer.DTOs.ModuloGeneral.Empresas;
using BussinessLayer.DTOs.Configuracion.Seguridad;
using BussinessLayer.DTOs.Configuracion.Seguridad.Autenticacion;
using BussinessLayer.DTOs.Configuracion.Account;
using BussinessLayer.FluentValidations.Configuracion.Seguridad;
using BussinessLayer.FluentValidations.Configuracion.Account;

namespace BussinessLayer.DendeciesInjections
{
    public static class ValidationRegistration
    {
        public static void AddValidationInjections(this IServiceCollection services)
        {
            services.AddScoped<RegisterRequestValidator>();
            services.AddScoped<ProductosRequestValidator>();
            services.AddTransient<IValidator<GnEmpresaRequest>, SaveGnEmpresaRequestValidator>();
            services.AddScoped <IValidator<GnPerfilRequest>, GnPerfilRequestValidator>();
            services.AddScoped <IValidator<RegisterRequest>, RegisterRequestValidator>();
            services.AddScoped <IValidator<LoginRequestDTO>, LoginRequestValidator>();
            services.AddScoped<CreateProductosRequestValidator>();
            services.AddScoped<EditProductosRequestValidator>();
        }
    }
}
