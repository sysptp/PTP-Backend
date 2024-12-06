using BussinessLayer.FluentValidations.Empresas.BussinessLayer.FluentValidations.Empresas;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using BussinessLayer.DTOs.ModuloGeneral.Empresas;
using BussinessLayer.FluentValidations.ModuloInventario.Precios;
using BussinessLayer.FluentValidations.ModuloInventario.Productos;
using BussinessLayer.FluentValidations;
using BussinessLayer.DTOs.ModuloGeneral.Sucursal;
using BussinessLayer.FluentValidations.ModuloGeneral.Empresas;
using BussinessLayer.FluentValidations.ModuloGeneral.Geografia;
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
using BussinessLayer.DTOs.ModuloGeneral.ParametroGenerales;
using BussinessLayer.FluentValidations.Configuracion.ParametrosGenerales;
using BussinessLayer.DTOs.ModuloGeneral.Monedas;
using BussinessLayer.FluentValidations.ModuloGeneral.Monedas;
using BussinessLayer.FluentValidations.ModuloHelpDesk;
using BussinessLayer.DTOs.ModuloGeneral.Imagenes;
using BussinessLayer.FluentValidations.ModuloGeneral.Imagenes;
using BussinessLayer.DTOs.ModuloInventario.Otros;
using BussinessLayer.FluentValidations.ModuloInventario.Otros;
using BussinessLayer.DTOs.ModuloReporteria;
using BussinessLayer.FluentValidations.ModuloReporteria;
using BussinessLayer.FluentValidations.ModuloAuditoria;
using BussinessLayer.DTOs.ModuloAuditoria;
using BussinessLayer.DTOs.ModuloHelpDesk;
using BussinessLayer.DTOs.ModuloGeneral.Archivos;
using BussinessLayer.FluentValidations.ModuloGeneral.Archivo;
using BussinessLayer.FluentValidations.ModuloGeneral.ModuloReporteria;
using BussinessLayer.DTOs.ModuloGeneral.Geografia.DMunicipio;
using BussinessLayer.DTOs.ModuloGeneral.Geografia.DPais;
using BussinessLayer.DTOs.ModuloGeneral.Geografia.DProvincia;
using BussinessLayer.DTOs.ModuloGeneral.Geografia.DRegion;
using BussinessLayer.DTOs.ModuloGeneral.Seguridad.Autenticacion;
using BussinessLayer.DTOs.ModuloGeneral.Seguridad.Perfil;
using BussinessLayer.DTOs.ModuloGeneral.Seguridad.Permiso;
using BussinessLayer.DTOs.Account;
using BussinessLayer.FluentValidations.Account;

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
            services.AddScoped<IValidator<CreateCurrencyDTO>, CreateCurrencyRequestValidator>();
            services.AddScoped<IValidator<EditCurrencyDTO>, EditCurrencyRequestValidator>();

            services.AddScoped<IValidator<EditInvProductoSuplidorDTO>, EditInvProductoSuplidorRequestValidator>();
            services.AddScoped<IValidator<CreateInvProductoSuplidorDTO>, CreateInvProductoSuplidorRequestValidator>();

            services.AddScoped<IValidator<CreateContactosSuplidoresDto>, CreateContactosSuplidoresRequestValidation>();
            services.AddScoped<IValidator<EditContactosSuplidoresDto>, EditContactosSuplidoresRequestValidation>();

            services.AddScoped<IValidator<CreateTipoMovimientoDto>, CreateTipoMovimientoRequestValidation>();
            services.AddScoped<IValidator<EditTipoMovimientoDto>, EditTipoMovimientoRequestValidation>();

            services.AddScoped<IValidator<CreateDetallePedidoDto>, CreateDetallePedidoRequestValidator>();
            services.AddScoped<IValidator<EditDetallePedidoDto>, EditDetallePedidoRequestValidator>();

            services.AddScoped<IValidator<CreateRepReporteDto>, CreateRepReporteDtoValidator>();
            services.AddScoped<IValidator<EditRepReporteDto>, EditRepReporteDtoValidator>();

            services.AddScoped<IValidator<CreateRepReportesVariableDto>, CreateRepReportesVariableRequestValidator>();
            services.AddScoped<IValidator<EditRepReportesVariableDto>, EditRepReportesVariableRequestValidator>();

            services.AddScoped<IValidator<CreateGnUploadFileParametroDto>, CreateGnUploadFileParametroRequestValidator>();
            services.AddScoped<IValidator<EditGnUploadFileParametroDto>, EditGnUploadFileParametroRequestValidator>();

            services.AddScoped<IValidator<CreateGnTecnoAlmacenExternoDto>, CreateGnTecnoAlmacenExternoDtoValidator>();
            services.AddScoped<IValidator<EditGnTecnoAlmacenExternoDto>, EditGnTecnoAlmacenExternoDtoValidator>();


            services.AddScoped<IValidator<AddImageProductDTO>, AddImageRequestValidator>();

            services.AddScoped<IValidator<GnSucursalRequest>, GnSucursalRequestValidator>();
            services.AddScoped<IValidator<GnPermisoRequest>, GnPermisoRequestValidator>();
            services.AddScoped<IValidator<CountryRequest>, CountryRequestValidator>();
            services.AddScoped<IValidator<RegionRequest>, RegionRequestValidator>();
            services.AddScoped<IValidator<ProvinceRequest>, ProvinceRequestValidator>();
            services.AddScoped<IValidator<MunicipioRequest>, MunicipalityRequestValidator>();
            services.AddScoped<IValidator<string>, StringsRequestValidator>();
            services.AddScoped<IValidator<long>, NumbersRequestValidator>();
            services.AddScoped<IValidator<decimal>, DecimalsRequestValidator>();



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
