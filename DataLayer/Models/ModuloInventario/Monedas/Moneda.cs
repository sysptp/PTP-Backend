using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DataLayer.Models.ModuloInventario.Impuesto;
using DataLayer.Models.ModuloInventario.Precios;
using DataLayer.Models.ModuloInventario.Productos;

namespace DataLayer.Models.ModuloInventario.Monedas
{
    [Table("Moneda")]
    public class Moneda
    {
        [Key]
        public int? Id { get; set; }

        [Required]
        public int? IdPais { get; set; }

        [Required]
        public long? IdEmpresa { get; set; }

        [Required, MaxLength(100)]
        public string? Descripcion { get; set; }

        [Required, MaxLength(10)]
        public string? Siglas { get; set; }

        [Required, MaxLength(100)]
        public string? Simbolo { get; set; }

        [Required]
        public bool? EsLocal { get; set; }

        public decimal? TasaCambio { get; set; }

        [Required]
        public DateTime? FechaCreacion { get; set; }

        public DateTime? FechaModificacion { get; set; }

        [Required]
        public string? UsuarioCreacion { get; set; }

        public string? UsuarioModificacion { get; set; }

        // Relación: Un `Moneda` puede estar asociado a muchos `Precio`
        public virtual ICollection<Precio>? Precios { get; set; }

        public virtual ICollection<InvProductoSuplidor>? ProductoSuplidores { get; set; }

        public virtual ICollection<SC_IMP001>? Impuestos { get; set; }
    }
}
