using BussinessLayer.FluentValidations.Empresas.BussinessLayer.FluentValidations.Empresas;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using BussinessLayer.DTOs.ModuloGeneral.Empresas;
using BussinessLayer.FluentValidations.ModuloInventario.Precios;
using BussinessLayer.FluentValidations.ModuloInventario.Productos;
using BussinessLayer.FluentValidations;
using BussinessLayer.DTOs.ModuloGeneral.Sucursal;
using BussinessLayer.FluentValidations.ModuloGeneral.Empresas;
using BussinessLayer.DTOs.HelpDesk;
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
using BussinessLayer.FluentValidations.Auditoria;
using BussinessLayer.DTOs.Auditoria;
using Azure.Core;
using BussinessLayer.FluentValidations.ModuloGeneral.Configuracion.Account;
using BussinessLayer.FluentValidations.ModuloGeneral.Configuracion.Seguridad;
using BussinessLayer.DTOs.ModuloGeneral.Configuracion.Account;
using BussinessLayer.DTOs.ModuloGeneral.Configuracion.Seguridad;
using BussinessLayer.DTOs.ModuloGeneral.Configuracion.Geografia.DMunicipio;
using BussinessLayer.DTOs.ModuloGeneral.Configuracion.Geografia.DPais;
using BussinessLayer.DTOs.ModuloGeneral.Configuracion.Geografia.DProvincia;
using BussinessLayer.DTOs.ModuloGeneral.Configuracion.Geografia.DRegion;
using BussinessLayer.DTOs.ModuloGeneral.Configuracion.Seguridad.Autenticacion;
using BussinessLayer.DTOs.ModuloGeneral.Configuracion.Seguridad.Permiso;
using BussinessLayer.DTOs.ModuloGeneral.Configuracion.Seguridad.Schedule;
using DataLayer.Models.ModuloGeneral;
using BussinessLayer.DTOs.ModuloGeneral.ParametroGenerales;
using BussinessLayer.FluentValidations.Configuracion.ParametrosGenerales;
using BussinessLayer.DTOs.ModuloGeneral.Monedas;
using BussinessLayer.FluentValidations.ModuloGeneral.Monedas;
using BussinessLayer.FluentValidations.ModuloHelpDesk;
using BussinessLayer.DTOs.ModuloGeneral.Imagenes;
using BussinessLayer.FluentValidations.ModuloGeneral.Imagenes;
using BussinessLayer.DTOs.ModuloInventario.Otros;
using BussinessLayer.FluentValidations.ModuloInventario.Otros;
using DataLayer.Models.Modulo_Citas;
using BussinessLayer.Validations.ModuloCitas.CtaAppointmentManagement;
using BussinessLayer.DTOs.ModuloCitas.CtaAppointmentManagement;
using BussinessLayer.DTOs.ModuloCitas.CtaAppointmentMovements;
using BussinessLayer.Validations.ModuloCitas.CtaAppointmentMovements;
using BussinessLayer.DTOs.ModuloCitas.CtaAppointmentReason;
using BussinessLayer.Validations.ModuloCitas.CtaAppointmentReason;
using BussinessLayer.Validations.ModuloCitas.CtaAppointments;
using BussinessLayer.DTOs.ModuloCitas.CtaCitaConfiguracion;
using BussinessLayer.Validations.ModuloCitas.CtaCitaConfiguracion;
using BussinessLayer.DTOs.ModuloCitas.CtaAppointments;
using BussinessLayer.DTOs.ModuloCitas.CtaEmailConfiguracion;
using BussinessLayer.Validations.ModuloCitas.CtaEmailConfiguracion;
using BussinessLayer.DTOs.ModuloCitas.CtaMeetingPlace;
using BussinessLayer.Validations.ModuloCitas.CtaMeetingPlace;
using BussinessLayer.DTOs.ModuloCitas.CtaSessionDetails;
using BussinessLayer.Validations.ModuloCitas.CtaSessionDetails;
using BussinessLayer.DTOs.ModuloCitas.CtaSessions;
using BussinessLayer.Validations.ModuloCitas.CtaSessions;
using BussinessLayer.DTOs.ModuloCitas.CtaState;
using BussinessLayer.Validations.ModuloCitas.CtaState;
using BussinessLayer.DTOs.ModuloCitas.CtaUnwanted;
using BussinessLayer.Validations.ModuloCitas.CtaUnwanted;
using BussinessLayer.FluentValidations.ModuloInventario;
using BussinessLayer.DTOs.ModuloCampaña.CmpCliente;
using BussinessLayer.FluentValidations.ModuloCampaña.CmpClientes;
using BussinessLayer.DTOs.ModuloCampaña.CmpContacto;
using BussinessLayer.FluentValidations.ModuloCampaña.CmpContactos;
using BussinessLayer.DTOs.ModuloCampaña.CmpEmail;
using BussinessLayer.FluentValidations.ModuloCampaña.CmpSmtpConfiguraciones;
using BussinessLayer.DTOs.ModuloCampaña.CmpServidores;
using BussinessLayer.DTOs.ModuloCampaña.CmpConfiguraciones;
using BussinessLayer.DTOs.ModuloCampaña.CmpTipoPlantillas;
using BussinessLayer.FluentValidations.ModuloCampaña.CmpTipoPlantilla;

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

            services.AddScoped<IValidator<CreateProductsDto>, CreateProductosRequestValidator>();
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

            services.AddScoped<IValidator<CmpClientCreateDto>, CmpClientCreateDtoValidation>();

            services.AddScoped<IValidator<EditInvProductoSuplidorDTO>, EditInvProductoSuplidorRequestValidator>();
            services.AddScoped<IValidator<CreateInvProductoSuplidorDTO>, CreateInvProductoSuplidorRequestValidator>();

            services.AddScoped<IValidator<CreateContactosSuplidoresDto>, CreateContactosSuplidoresRequestValidation>();
            services.AddScoped<IValidator<EditContactosSuplidoresDto>, EditContactosSuplidoresRequestValidation>();

            services.AddScoped<IValidator<CreateTipoMovimientoDto>, CreateTipoMovimientoRequestValidation>();
            services.AddScoped<IValidator<EditTipoMovimientoDto>, EditTipoMovimientoRequestValidation>();

            services.AddScoped<IValidator<CreateDetallePedidoDto>, CreateDetallePedidoRequestValidator>();
            services.AddScoped<IValidator<EditDetallePedidoDto>, EditDetallePedidoRequestValidator>();

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

            #region Campaña
            services.AddScoped<IValidator<CmpContactoCreateDto>, CmpContactoCreateValidator>();
            services.AddScoped<IValidator<CmpContactoUpdateDto>, CmpContactoUpdateValidator>();
            services.AddScoped<IValidator<CmpServidoresSmtpUpdateDto>, CmpServidoresSmtpUpdateValidator>();
            services.AddScoped<IValidator<CmpServidoresSmtpCreateDto>, CmpServidoresSmtpCreateValidator>();
            services.AddScoped<IValidator<CmpConfiguracionCreateDto>, CmpConfiguracionesSmtpCreateValidator>();
            services.AddScoped<IValidator<CmpTipoPlantillaCreateDto>, CmpTipoPlantillaCreateValidator>();
            #endregion
            services.AddScoped(typeof(IValidateService<>), typeof(ValidateService<>));


            #region Modulo General
            services.AddScoped<IValidator<GnScheduleRequest>, GnScheduleRequestValidator>();
            services.AddScoped<IValidator<GnScheduleUserRequest>, GnScheduleUserRequestValidator>();
            #endregion

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

            #region Modulo Citas

            services.AddScoped<IValidator<CtaAppointmentManagementRequest>, CtaAppointmentManagementRequestValidation>();
            services.AddScoped<IValidator<CtaAppointmentMovementsRequest>, CtaAppointmentMovementsRequestValidation>();
            services.AddScoped<IValidator<CtaAppointmentReasonRequest>, CtaAppointmentReasonRequestValidation>();
            services.AddScoped<IValidator<CtaAppointmentsRequest>, CtaAppointmentsRequestValidation>();
            services.AddScoped<IValidator<CtaCitaConfiguracionRequest>, CtaCitaConfiguracionRequestValidation>();
            services.AddScoped<IValidator<CtaEmailConfiguracionRequest>, CtaEmailConfiguracionRequestValidation>();
            services.AddScoped<IValidator<CtaMeetingPlaceRequest>, CtaMeetingPlaceRequestValidation>();
            services.AddScoped<IValidator<CtaSessionDetailsRequest>, CtaSessionDetailsRequestValidation>();
            services.AddScoped<IValidator<CtaSessionsRequest>, CtaSessionsRequestValidation>();
            services.AddScoped<IValidator<CtaStateRequest>, CtaStateRequestValidation>();
            services.AddScoped<IValidator<CtaUnwantedRequest>, CtaUnwantedRequestValidation>();

            #endregion
        }
    }
}
