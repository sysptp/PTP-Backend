using DataLayer.Models;
using DataLayer.Models.MenuApp;
using DataLayer.Models.Caja;
using DataLayer.Models.Reporteria;
using DataLayer.Models.Bancos;
using DataLayer.Models.Boveda;
using DataLayer.Models.Otros;
using DataLayer.Models.Geografia;
using DataLayer.Models.Seguridad;
using DataLayer.Models.Empresa;
using DataLayer.Models.Cotizaciones;
using DataLayer.Models.Cuentas;
using DataLayer.Models.Facturas;
using Microsoft.EntityFrameworkCore;
using DataLayer.Models.Entities;
using DataLayer.Models.ModuloGeneral;
using DataLayer.Models.ModuloInventario.Version;
using DataLayer.Models.ModuloInventario.Suplidor;
using DataLayer.Models.ModuloInventario.Productos;
using DataLayer.Models.ModuloInventario.Precios;
using DataLayer.Models.ModuloInventario.Pedidos;
using DataLayer.Models.ModuloInventario.Monedas;
using DataLayer.Models.ModuloInventario.Marcas;
using DataLayer.Models.ModuloInventario.Impuesto;
using DataLayer.Models.ModuloInventario.Descuento;
using DataLayer.Models.ModuloInventario.Almacen;

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


        // Reporteria
        // CREADO POR MANUEL 3/10/2024
        public DbSet<CentroReporteria> CentroReporterias { get; set; }

        // CREADO POR MANUEL 3/10/2024
        public DbSet<VariablesReporteria> VariablesReporterias { get; set; }

        // 

        public DbSet<InvProductoImagen> InvProductoImagens { get; set; }

        public DbSet<InvProductoImpuesto> InvProductoImpuestos { get; set; }

        public DbSet<InvProductoSuplidor> InvProductoSuplidors { get; set; }

        public DbSet<Pais> Pais { get; set; }

        public DbSet<Region> Region { get; set; }

        public DbSet<Provincia> Provincia { get; set; }

        public DbSet<Municipio> Municipio { get; set; }

        public DbSet<Marca> Marcas { get; set; }

        public DbSet<Imagen> ImagenesProducto { get; set; }

        public DbSet<Versiones> Versiones{ get; set; }

        public DbSet<Producto> Productos { get; set; }

        public DbSet<Descuentos> Descuentos { get; set; }
        
        public DbSet<Almacenes> Almacenes { get; set; }

        public DbSet<TipoMovimiento> TipoMovimientos { get; set; }

        public DbSet<TipoPago> TipoPagos { get; set; }

        public DbSet<MovimientoAlmacen> MovimientoAlmacenes { get; set; }

        public DbSet<DetalleMovimientoAlmacen> DetalleMovimientoAlmacenes { get; set; }

        public DbSet<CuentasPorPagar> CuentasPorPagar { get; set; }

        public DbSet<DetalleCuentaPorPagar> DetalleCuentaPorPagar { get; set; }

        public DbSet<Cliente> Clientes { get; set; }

        public DbSet<Suplidores> Suplidores { get; set; }

        public DbSet<ContactosSuplidores> ContactosSuplidores { get; set; }

        public DbSet<DgiiNcfSecuencia> DgiiNcfSecuencia { get; set; }

        public DbSet<DgiiNcf> DgiiNcf { get; set; }

        public DbSet<Precio> Precios { get; set; }

        public DbSet<Pedido> Pedidos { get; set; }

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

        public DbSet<GnEmpresa> GnEmpresa  { get; set; }

        public DbSet<GnSucursal> GnSucursal { get; set; }

        public DbSet<SC_HORA_X_USR002> SC_HORA_X_USR002 { get; set; }

        public DbSet<SC_HORAGROUP002> SC_HORAGROUP002 { get; set; }

        public DbSet<SC_HORARIO001> SC_HORARIO001 { get; set; }

        public DbSet<SC_IMP001> SC_IMP001 { get; set; }

        public DbSet<SC_IPSYS001> SC_IPSYS001 { get; set; }

        public DbSet<SC_MUNIC001> SC_MUNIC001 { get; set; }

        public DbSet<SC_PAIS001> SC_PAIS001 { get; set; }

        public DbSet<SC_PROV001> SC_PROV001 { get; set; }

        public DbSet<SC_REG001> SC_REG001 { get; set; }

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

        #region Seguridad
        public DbSet<GnPerfil> GnPerfil {get; set; }
        public DbSet<GnPermiso> GnPermiso { get; set; }
        public DbSet<Usuario> Usuario { get; set; }
        #endregion

        #region refactor del Menu
        public DbSet<GnModulo> GNModulos { get; set; }
        public DbSet<GnSubMenu> GNSubMenus { get; set; }
        public DbSet<GnEmpresaXModulo> GnEmpresaXModulos { get; set; }
        public DbSet<GnEmpresaXPerfilXSubMenu> GnEmpresaXPerfilXSubMenus { get; set; }
        #endregion
    }
}
