using BussinessLayer.Helpers.CargaMasivaHelpers;
using BussinessLayer.Helpers.CentroReporteriaHelpers;
using BussinessLayer.Interface.ICotizaciones;
using BussinessLayer.Interface.IFacturacion;
using BussinessLayer.Interface.IOtros;
using BussinessLayer.Interfaces.IAutenticacion;
using BussinessLayer.Interfaces.IBancos;
using BussinessLayer.Interfaces.IBoveda;
using BussinessLayer.Interfaces.ICaja;
using BussinessLayer.Interfaces.ICargaMasiva;
using BussinessLayer.Interfaces.ICentroReporteria;
using BussinessLayer.Interfaces.ICuentas;
using BussinessLayer.Interfaces.IEmpresa;
using BussinessLayer.Interfaces.IGeografia;
using BussinessLayer.Interfaces.IMenu;
using BussinessLayer.Interfaces.IOtros;
using BussinessLayer.Interfaces.ISeguridad;
using BussinessLayer.Interfaces.Repositories;
using BussinessLayer.Repository.ROtros;
using BussinessLayer.Services;
using BussinessLayer.Services.SAutenticacion;
using BussinessLayer.Services.SBancos;
using BussinessLayer.Services.SBoveda;
using BussinessLayer.Services.SCaja;
using BussinessLayer.Services.SCargaMasiva;
using BussinessLayer.Services.SCentroReporteria;
using BussinessLayer.Services.SCotizaciones;
using BussinessLayer.Services.SCuentas;
using BussinessLayer.Services.SEmpresa;
using BussinessLayer.Services.SFacturacion;
using BussinessLayer.Services.SGeografia;
using BussinessLayer.Services.SNcfs;
using BussinessLayer.Services.SMenu;
using BussinessLayer.Services.SOtros;
using BussinessLayer.Services.SSeguridad;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using BussinessLayer.Interfaces.IModulo;
using BussinessLayer.Services.SModulo;
using BussinessLayer.Interfaces.ModuloInventario.Precios;
using BussinessLayer.Interfaces.ModuloInventario.Productos;
using BussinessLayer.Interfaces.ModuloInventario.Impuestos;
using BussinessLayer.Services.ModuloInventario.Suplidores;
using BussinessLayer.Services.ModuloInventario.Impuesto;
using BussinessLayer.Interfaces.ModuloInventario.Suplidores;
using BussinessLayer.Interfaces.IHelpDesk;
using BussinessLayer.Services.SHelpDesk;
using BussinessLayer.Services.SSeguridad.Perfil;
using BussinessLayer.Services.SSeguridad.SUsuario;
using BussinessLayer.Services.SSeguridad.Permiso;
using BussinessLayer.Services.ModuloInventario.Productos;


