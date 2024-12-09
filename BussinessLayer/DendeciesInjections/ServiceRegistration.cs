using BussinessLayer.Helpers.CargaMasivaHelpers;
using BussinessLayer.Interface.IOtros;
using BussinessLayer.Interfaces.IAutenticacion;
using BussinessLayer.Interfaces.ICargaMasiva;
using BussinessLayer.Interfaces.IOtros;
using BussinessLayer.Interfaces.Repositories;
using BussinessLayer.Repository.ROtros;
using BussinessLayer.Services;
using BussinessLayer.Services.SAutenticacion;
using BussinessLayer.Services.SCargaMasiva;
using BussinessLayer.Services.SNcfs;
using BussinessLayer.Services.SOtros;
using Microsoft.Extensions.DependencyInjection;
using BussinessLayer.Interfaces.ModuloInventario.Precios;
using BussinessLayer.Interfaces.ModuloInventario.Productos;
using BussinessLayer.Interfaces.ModuloInventario.Impuestos;
using BussinessLayer.Services.ModuloInventario.Suplidores;
using BussinessLayer.Services.ModuloInventario.Impuesto;
using BussinessLayer.Interfaces.ModuloInventario.Suplidores;
using BussinessLayer.Services.ModuloInventario.Productos;
using BussinessLayer.Interfaces.ModuloGeneral.Imagenes;
using BussinessLayer.Services.ModuloGeneral.Imagenes;
using BussinessLayer.Interfaces.ModuloInventario.Otros;
using BussinessLayer.Services.ModuloInventario.Otros;
using BussinessLayer.Interfaces.ModuloReporteria;
using BussinessLayer.Services.ModuloReportes;
using BussinessLayer.Services.ModuloHelpDesk;
using BussinessLayer.Services.ModuloAuditoria;
using BussinessLayer.Interfaces.ModuloAuditoria;
using BussinessLayer.Interfaces.ModuloFacturacion;
using BussinessLayer.Interfaces.ModuloHelpDesk;
using BussinessLayer.Interfaces.ModuloGeneral.Archivos;
using BussinessLayer.Services.ModuloGeneral.Archivos;
using BussinessLayer.Services.ModuloGeneral.Empresas;
using BussinessLayer.Services.ModuloGeneral.Modulo;
using BussinessLayer.Services.ModuloGeneral.Menu;
using BussinessLayer.Services.ModuloGeneral.Seguridad;
using BussinessLayer.Interfaces.ModuloGeneral.Seguridad;
using BussinessLayer.Interfaces.ModuloGeneral.Modulo;
using BussinessLayer.Interfaces.ModuloGeneral.Menu;
using BussinessLayer.Interfaces.ModuloGeneral.Empresas;
using BussinessLayer.Services.ModuloGeneral.Language.Translation;
using BussinessLayer.Interfaces.ModuloGeneral.Language;
using BussinessLayer.Interfaces.ModuloGeneral.Geografia;
using BussinessLayer.Services.ModuloGeneral.Geografia;
using BussinessLayer.Services.ModuloGeneral.Seguridad.IpWhois;
using BussinessLayer.Services.ModuloVentas.Bancos;
using BussinessLayer.Services.ModuloVentas.Boveda;
using BussinessLayer.Services.ModuloVentas.Caja;
using BussinessLayer.Services.ModuloVentas.Cotizaciones;
using BussinessLayer.Interfaces.ModuloVentas.IBancos;
using BussinessLayer.Interfaces.ModuloVentas.IBoveda;
using BussinessLayer.Interfaces.ModuloVentas.ICotizaciones;
using BussinessLayer.Interfaces.ModuloGeneral.Seguridad.IpWhois;
using BussinessLayer.Interfaces.ModuloVentas.ICaja;
using BussinessLayer.Services.ModuloVentas.Facturacion;
using BussinessLayer.Interfaces.ModuloGeneral.ParametrosGenerales;
using BussinessLayer.Services.ModuloGeneral.ParametrosGenerales;
using BussinessLayer.Interfaces.ModuloInventario.Almacen;
using BussinessLayer.Services.ModuloInventario.SAlmacen;


