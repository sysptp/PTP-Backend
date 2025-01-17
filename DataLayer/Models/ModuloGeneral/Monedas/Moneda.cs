using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DataLayer.Models.ModuloInventario.Impuesto;
using DataLayer.Models.ModuloInventario.Otros;
using DataLayer.Models.ModuloInventario.Precios;

namespace DataLayer.Models.ModuloGeneral.Monedas
{
    [Table("GnMoneda")]
    public class Moneda
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int IdPais { get; set; }

        [Required]
        public long IdEmpresa { get; set; }

        [Required, MaxLength(100)]
        public string? Descripcion { get; set; }

        [Required, MaxLength(10)]
        public string? Siglas { get; set; }

        [Required, MaxLength(100)]
        public string? Simbolo { get; set; }

        [Required]
        public bool? EsLocal { get; set; }

        public decimal? TasaCambio { get; set; }
        public string? RutaImagen { get; set; }
        [Required]
        public bool? Borrado { get; set; }

        [Required]
        public bool? Activo { get; set; }

        [Required]
        public DateTime? FechaCreacion { get; set; }

        public DateTime? FechaModificacion { get; set; }

        [Required]
        public string? UsuarioCreacion { get; set; }

        public string? UsuarioModificacion { get; set; }

        // Relación: Un `Moneda` puede estar asociado a muchos `Precio`
        public virtual ICollection<Precio>? Precios { get; set; }

        public virtual ICollection<InvProductoSuplidor>? ProductoSuplidores { get; set; }

        public virtual ICollection<InvImpuestos>? Impuestos { get; set; }
    }
}
