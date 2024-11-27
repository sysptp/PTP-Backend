using DataLayer.Models.ModuloInventario.Productos;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Models.ModuloInventario.Almacen
{
    [Table("InvMovimientoAlmacenDetalle")]
    public class DetalleMovimientoAlmacen
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int IdMovimiento { get; set; }

        [Required]
        public int IdProducto { get; set; }

        public long? IdEmpresa { get; set; }

        [Required]
        public int Cantidad { get; set; }

        [Required]
        public bool Borrado { get; set; }

        [Required]
        public bool EsVencimiento { get; set; }

        public DateTime? FechaVencimiento { get; set; }

        public DateTime? FechaModificacion { get; set; }

        [MaxLength(50)]
        public string? UsuarioModificacion { get; set; }

        [Required]
        [MaxLength(50)]
        public string? UsuarioCreacion { get; set; }

        [Required]
        public DateTime FechaCreacion { get; set; }

        // Navigation Property
        [ForeignKey("IdProducto")]
        public virtual Producto? Producto { get; set; }

    }
}
