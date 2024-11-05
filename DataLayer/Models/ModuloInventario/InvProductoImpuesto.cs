using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.Models.ModuloInventario
{
    [Table("InvProductoImpuesto")]
    public class InvProductoImpuesto
    {
        [Key]
        public int? Id { get; set; }  // Clave primaria

        public int? ProductoId { get; set; }  // Clave externa a Producto
        public int? ImpuestoId { get; set; }  // Clave externa a SC_IMP001 (Impuesto)

        // Propiedades de navegación
        [ForeignKey("ProductoId")]
        public virtual Producto? Producto { get; set; }

        [ForeignKey("ImpuestoId")]
        public virtual SC_IMP001? Impuesto { get; set; }
    }

}