public static class ServiceRegistration
{
    public static void AddServiceRegistration(this IServiceCollection services)
    {

        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        services.AddScoped(typeof(IGenericService<,,>), typeof(GenericService<,,>));
        services.AddScoped<IRepositorySection, RepositorySection>();
        services.AddScoped<IGnUploadFileParametroService, GnUploadFileParametroService>();
        services.AddScoped<IGnTecnoAlmacenExternoService, GnTecnoAlmacenExternoService>();
        services.AddScoped<IRepReporteService, RepReporteService>();
        services.AddScoped<IRepReportesVariableService, RepReportesVariableService>();
        //services.AddScoped<IAlmacenesService, AlmacenesService>();
        //services.AddScoped<, >();
        // services.AddScoped<IAlmacenesService, AlmacenesService>();
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
        services.AddScoped<IDgiiNcfService, DgiiNcfService>();
        services.AddScoped<IFacturacionService, FacturacionService>();
        services.AddScoped<IMarcaService, MarcaService>();
        // services.AddScoped<IMovimientoAlmacenService, MovimientoAlmacenService>();
        services.AddScoped<IPedidoService, PedidoService>();
        services.AddScoped<IPrecioService, PrecioService>();
        services.AddScoped<IProductoService, ProductoService>();
        services.AddScoped<IGnEmpresaservice, GnEmpresaservice>();
        services.AddScoped<ISuplidoresService, SuplidoresService>();
        services.AddScoped<ITipoMovimientoService, TipoMovimientoService>();
        services.AddScoped<ITipoPagoService, TipoPagoService>();
        services.AddScoped<ITipoTransaccionService, TipoTransaccionService>();
        services.AddScoped<IVersionService, VersionService>();
        services.AddScoped<IAutenticacionService, AutenticacionService>();
        services.AddScoped<IClaimsService, ClaimsService>();
        services.AddScoped<ICargaMasivaService, CargaMasivaService>();
        services.AddScoped<IAperturaCierreCajasService, AperturaCierreCajasService>();
        services.AddScoped<IGnSucursalService, GnSucursalService>();
        services.AddScoped<ICajaService, CajaService>();
        services.AddScoped<ITipoMovimientoBancoService, TipoMovimientoBancoService>();
        services.AddScoped<ITipoIdentificacionService, TipoIdentificacionService>();
        services.AddScoped<ICiudades_X_PaisesService, Ciudades_X_PaisesService>();
        services.AddScoped<ISC_IPSYS001Service, SC_IPSYS001Service>();
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
        services.AddScoped<EntityMapper>();
        services.AddScoped<CsvProcessor>();
        services.AddScoped<IGnPerfilService, GnPerfilService>();
        services.AddScoped<IGnEmpresaservice, GnEmpresaservice>();
        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<IGnSucursalService, GnSucursalService>();
        services.AddScoped<INcfService, NcfService>();
        services.AddScoped<IUsuarioService, UsuarioService>();
        services.AddScoped<IGnPermisoService, GnPermisoService>();

        #region Geografia

        services.AddTransient<IPaisService, PaisService>();
        services.AddTransient<IMunicipioService, MunicipioService>();
        services.AddTransient<IRegionService, RegionService>();
        services.AddTransient<IProvinciaService, ProvinciaService>();

        #endregion

        #region Configuracion 
        services.AddTransient<IGnMenuService,GnMenuService>();
        services.AddTransient<IGnModuloService, GnModuloService>();
        services.AddTransient<IGnParametrosGeneralesService, GnParametrosGeneralesService>();
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

        #region Auditoria
        services.AddScoped<IAleAuditoriaService, AleAuditoriaService>();
        services.AddTransient<IAleLogsService, AleLogsService>();
        services.AddTransient<IAleLoginService, AleLoginService>();
        services.AddTransient<IAlePrintService, AlePrintService>();
        #endregion

        #region Geocalizacion 
        services.AddTransient<IIpGeolocalitationService, IpWhoisService>();
        #endregion

        #region Language
        services.AddScoped<ITranslationFieldService, TranslationFieldService>();
        services.AddScoped<IJsonTranslationService, JsonTranslationService>();
        #endregion

        #region Almacen
        services.AddTransient<IInvAlmacenesService, InvAlmacenesService>();
        services.AddTransient<IInvAlmacenInventarioService, InvAlmacenInventarioService>();
        services.AddTransient<IInvInventarioSucursalService, InvInventarioSucursalService>();
        services.AddTransient<IInvMovAlmacenSucursalService, InvMovAlmacenSucursalService>();
        services.AddTransient<IInvMovAlmacenSucursalDetalleService, InvMovAlmacenSucursalDetalleService>();
        services.AddTransient<IInvMovimientoAlmacenService, InvMovimientoAlmacenService>();
        services.AddTransient<IInvMovimientoAlmacenDetalleService, InvMovimientoAlmacenDetalleService>();
        services.AddTransient<IInvMovimientoSucursalDetalleService, InvMovimientoSucursalDetalleService>();
        services.AddTransient<IInvMovInventarioSucursalService, InvMovInventarioSucursalService>();
        #endregion
    }
}

