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
using BussinessLayer.FluentValidations;
using BussinessLayer.DTOs.ModuloGeneral.Sucursal;
using BussinessLayer.FluentValidations.ModuloGeneral.Empresas;
using BussinessLayer.FluentValidations.HelpDesk;
using BussinessLayer.DTOs.HelpDesk;
using BussinessLayer.DTOs.Configuracion.Seguridad.Permiso;
using BussinessLayer.DTOs.Configuracion.Geografia.DPais;
using BussinessLayer.FluentValidations.ModuloGeneral.Geografia;
using BussinessLayer.DTOs.Configuracion.Geografia.DRegion;
using BussinessLayer.DTOs.Configuracion.Geografia.DProvincia;
using BussinessLayer.DTOs.Configuracion.Geografia.DMunicipio;

namespace BussinessLayer.DendeciesInjections
{
    public static class ValidationRegistration
    {
        public static void AddValidationInjections(this IServiceCollection services)
        {
            services.AddTransient<IValidator<GnEmpresaRequest>, SaveGnEmpresaRequestValidator>();
            services.AddScoped<IValidator<GnPerfilRequest>, GnPerfilRequestValidator>();
            services.AddScoped<IValidator<RegisterRequest>, RegisterRequestValidator>();
            services.AddScoped<IValidator<LoginRequestDTO>, LoginRequestValidator>();
            services.AddScoped<RegisterRequestValidator>();
            services.AddScoped<CreateProductosRequestValidator>();
            services.AddScoped<EditProductosRequestValidator>();
            services.AddScoped<CreatePreciosRequestValidator>();
            services.AddScoped<EditPreciosRequestValidator>();
            services.AddScoped<NumbersRequestValidator>();
            services.AddScoped<StringsRequestValidator>();
            services.AddScoped<EditProductsTypeRequestValidator>();
            services.AddScoped<CreateProductsTypeRequestValidator>();
            services.AddScoped<IValidator<GnSucursalRequest>, GnSucursalRequestValidator>();
            services.AddScoped<IValidator<GnPermisoRequest>, GnPermisoRequestValidator>();
            services.AddScoped<IValidator<CountryRequest>, CountryRequestValidator>();
            services.AddScoped<IValidator<RegionRequest>, RegionRequestValidator>();
            services.AddScoped<IValidator<ProvinceRequest>, ProvinceRequestValidator>();
            services.AddScoped<IValidator<MunicipioRequest>, MunicipalityRequestValidator>();
            services.AddScoped<IValidator<string>, StringsRequestValidator>();
            services.AddScoped<IValidator<long>, NumbersRequestValidator>();


            services.AddScoped<IValidator<HdkCategoryTicketRequest>, HdkCategoryTicketRequestValidator>();
            
        }
    }
}
