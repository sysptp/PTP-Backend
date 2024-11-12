using BussinessLayer.FluentValidations.Empresas.BussinessLayer.FluentValidations.Empresas;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using BussinessLayer.DTOs.ModuloGeneral.Empresas;
using BussinessLayer.DTOs.Configuracion.Seguridad;
using BussinessLayer.DTOs.Configuracion.Seguridad.Autenticacion;
using BussinessLayer.DTOs.Configuracion.Account;
using BussinessLayer.FluentValidations.Configuracion.Seguridad;
using BussinessLayer.FluentValidations.Configuracion.Account;
using BussinessLayer.FluentValidations.ModuloInventario.Precios;
using BussinessLayer.FluentValidations.ModuloInventario.Productos;
using BussinessLayer.DTOs.ModuloGeneral.Sucursal;
using BussinessLayer.FluentValidations.ModuloGeneral.Empresas;
using BussinessLayer.FluentValidations.HelpDesk;
using BussinessLayer.DTOs.HelpDesk;

namespace BussinessLayer.DendeciesInjections
{
    public static class ValidationRegistration
    {
        public static void AddValidationInjections(this IServiceCollection services)
        {
            services.AddScoped<RegisterRequestValidator>();
            services.AddTransient<IValidator<GnEmpresaRequest>, SaveGnEmpresaRequestValidator>();
            services.AddScoped <IValidator<GnPerfilRequest>, GnPerfilRequestValidator>();
            services.AddScoped <IValidator<RegisterRequest>, RegisterRequestValidator>();
            services.AddScoped <IValidator<LoginRequestDTO>, LoginRequestValidator>();
            services.AddScoped<CreateProductosRequestValidator>();
            services.AddScoped<EditProductosRequestValidator>();
            services.AddScoped<CreatePreciosRequestValidator>();
            services.AddScoped<EditPreciosRequestValidator>();
            services.AddScoped<IValidator<GnSucursalRequest>, GnSucursalRequestValidator>();
            services.AddScoped<IValidator<HdkCategoryTicketRequest>, HdkCategoryTicketRequestValidator>();
            
        }
    }
}
