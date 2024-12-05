using DataLayer.Models.ModuloInventario.Almacen;
using DataLayer.Models.ModuloInventario.Otros;
using DataLayer.Models.ModuloInventario.Pedidos;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Models.ModuloInventario.Productos
{
    [Table("InvProducto")]
    public class Producto
    {
        [Key]
        public int? Id { get; set; }

        [Required]
        public long? IdEmpresa { get; set; }

        // Relación con la tabla Versiones
        public int? IdVersion { get; set; }

        [ForeignKey("IdVersion")]
        public virtual Versiones? Version { get; set; }

        // Relación con la tabla GnTipoProducto
        public int? IdTipoProducto { get; set; }

        [ForeignKey("IdTipoProducto")]
        public virtual InvTipoProducto? TipoProducto { get; set; }

        [MaxLength(50)]
        public string? CodigoBarra { get; set; }

        [Required]
        [MaxLength(250)]
        public string? Codigo { get; set; }

        [Required]
        [MaxLength(100)]
        public string? NombreProducto { get; set; }

        [Required]
        public string? Descripcion { get; set; }

        [Required]
        public int? CantidadLote { get; set; }

        [Required]
        public int? CantidadInventario { get; set; }

        [Required]
        public int? CantidadMinima { get; set; }

        [Required]
        public DateTime? FechaCreacion { get; set; }

        public DateTime? FechaModificacion { get; set; }

        [Required]
        public bool? AdmiteDescuento { get; set; }

        [Required]
        public bool? HabilitaVenta { get; set; }

        [Required]
        public bool? AplicaImpuesto { get; set; }

        [Required]
        public bool? EsLote { get; set; }

        [Required]
        public bool? EsProducto { get; set; }

        [Required]
        public bool? EsLocal { get; set; }

        [Required]
        public bool? Borrado { get; set; }

        [Required]
        public bool? Activo { get; set; }

        [Required]
        public string? UsuarioCreacion { get; set; }

        public string? UsuarioModificacion { get; set; }

        public virtual ICollection<InvProductoImpuesto>? InvProductoImpuestos { get; set; }

        public virtual ICollection<InvProductoSuplidor>? InvProductoSuplidores { get; set; }

        public virtual ICollection<InvProductoImagen>? InvProductoImagenes { get; set; }

        public virtual ICollection<DetalleMovimientoAlmacen>? MovimientoDetalles { get; set; }

        public virtual ICollection<DetallePedido>? DetallePedidos { get; set; }

        public virtual ICollection<InvInventarioSucursal>? InventarioSucursales { get; set; }
    }
}
