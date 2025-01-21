using DataLayer.Models.Otros;
using DataLayer.Models.Cuentas;
using Microsoft.EntityFrameworkCore;
using DataLayer.Models.ModuloInventario.Productos;
using DataLayer.Models.ModuloInventario.Precios;
using DataLayer.Models.ModuloInventario.Pedidos;
using DataLayer.Models.ModuloInventario.Marcas;
using DataLayer.Models.ModuloInventario.Impuesto;
using DataLayer.Models.ModuloInventario.Descuento;
using DataLayer.Models.ModuloInventario.Almacen;
using DataLayer.Models.ModuloGeneral.Language;
using DataLayer.Models.ModuloGeneral.Monedas;
using DataLayer.Models.ModuloGeneral.Imagen;
using DataLayer.Models.ModuloInventario.Otros;
using DataLayer.Models.ModuloReporteria;
using DataLayer.Models.ModuloHelpDesk;
using DataLayer.Models.ModuloAuditoria;
using DataLayer.Models.ModuloGeneral.Archivos;
using DataLayer.Models.ModuloGeneral.Seguridad;
using DataLayer.Models.ModuloGeneral.Empresa;
using DataLayer.Models.ModuloGeneral.Menu;
using DataLayer.Models.ModuloGeneral.Geografia;
using DataLayer.Models.ModuloVentas.Caja;
using DataLayer.Models.ModuloVentas.Boveda;
using DataLayer.Models.ModuloVentas.Bancos;
using DataLayer.Models.ModuloVentas.Cotizaciones;
using DataLayer.Models.Modulo_Citas;
using DataLayer.Models.ModuloCitas;

namespace DataLayer.PDbContex
{
    public class PDbContext : DbContext
    {
        public PDbContext()
        {

        }


        public PDbContext(DbContextOptions<PDbContext> options) : base(options)
        {

        }
       

        #region Reporteria


        public DbSet<RepReporte> RepReportes { get; set; }
        public DbSet<RepReportesVariable> RepReportesVariables { get; set; }


        #endregion

        #region Inventario
        public DbSet<InvProductoImagen> InvProductoImagens { get; set; }

        public DbSet<InvProductoImpuesto> InvProductoImpuestos { get; set; }

        public DbSet<InvProductoSuplidor> InvProductoSuplidors { get; set; }

        public DbSet<InvTipoProducto> InvTipoProductos { get; set; }

        public DbSet<InvMarcas> Marcas { get; set; }

        public DbSet<Versiones> Versiones { get; set; }

        public DbSet<Producto> Productos { get; set; }

        public DbSet<Descuentos> Descuentos { get; set; }

       
        public DbSet<Suplidores> Suplidores { get; set; }

        public DbSet<ContactosSuplidores> ContactosSuplidores { get; set; }

        public DbSet<InvImpuestos> Impuestos { get; set; }

        public DbSet<Precio> Precios { get; set; }

        public DbSet<Pedido> Pedidos { get; set; }

        

        public DbSet<InvMetodoPago> InvMetodoPagos { get; set; }

        public DbSet<TipoMovimiento> TipoMovimientos { get; set; }

        #endregion

        #region Seguridad
        public DbSet<GnPerfil> GnPerfil {get; set; }
        public DbSet<GnPermiso> GnPermiso { get; set; }
        public DbSet<Usuario> Usuario { get; set; }
        #endregion

        #region Refactor del Menu
        public DbSet<GnModulo> GNModulos { get; set; }
        public DbSet<GnSubMenu> GNSubMenus { get; set; }
        public DbSet<GnEmpresaXModulo> GnEmpresaXModulos { get; set; }
        public DbSet<GnEmpresaXPerfilXSubMenu> GnEmpresaXPerfilXSubMenus { get; set; }
        #endregion

        #region HelpDesk
        public DbSet<HdkCategoryTicket> HdkCategoryTicket { get; set; }
        public DbSet<HdkDepartaments> HdkDepartaments { get; set; }
        public DbSet<HdkDepartXUsuario> HdkDepartXUsuario { get; set; }
        public DbSet<HdkErrorSubCategory> HdkErrorSubCategory { get; set; }
        public DbSet<HdkFileEvidenceTicket> HdkFileEvidenceTicket { get; set; }
        public DbSet<HdkNoteTicket> HdkNoteTicket { get; set; }
        public DbSet<HdkPrioridadTicket> HdkPrioridadTicket { get; set; }
        public DbSet<HdkSolutionTicket> HdkSolutionTicket { get; set; }
        public DbSet<HdkStatusTicket> HdkStatusTicket { get; set; }
        public DbSet<HdkSubCategory> HdkSubCategory { get; set; }
        public DbSet<HdkTickets> HdkTickets { get; set; }
        public DbSet<HdkTypeTicket> HdkTypeTicket { get; set; }

        #endregion

        #region Language
        public DbSet<GnLanguages> GnLanguages { get; set; }
        public DbSet<GnLanguagesByTable> GnLanguagesByTable { get; set; }
        public DbSet<GnLanguagesTableSistemas> GnLanguagesTableSistemas { get; set; }
        #endregion

        #region Modulo General
        public DbSet<GnSchedule> GnSchedule { get; set; }
        public DbSet<GnScheduleUser> GnScheduleUser { get; set; }
        public DbSet<GnUploadFileParametro> GnUploadFileParametro { get; set; }
        public DbSet<GnTecnoAlmacenExterno> GnTecnoAlmacenExterno { get; set; }
        #endregion

