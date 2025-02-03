using BussinessLayer.Helpers.CargaMasivaHelpers;
using BussinessLayer.Interfaces.Repositories;
using BussinessLayer.Repository.ROtros;
using BussinessLayer.Services;
using BussinessLayer.Services.SAutenticacion;
using BussinessLayer.Services.SCargaMasiva;
using BussinessLayer.Services.SCuentas;
using BussinessLayer.Services.SNcfs;
using BussinessLayer.Services.SOtros;
using Microsoft.Extensions.DependencyInjection;
using BussinessLayer.Services.ModuloInventario.Suplidores;
using BussinessLayer.Services.ModuloInventario.Impuesto;
using BussinessLayer.Services.ModuloInventario.Productos;
using BussinessLayer.Services.ModuloGeneral.Imagenes;
using BussinessLayer.Services.ModuloInventario.Otros;
using BussinessLayer.Services.ModuloReportes;
using BussinessLayer.Services.ModuloHelpDesk;
using BussinessLayer.Services.ModuloAuditoria;
using BussinessLayer.Services.ModuloGeneral.Archivos;
using BussinessLayer.Services.ModuloGeneral.Empresas;
using BussinessLayer.Services.ModuloGeneral.Modulo;
using BussinessLayer.Services.ModuloGeneral.Menu;
using BussinessLayer.Services.ModuloGeneral.Seguridad;
using BussinessLayer.Services.ModuloGeneral.Language.Translation;
using BussinessLayer.Services.ModuloGeneral.Geografia;
using BussinessLayer.Services.ModuloGeneral.Seguridad.IpWhois;
using BussinessLayer.Services.ModuloVentas.Bancos;
using BussinessLayer.Services.ModuloVentas.Boveda;
using BussinessLayer.Services.ModuloVentas.Caja;
using BussinessLayer.Services.ModuloVentas.Cotizaciones;
using BussinessLayer.Services.ModuloVentas.Facturacion;
using BussinessLayer.Services.SModuloGeneral.SParametrosGenerales;
using BussinessLayer.Services.SContactos;
using BussinessLayer.Services.SModuloCampaña;
using DataLayer.Models.Modulo_Citas;
using BussinessLayer.Interfaces.Services.IAutenticacion;
using BussinessLayer.Interfaces.Services.ICargaMasiva;
using BussinessLayer.Interfaces.Services.ICuentas;
using BussinessLayer.Interfaces.Services.IModuloGeneral.IParametrosGenerales;
using BussinessLayer.Interfaces.Services.IOtros;
using BussinessLayer.Interfaces.Services.ModuloAuditoria;
using BussinessLayer.Interfaces.Services.ModuloCitas;
using BussinessLayer.Interfaces.Services.ModuloFacturacion;
using BussinessLayer.Interfaces.Services.ModuloGeneral.Archivos;
using BussinessLayer.Interfaces.Services.ModuloGeneral.Empresas;
using BussinessLayer.Interfaces.Services.ModuloGeneral.Geografia;
using BussinessLayer.Interfaces.Services.ModuloGeneral.Imagenes;
using BussinessLayer.Interfaces.Services.ModuloGeneral.Language;
using BussinessLayer.Interfaces.Services.ModuloGeneral.Menu;
using BussinessLayer.Interfaces.Services.ModuloGeneral.Modulo;
using BussinessLayer.Interfaces.Services.ModuloGeneral.Seguridad;
using BussinessLayer.Interfaces.Services.ModuloGeneral.Seguridad.IpWhois;
using BussinessLayer.Interfaces.Services.ModuloHelpDesk;
using BussinessLayer.Interfaces.Services.ModuloInventario.Impuestos;
using BussinessLayer.Interfaces.Services.ModuloInventario.Otros;
using BussinessLayer.Interfaces.Services.ModuloInventario.Precios;
using BussinessLayer.Interfaces.Services.ModuloInventario.Productos;
using BussinessLayer.Interfaces.Services.ModuloInventario.Suplidores;
using BussinessLayer.Interfaces.Services.ModuloReporteria;
using BussinessLayer.Interfaces.Services.ModuloVentas.IBancos;
using BussinessLayer.Interfaces.Services.ModuloVentas.IBoveda;
using BussinessLayer.Interfaces.Services.ModuloVentas.ICaja;
using BussinessLayer.Interfaces.Services.ModuloVentas.ICotizaciones;
using BussinessLayer.Interfaces.ModuloInventario.Almacen;
using BussinessLayer.Services.ModuloInventario.SAlmacen;
using BussinessLayer.Interfaces.ModuloCampaña.Services;
using BussinessLayer.FluentValidations.Generic;
using BussinessLayer.Services.ModuloCitas;
using BussinessLayer.Interfaces.Services.ModuloGeneral;
using BussinessLayer.Services.ModuloGeneral;
using BussinessLayer.Interfaces.Repository.ModuloGeneral.Email;
using BussinessLayer.Services.ModuloGeneral.Email;

