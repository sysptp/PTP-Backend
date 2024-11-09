using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DataLayer.Models.ModuloInventario.Productos;

namespace DataLayer.Models.ModuloInventario.Descuento
{
    [Table("Descuentos")]
    public class Descuentos
    {
        [Key]
        public int? Id { get; set; }

        [Required]
        public int? IdProducto { get; set; }

        [Required]
        public long? IdEmpresa { get; set; }

        [Required]
        public bool? EsPorcentaje { get; set; }

        [Required]
        [Range(0, 999999.99)]
        public decimal? ValorDescuento { get; set; }

        [Required]
        public bool? EsPermanente { get; set; }

        [Required]
        public DateTime? FechaInicio { get; set; }

        [Required]
        public DateTime? FechaFin { get; set; }

        public DateTime? FechaModificacion { get; set; }

        [Required]
        public DateTime? FechaCreacion { get; set; }

        [Required]
        public bool? Activo { get; set; }

        [Required]
        public bool? Borrado { get; set; }

        [Required]
        public string? UsuarioCreacion { get; set; }

        public string? UsuarioModificacion { get; set; }

        // Relación: Un `Descuento` está asociado a un `Producto`
        [ForeignKey("IdProducto")]
        public virtual Producto? Producto { get; set; }
    }
}
