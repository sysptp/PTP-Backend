using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DataLayer.Models.ModuloGeneral.Monedas;
using DataLayer.Models.ModuloInventario.Otros;

namespace DataLayer.Models.ModuloInventario.Impuesto
{
    [Table("InvImpuestos")]
    public class InvImpuestos
    {
        [Key]
        public int? Id { get; set; }

        [Required]
        public int? IdEmpresa { get; set; }

        public int? IdMoneda { get; set; }

        [Required]
        public bool? EsPorcentaje { get; set; }

        [Required]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal ValorImpuesto { get; set; }

        [Required]
        [MaxLength(250)]
        public string? NombreImpuesto { get; set; }

        [Required]
        public bool? Borrado { get; set; }

        [Required]
        public DateTime? FechaCreacion { get; set; } = DateTime.Now;

        public DateTime? FechaModificacion { get; set; }

        [Required]
        public string? UsuarioCreacion { get; set; }

        public string? UsuarioModificacion { get; set; }

        [ForeignKey("IdMoneda")]
        public virtual Moneda? Moneda { get; set; }

        public virtual ICollection<InvProductoImpuesto>? ProductoImpuestos { get; set; }
    }
}