public static class ServiceRegistration
{
    public static void AddServiceRegistration(this IServiceCollection services)
    {

        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        services.AddScoped(typeof(IGenericService<,,>), typeof(GenericService<,,>));
        services.AddScoped<IRepositorySection, RepositorySection>();
        services.AddScoped<IReporteriaService, ReporteriaService>();
        services.AddScoped<IAlmacenesService, AlmacenesService>();
        services.AddScoped<IClientesService, ClienteService>();
        services.AddScoped<IContactosSuplidoresService, ContactosSuplidoresService>();
        services.AddScoped<ICotizacionService, CotizacionService>();
        services.AddScoped<ICuentaPorPagarService, CuentasPorPagarService>();
        services.AddScoped<ICuentasPorCobrar, CuentaPorCobrarService>();
        services.AddScoped<IDescuentoService, DescuentoService>();
        services.AddScoped<IDetalleCotizacionService, DetalleCotizacionService>();
        services.AddScoped<IDetalleCuentaPorPagar, DetalleCuentaPorPagarService>();
        services.AddScoped<IDetalleCuentasPorCobrar, DetalleCuentaPorCobrarService>();
        services.AddScoped<IDetalleFacturacionService, DetalleFacturacionService>();
        services.AddScoped<IDetalleMovimientoAlmacenService, DetalleMovimientoAlmacenService>();
        services.AddScoped<IDgiiNcfService, DgiiNcfService>();
        services.AddScoped<IFacturacionService, FacturacionService>();
        services.AddScoped<IMarcaService, MarcaService>();
        services.AddScoped<IMovimientoAlmacenService, MovimientoAlmacenService>();
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
        services.AddScoped<ISC_USUAR001Service, SC_USUAR001Service>();
        services.AddScoped<IGnSucursalService, GnSucursalService>();
        services.AddScoped<ICajaService, CajaService>();
        services.AddScoped<ITipoMovimientoBancoService, TipoMovimientoBancoService>();
        services.AddScoped<ITipoIdentificacionService, TipoIdentificacionService>();
        services.AddScoped<ISC_PAIS001Service, SC_PAIS001Service>();
        services.AddScoped<ISC_REG001Service, SC_REG001Service>();
        services.AddScoped<ISC_PROV001Service, SC_PROV001Service>();
        services.AddScoped<ISC_MUNIC001Service, SC_MUNIC001Service>();
        services.AddScoped<IGn_PerfilService, Gn_PerfilService>();
        services.AddScoped<ICiudades_X_PaisesService, Ciudades_X_PaisesService>();
        services.AddScoped<ISC_IPSYS001Service, SC_IPSYS001Service>();
        services.AddScoped<IImpuestosService, ImpuestosService>();
        services.AddScoped<ISC_HORARIO001Service, SC_HORARIO001Service>();
        services.AddScoped<ISC_HORAGROUP002Service, SC_HORAGROUP002Service>();
        services.AddScoped<ISC_HORA_X_USR002Service, SC_HORA_X_USR002Service>();
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
        services.AddScoped<DeserializadorCrearReporte>();
        services.AddScoped<EntityMapper>();
        services.AddScoped<CsvProcessor>();
        services.AddScoped<IGnPerfilService, GnPerfilService>();
        services.AddScoped<IGnEmpresaservice, GnEmpresaservice>();
        services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        services.AddScoped<ITokenService, TokenService>();
        services.AddTransient<IGnSucursalService, GnSucursalService>();
        services.AddTransient<INcfService, NcfService>();
        services.AddTransient<IUsuarioService, UsuarioService>();
        services.AddTransient<IGnPermisoService, GnPermisoService>();

        #region Geografia

        services.AddTransient<IPaisService, PaisService>();
        services.AddTransient<IMunicipioService, MunicipioService>();
        services.AddTransient<IRegionService, RegionService>();
        services.AddTransient<IProvinciaService, ProvinciaService>();

        #endregion

        #region Configuracion 
        services.AddTransient<IGnMenuService,GnMenuService>();
        services.AddTransient<IGnModuloService, GnModuloService>();
        #endregion

        #region HelpDesk
        services.AddTransient<IHdkCategoryTicketService, HdkCategoryTicketService>();
        services.AddTransient<IHdkDepartamentsService, HdkDepartamentsService>();
        services.AddTransient<IHdkDepartXUsuarioService, HdkDepartXUsuarioService>();
        //services.AddTransient<IHdkErrorSubCategoryService, HdkErrorSubCategoryService>();
        //services.AddTransient<IHdkFileEvidenceTicketService, HdkFileEvidenceTicketService>();
        //services.AddTransient<IHdkNoteTicketService, HdkNoteTicketService>();
        //services.AddTransient<IHdkPrioridadTicketService, HdkPrioridadTicketService>();
        //services.AddTransient<IHdkSolutionTicketService, HdkSolutionTicketService>();
        //services.AddTransient<IHdkStatusTicketService, HdkStatusTicketService>();
        //services.AddTransient<IHdkSubCategoryService, HdkSubCategoryService>();
        //services.AddTransient<IHdkTicketsService, HdkTicketsService>();
        //services.AddTransient<IHdkTypeTicketService, HdkTypeTicketService>();
        #endregion

    }
}

