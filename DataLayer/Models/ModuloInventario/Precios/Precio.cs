using DataLayer.Models.ModuloInventario.Monedas;
using DataLayer.Models.ModuloInventario.Productos;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Models.ModuloInventario.Precios
{
    [Table("Precio")]
    public class Precio
    {
        [Key]
        public int? Id { get; set; }

        [Required]
        public int? IdProducto { get; set; }

        public long? IdEmpresa { get; set; }

        public int? IdMoneda { get; set; }

        [Required]
        [Range(0, 9999999999.99)]
        public decimal PrecioValor { get; set; }

        [Required]
        public bool? HabilitarVenta { get; set; }

        [Required]
        public bool? Borrado { get; set; }

        [Required]
        public DateTime? FechaCreacion { get; set; }

        public DateTime? FechaModificacion { get; set; }

        [Required]
        public string? UsuarioCreacion { get; set; }

        public string? UsuarioModificacion { get; set; }

        // Relación: Un `Precio` está asociado a un `Producto`
        [ForeignKey("IdProducto")]
        public virtual Producto? Producto { get; set; }

        // Relación: Un `Precio` está asociado a una `Moneda`
        [ForeignKey("IdMoneda")]
        public virtual Moneda? Moneda { get; set; }
    }
}