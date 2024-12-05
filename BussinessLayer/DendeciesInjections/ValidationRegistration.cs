using BussinessLayer.FluentValidations.Empresas.BussinessLayer.FluentValidations.Empresas;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using BussinessLayer.DTOs.ModuloGeneral.Empresas;
using BussinessLayer.DTOs.Configuracion.Seguridad;
using BussinessLayer.DTOs.Configuracion.Seguridad.Autenticacion;
using BussinessLayer.DTOs.Configuracion.Account;
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
using BussinessLayer.FluentValidations.ModuloInventario.Marcas;
using BussinessLayer.FluentValidations.ModuloInventario.Versiones;
using BussinessLayer.FluentValidations.ModuloInventario.Impuestos;
using BussinessLayer.FluentValidations.ModuloInventario.Descuentos;
using BussinessLayer.FluentValidations.ModuloInventario.Suplidores;
using BussinessLayer.FluentValidations.ModuloInventario.Pedidos;
using BussinessLayer.DTOs.ModuloInventario.Productos;
using BussinessLayer.DTOs.ModuloInventario.Precios;
using BussinessLayer.DTOs.ModuloInventario.Descuentos;
using BussinessLayer.DTOs.ModuloInventario.Impuestos;
using BussinessLayer.DTOs.ModuloInventario.Marcas;
using BussinessLayer.DTOs.ModuloInventario.Pedidos;
using BussinessLayer.DTOs.ModuloInventario.Suplidores;
using BussinessLayer.DTOs.ModuloInventario.Versiones;
using BussinessLayer.FluentValidations.Auditoria;
using BussinessLayer.DTOs.Auditoria;
using Azure.Core;
using BussinessLayer.FluentValidations.ModuloGeneral.Configuracion.Account;
using BussinessLayer.FluentValidations.ModuloGeneral.Configuracion.Seguridad;
using DataLayer.Models.ModuloGeneral;
using BussinessLayer.DTOs.ModuloGeneral.ParametroGenerales;
using BussinessLayer.FluentValidations.Configuracion.ParametrosGenerales;

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

            services.AddScoped<IValidator<CreateProductsDto>,CreateProductosRequestValidator>();
            services.AddScoped<IValidator<EditProductDto>, EditProductosRequestValidator>();
            services.AddScoped<IValidator<CreatePreciosDto>, CreatePreciosRequestValidator>();
            services.AddScoped<IValidator<EditPricesDto>, EditPreciosRequestValidator>();
            services.AddScoped<IValidator<EditProductTypeDto>, EditProductsTypeRequestValidator>();
            services.AddScoped<IValidator<CreateTipoProductoDto>, CreateProductsTypeRequestValidator>();
            services.AddScoped<IValidator<CreateBrandDto>, CreateBrandRequestValidation>();
            services.AddScoped<IValidator<EditBrandDto>, EditBrandRequestValidation>();
            services.AddScoped<IValidator<EditVersionsDto>, EditVersionRequestValidation>();
            services.AddScoped<IValidator<CreateVersionsDto>, CreateVersionRequestValidation>();
            services.AddScoped<IValidator<EditTaxDto>, EditTaxRequestValidation>();
            services.AddScoped<IValidator<CreateTaxDto>, CreateTaxRequestValidation>();
            services.AddScoped<IValidator<EditDiscountDto>, EditDiscountRequestValidation>();
            services.AddScoped<IValidator<CreateDiscountDto>, CreateDiscountRequestValidation>();
            services.AddScoped<IValidator<EditSuppliersDto>, EditSuppliersRequestValidation>();
            services.AddScoped<IValidator<CreateSuppliersDto>, CreateSuppliersRequestValidation>();
            services.AddScoped<IValidator<EditOrderDto>, EditOrderRequestValidator>();
            services.AddScoped<IValidator<CreateOrderDto>, CreateOrderRequestValidator>();

            services.AddScoped<IValidator<GnSucursalRequest>, GnSucursalRequestValidator>();
            services.AddScoped<IValidator<GnPermisoRequest>, GnPermisoRequestValidator>();
            services.AddScoped<IValidator<CountryRequest>, CountryRequestValidator>();
            services.AddScoped<IValidator<RegionRequest>, RegionRequestValidator>();
            services.AddScoped<IValidator<ProvinceRequest>, ProvinceRequestValidator>();
            services.AddScoped<IValidator<MunicipioRequest>, MunicipalityRequestValidator>();
            services.AddScoped<IValidator<string>, StringsRequestValidator>();
            services.AddScoped<IValidator<long>, NumbersRequestValidator>();


            #region HelpDesk
            services.AddScoped<IValidator<HdkCategoryTicketRequest>, HdkCategoryTicketRequestValidator>();
            services.AddScoped<IValidator<HdkDepartamentsRequest>, HdkDepartamentsRequestValidator>();
            services.AddScoped<IValidator<HdkDepartXUsuarioRequest>, HdkDepartXUsuarioRequestValidator>();
            services.AddScoped<IValidator<HdkErrorSubCategoryRequest>, HdkErrorSubCategoryRequestValidator>();
            services.AddScoped<IValidator<HdkFileEvidenceTicketRequest>, HdkFileEvidenceTicketRequestValidator>();
            services.AddScoped<IValidator<HdkNoteTicketRequest>, HdkNoteTicketRequestValidator>();
            services.AddScoped<IValidator<HdkPrioridadTicketRequest>, HdkPrioridadTicketRequestValidator>();
            services.AddScoped<IValidator<HdkSolutionTicketRequest>, HdkSolutionTicketRequestValidator>();
            services.AddScoped<IValidator<HdkStatusTicketRequest>, HdkStatusTicketRequestValidator>();
            services.AddScoped<IValidator<HdkSubCategoryRequest>, HdkSubCategoryRequestValidator>();
            services.AddScoped<IValidator<HdkTicketsRequest>, HdkTicketsRequestValidator>();
            services.AddScoped<IValidator<HdkTypeTicketRequest>, HdkTypeTicketRequestValidator>();

            #endregion

            #region Auditoria
            services.AddScoped<IValidator<AleAuditoriaRequest>, AleAuditoriaRequestValidator>();
            services.AddScoped<IValidator<AleLoginRequest>, AleLoginRequestValidator>();
            services.AddScoped<IValidator<AleLogsRequest>, AleLogsRequestValidator>();
            services.AddScoped<IValidator<AlePrintRequest>, AlePrintRequestValidator>();
            #endregion

            #region Modulo General
            
                services.AddScoped<IValidator<GnParametrosGeneralesRequest>, GnParametrosGeneralesRequestValidator>();
            #endregion
        }
    }
}