public static class ServiceRegistration
{
    public static void AddServiceRegistration(this IServiceCollection services)
    {
        

        #region Modulo Inventario
        services.AddScoped<IInvAlmacenesService, InvAlmacenesService>();
        #region General
        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        services.AddScoped(typeof(IGenericService<,,>), typeof(GenericService<,,>));
        services.AddScoped<IRepositorySection, RepositorySection>();
        //services.AddScoped<, >();
        //services.AddScoped<IAlmacenesService, AlmacenesService>();
        services.AddScoped<IContactosSuplidoresService, ContactosSuplidoresService>();
        services.AddScoped<ICotizacionService, CotizacionService>();
       // services.AddScoped<ICuentaPorPagarService, CuentasPorPagarService>();
        services.AddScoped<ICuentasPorCobrar, CuentaPorCobrarService>();
        services.AddScoped<IDescuentoService, DescuentoService>();
        services.AddScoped<IDetalleCotizacionService, DetalleCotizacionService>();
        services.AddScoped<IDetalleCuentaPorPagar, DetalleCuentaPorPagarService>();
        services.AddScoped<IDetalleCuentasPorCobrar, DetalleCuentaPorCobrarService>();
        services.AddScoped<IDetalleFacturacionService, DetalleFacturacionService>();
//services.AddScoped<IDetalleMovimientoAlmacenService, DetalleMovimientoAlmacenService>();
        services.AddScoped<IInvProductoImpuestoService, InvProductoImpuestoService>();
        //services.AddScoped<IMovimientoAlmacenService, MovimientoAlmacenService>();
        services.AddScoped<IPedidoService, PedidoService>();
        services.AddScoped<IPrecioService, PrecioService>();
        services.AddScoped<IProductoService, ProductoService>();
         services.AddScoped<ITipoMovimientoService, TipoMovimientoService>();
        services.AddScoped<ITipoPagoService, TipoPagoService>();
        services.AddScoped<ITipoTransaccionService, TipoTransaccionService>();
        services.AddScoped<IVersionService, VersionService>();
       services.AddScoped<IAperturaCierreCajasService, AperturaCierreCajasService>();
        services.AddScoped<ICajaService, CajaService>();
        services.AddScoped<ITipoMovimientoBancoService, TipoMovimientoBancoService>();
        services.AddScoped<ITipoIdentificacionService, TipoIdentificacionService>();
       services.AddScoped<IImpuestosService, ImpuestosService>();
        services.AddScoped<IMovimientoBancoesService, MovimientoBancoesService>();
        services.AddScoped<IMonedasService, MonedasService>();
        services.AddScoped<ICuentaBancosService, CuentaBancosService>();
        services.AddScoped<IConciliacionTCTFsService, ConciliacionTCTFsService>();
        services.AddScoped<IBovedaMovimientoesService, BovedaMovimientoesService>();
        services.AddScoped<IBovedaCajasService, BovedaCajasService>();
        services.AddScoped<IBovedaCajaDesglosesService, BovedaCajaDesglosesService>();
        services.AddScoped<IBilletes_MonedaService, Billetes_MonedaService>();
        services.AddScoped<IBancosService, BancosService>();
        services.AddScoped<ITipoProductoService, TipoProductoService>();
        services.AddScoped<IImagenesService, ImagenesService>();
        services.AddScoped<IInvProductoSuplidorService, InvProductoSuplidorService>();
        services.AddScoped<IInvAlmacenInventarioService, InvAlmacenInventarioService>();
        services.AddScoped<IInvMovInventarioSucursalService, InvMovInventarioSucursalService>();
        services.AddScoped<IInvInventarioSucursalService, InvInventarioSucursalService>();
        services.AddScoped<IInvMovAlmacenSucursalService, InvMovAlmacenSucursalService>();
        services.AddScoped<IInvMovAlmacenSucursalDetalleService, InvMovAlmacenSucursalDetalleService>();
        services.AddScoped<IInvMovimientoAlmacenService, InvMovimientoAlmacenService>();
        services.AddScoped<IInvMovimientoAlmacenDetalleService, InvMovimientoAlmacenDetalleService>();
        services.AddScoped<IInvMovimientoSucursalDetalleService, InvMovimientoSucursalDetalleService>();
        #endregion

        #region Auditoria
        services.AddScoped<IAleBitacoraService, AleBitacoraService>();
        services.AddTransient<IAleLogsService, AleLogsService>();
        services.AddTransient<IAleLoginService, AleLoginService>();
        services.AddTransient<IAlePrintService, AlePrintService>();
        services.AddTransient<IAleAuditLogService, AleAuditLogService>();
        services.AddTransient<IAleAuditTableControlService, AleAuditTableControlService>();
        #endregion

        #region Modulo de Citas

        services.AddScoped<ICtaAppointmentManagementService, CtaAppointmentManagementService>();
        services.AddScoped<ICtaAppointmentMovementsService, CtaAppointmentMovementsService>();
        services.AddScoped<ICtaAppointmentReasonService, CtaAppointmentReasonService>();
        services.AddScoped<ICtaAppointmentsService, CtaAppointmentsService>();
        services.AddScoped<ICtaConfiguracionService, CtaConfiguracionService>();
        services.AddScoped<ICtaEmailConfiguracionService, CtaEmailConfiguracionService>();
        services.AddScoped<ICtaMeetingPlaceService, CtaMeetingPlaceService>();
        services.AddScoped<ICtaSessionDetailsService, CtaSessionDetailsService>();
        services.AddScoped<ICtaSessionsService, CtaSessionsService>();
        services.AddScoped<ICtaStateService, CtaStateService>();
        services.AddScoped<ICtaUnwantedService, CtaUnwantedService>();
        services.AddScoped<ICtaAppointmentSequenceService, CtaAppointmentSequenceService>();
        services.AddScoped<ICtaAppointmentAreaService, CtaAppointmentAreaService>();
        services.AddScoped<ICtaAreaXUserService, CtaAreaXUserService>();
        services.AddScoped<ICtaContactTypeService, CtaContactTypeService>();
        services.AddScoped<ICtaAppointmentContactsService, CtaAppointmentContactsService>();
        services.AddScoped<ICtaAppointmentUsersService, CtaAppointmentUsersService>();

        #endregion
        #region Modulo Contabilidad
        #endregion

        #region Modulo General

        services.AddTransient<IGnScheduleService, GnScheduleService>();
        services.AddTransient<IGnScheduleUserService, GnScheduleUserService>();

        #region Geocalizacion 
        services.AddTransient<IIpGeolocalitationService, IpWhoisService>();
        #endregion

        #region Language
        services.AddScoped<ITranslationFieldService, TranslationFieldService>();
        services.AddScoped<IJsonTranslationService, JsonTranslationService>();
        #endregion
        services.AddScoped<EntityMapper>();
        services.AddScoped<CsvProcessor>();
        services.AddScoped<IGnPerfilService, GnPerfilService>();

        services.AddScoped<IGnEmpresaservice, GnEmpresaservice>();
        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<IGnSucursalService, GnSucursalService>();

        services.AddScoped<INcfService, NcfService>();
        services.AddScoped<IUsuarioService, UsuarioService>();
        services.AddScoped<IGnPermisoService, GnPermisoService>();

        //services.AddScoped<IClientService, ClientService>();
        services.AddScoped<IContactService, ContactService>();
        #endregion

        #region FluentValidation
        services.AddScoped<IGenericValidation,GenericValidation>();
        #endregion
        
        #region ModuloCampaña
        services.AddScoped<ICmpClientService, CmpClientService>();
        services.AddScoped<ICmpServidoresSmtpService, CmpServidoresSmtpService>();
        services.AddScoped<ICmpPlantillaService, CmpPlantillaService>();
        services.AddScoped<ICmpEmailService, CmpEmailService>();
        services.AddScoped<ICmpCampanaService, CmpCampanaService>();
        services.AddScoped<ICmpCampanaDetalleService, CmpCampanaDetalleService>();
        #endregion

        #region Geografia

        services.AddTransient<IPaisService, PaisService>();
        services.AddTransient<IMunicipioService, MunicipioService>();
        services.AddTransient<IRegionService, RegionService>();
        services.AddTransient<IProvinciaService, ProvinciaService>();

        #endregion

        #region Configuracion 
        services.AddTransient<IGnMenuService, GnMenuService>();
        services.AddTransient<IGnModuloService, GnModuloService>();
        services.AddTransient<IGnParametrosGeneralesService, GnParametrosGeneralesService>();
        #endregion

        #endregion

        #region HelpDesk
        services.AddTransient<IHdkCategoryTicketService, HdkCategoryTicketService>();
        services.AddTransient<IHdkDepartamentsService, HdkDepartamentsService>();
        services.AddTransient<IHdkDepartXUsuarioService, HdkDepartXUsuarioService>();
        services.AddTransient<IHdkErrorSubCategoryService, HdkErrorSubCategoryService>();
        services.AddTransient<IHdkFileEvidenceTicketService, HdkFileEvidenceTicketService>();
        services.AddTransient<IHdkNoteTicketService, HdkNoteTicketService>();
        services.AddTransient<IHdkPrioridadTicketService, HdkPrioridadTicketService>();
        services.AddTransient<IHdkSolutionTicketService, HdkSolutionTicketService>();
        services.AddTransient<IHdkStatusTicketService, HdkStatusTicketService>();
        services.AddTransient<IHdkSubCategoryService, HdkSubCategoryService>();
        services.AddTransient<IHdkTicketsService, HdkTicketsService>();
        services.AddTransient<IHdkTypeTicketService, HdkTypeTicketService>();
        #endregion

        #region Modulo Inventario
        #endregion

        #region Modulo Reporteria
        #endregion

        #region Modulo RRHH
        #endregion

        #region Modulo Ventas
        #endregion

        #region NCFs
        #endregion

        #region Modulo General

        services.AddScoped<IGnRepeatUnitService,GnRepeatUnitService>();
        services.AddScoped<IGnEmailGenericSerivce,GnEmailService>();
        #endregion 

        #region Otros
        #endregion

        #region De momento sin Modulo
        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        services.AddScoped(typeof(IGenericService<,,>), typeof(GenericService<,,>));
        services.AddScoped<IRepositorySection, RepositorySection>();
        services.AddScoped<IGnUploadFileParametroService, GnUploadFileParametroService>();
        services.AddScoped<IGnTecnoAlmacenExternoService, GnTecnoAlmacenExternoService>();
        services.AddScoped<IRepReporteService, RepReporteService>();
        services.AddScoped<IRepReportesVariableService, RepReportesVariableService>();
        services.AddScoped<IDgiiNcfService, DgiiNcfService>();
        services.AddScoped<IFacturacionService, FacturacionService>();
        services.AddScoped<IMarcaService, MarcaService>();
        services.AddScoped<IGnEmpresaservice, GnEmpresaservice>();
        services.AddScoped<ISuplidoresService, SuplidoresService>();
        services.AddScoped<IClaimsService, ClaimsService>();
        services.AddScoped<ICargaMasivaService, CargaMasivaService>();
        services.AddScoped<IGnSucursalService, GnSucursalService>();
        services.AddScoped<ITipoIdentificacionService, TipoIdentificacionService>();
        services.AddScoped<ICiudades_X_PaisesService, Ciudades_X_PaisesService>();
        services.AddScoped<ISC_IPSYS001Service, SC_IPSYS001Service>();
        services.AddScoped<ICuentaBancosService, CuentaBancosService>();
        services.AddScoped<ICuentaBancosService, CuentaBancosService>();
        services.AddScoped<ICuentaBancosService, CuentaBancosService>();
        services.AddScoped<ICuentaBancosService, CuentaBancosService>();
        services.AddScoped<ICuentaBancosService, CuentaBancosService>();
        services.AddScoped<IBovedaCajasService, BovedaCajasService>();
        services.AddScoped<IBovedaCajaDesglosesService, BovedaCajaDesglosesService>();
        services.AddScoped<IBilletes_MonedaService, Billetes_MonedaService>();
        services.AddScoped<IBancosService, BancosService>();
        services.AddScoped<EntityMapper>();
        services.AddScoped<CsvProcessor>();
        services.AddScoped<IGnPerfilService, GnPerfilService>();
        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<INcfService, NcfService>();
        services.AddScoped<IUsuarioService, UsuarioService>();
        services.AddScoped<IGnPermisoService, GnPermisoService>();
        #endregion
    }
}

