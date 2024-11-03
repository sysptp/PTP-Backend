using BussinessLayer.Helpers.CargaMasivaHelpers;
using BussinessLayer.Helpers.CentroReporteriaHelpers;
using BussinessLayer.Interface.IAccount;
using BussinessLayer.Interface.IAlmacenes;
using BussinessLayer.Interface.ICotizaciones;
using BussinessLayer.Interface.IFacturacion;
using BussinessLayer.Interface.IOtros;
using BussinessLayer.Interface.IPedido;
using BussinessLayer.Interface.IProductos;
using BussinessLayer.Interface.ISuplidores;
using BussinessLayer.Interfaces.IAutenticacion;
using BussinessLayer.Interfaces.IBancos;
using BussinessLayer.Interfaces.IBoveda;
using BussinessLayer.Interfaces.ICaja;
using BussinessLayer.Interfaces.ICargaMasiva;
using BussinessLayer.Interfaces.ICentroReporteria;
using BussinessLayer.Interfaces.ICuentas;
using BussinessLayer.Interfaces.IEmpresa;
using BussinessLayer.Interfaces.IGeografia;
using BussinessLayer.Interfaces.IOtros;
using BussinessLayer.Interfaces.ISeguridad;
using BussinessLayer.Interfaces.Repositories;
using BussinessLayer.Repository.ROtros;
using BussinessLayer.Services;
using BussinessLayer.Services.SALmacenes;
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
using BussinessLayer.Services.SOtros;
using BussinessLayer.Services.SPedidos;
using BussinessLayer.Services.SProductos;
using BussinessLayer.Services.SSeguridad;
using BussinessLayer.Services.SSuplidores;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System.Runtime.InteropServices;

namespace BussinessLayer.DendeciesInjections
{
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
            services.AddScoped<IEnvaseService, EnvaseService>();
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
            services.AddScoped<ISC_IMP001Service, SC_IMP001Service>();
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
            services.AddScoped<DeserializadorCrearReporte>();
            services.AddScoped<EntityMapper>();
            services.AddScoped<CsvProcessor>();
            services.AddScoped<IGnPerfilService, GnPerfilService>();
            services.AddScoped<IGnEmpresaservice, GnEmpresaservice>(); 
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddTransient<IGnSucursalService,  GnSucursalService>();

            #region Geografia

            services.AddTransient<IPaisService, PaisService>();
            services.AddTransient<IMunicipioService, MunicipioService>();
            services.AddTransient<IRegionService, RegionService>();
            services.AddTransient<IProvinciaService, ProvinciaService>();

            #endregion
        }
    }
}