        #region Auditoria
        public DbSet<AleAuditoria> AleAuditoria { get; set; }
        public DbSet<AleLogin> AleLogin { get; set; }
        public DbSet<AleLogs> AleLogs { get; set; }
        public DbSet<AlePrint> AlePrint { get; set; }
        public DbSet<AleAuditLog> AleAuditLog { get; set; }
        public DbSet<AleAuditTableControl> AleAuditTableControl { get; set; }
        #endregion

        #region Modulo Citas

        public DbSet<CtaAppointmentManagement> ctaAppointmentManagement { get; set; }
        public DbSet<CtaAppointmentMovements> CtaAppointmentMovements { get; set; }
        public DbSet<CtaAppointmentReason> CtaAppointmentReason { get; set; }
        public DbSet<CtaAppointments> CtaAppointments { get; set; }
        public DbSet<CtaConfiguration> CtaConfiguration { get; set; }
        public DbSet<CtaEmailConfiguration> CtaEmailConfiguration { get; set; }
        public DbSet<CtaMeetingPlace> CtaMeetingPlace { get; set; }
        public DbSet<CtaSessionDetails> CtaSessionDetails { get; set; }
        public DbSet<CtaSessions> CtaSessions { get; set; }
        public DbSet<CtaState> CtaState { get; set; }
        public DbSet<CtaUnwanted> CtaUnwanted { get; set; }
        public DbSet<CtaAppointmentSequence> CtaAppointmentSequence { get; set; }
        public DbSet<CtaAppointmentSequence> CtaAppointmentArea { get; set; }
        public DbSet<CtaAppointmentSequence> CtaAreaXUser { get; set; }

        #endregion 
        public DbSet<Pais> Pais { get; set; }

        public DbSet<Region> Region { get; set; }

        public DbSet<Provincia> Provincia { get; set; }

        public DbSet<Municipio> Municipio { get; set; }

        public DbSet<Imagen> Imagenes { get; set; }

        public DbSet<TipoPago> TipoPagos { get; set; }

        public DbSet<CuentasPorPagar> CuentasPorPagar { get; set; }

        public DbSet<DetalleCuentaPorPagar> DetalleCuentaPorPagar { get; set; }

        public DbSet<Clientes> Clientes { get; set; }

        public DbSet<DgiiNcfSecuencia> DgiiNcfSecuencia { get; set; }

        public DbSet<DgiiNcf> DgiiNcf { get; set; }

        public DbSet<DetallePedido> DetallePedido { get; set; }

        public DbSet<Facturacion> Facturacion { get; set; }

        public DbSet<DetalleFacturacion> DetalleFacturacion { get; set; }

        public DbSet<TipoTransaccion> TipoTransaccion { get; set; }

        public DbSet<CuentasPorCobrar> CuentasPorCobrar { get; set; }

        public DbSet<DetalleCuentasPorCobrar> DetalleCuentasPorCobrar { get; set; }

        public DbSet<Cotizacion> Cotizacion { get; set; }

        public DbSet<DetalleCotizacion> DetalleCotizacion { get; set; }

        public DbSet<Gn_Perfil> Gn_Perfil { get; set; }

        public DbSet<Gn_Permiso> Gn_Permiso { get; set; }

        public DbSet<GnMenu> GnMenu { get; set; }

        public DbSet<GnEmpresa> GnEmpresa { get; set; }

        public DbSet<GnSucursal> GnSucursal { get; set; }

        public DbSet<SC_HORA_X_USR002> SC_HORA_X_USR002 { get; set; }

        public DbSet<SC_HORAGROUP002> SC_HORAGROUP002 { get; set; }

        public DbSet<SC_HORARIO001> SC_HORARIO001 { get; set; }

        public DbSet<SC_IPSYS001> SC_IPSYS001 { get; set; }

        public DbSet<SC_USUAR001> SC_USUAR001 { get; set; }

        public DbSet<Ciudades_X_Paises> Ciudades_X_Paises { get; set; }

        public DbSet<Tipo_Identificacion> Tipo_Identificacion { get; set; }

        public DbSet<Bancos> Bancos { get; set; }

        public DbSet<AperturaCierreCaja> AperturaCierreCajas { get; set; }

        public DbSet<Caja> Cajas { get; set; }

        public DbSet<Billetes_Moneda> Billetes_Moneda { get; set; }

        public DbSet<BovedaCaja> BovedaCajas { get; set; }

        public DbSet<BovedaCajaDesglose> BovedaCajaDesgloses { get; set; }

        public DbSet<BovedaMovimiento> BovedaMovimientoes { get; set; }

        public DbSet<ConciliacionTCTF> ConciliacionTCTFs { get; set; }

        public DbSet<Moneda> Monedas { get; set; }

        public DbSet<CuentaBancos> CuentaBancos { get; set; }

        public DbSet<MovimientoBanco> MovimientoBancoes { get; set; }

        public DbSet<TipoMovimientoBanco> TipoMovimientoBancoes { get; set; }

        #region Almacen
        public DbSet<InvAlmacenes> InvAlmacenes { get; set; }
        public DbSet<InvAlmacenInventario> InvAlmacenInventario { get; set; }
        public DbSet<InvInventarioSucursal> InvInventarioSucursal { get; set; }
        public DbSet<InvMovAlmacenSucursal> InvMovAlmacenSucursal { get; set; }
        public DbSet<InvMovAlmacenSucursalDetalle> InvMovAlmacenSucursalDetalle { get; set; }
        public DbSet<InvMovimientoAlmacen> InvMovimientoAlmacen { get; set; }
        public DbSet<InvMovimientoAlmacenDetalle> InvMovimientoAlmacenDetalle { get; set; }
        public DbSet<InvMovimientoSucursalDetalle> InvMovimientoSucursalDetalle { get; set; }
        public DbSet<InvMovInventarioSucursal> InvMovInventarioSucursal { get; set; }
        #endregion

    }
}
