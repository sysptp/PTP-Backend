using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using DataLayer.Models.ModuloInventario.Impuesto;

namespace DataLayer.Models.ModuloInventario.Productos
{
    [Table("InvProductoImpuesto")]
    public class InvProductoImpuesto
    {
        [Key]
        public int? Id { get; set; }  

        public int? ProductoId { get; set; }  

        public int? ImpuestoId { get; set; }  

        [ForeignKey("ProductoId")]
        public virtual Producto? Producto { get; set; }

        [ForeignKey("ImpuestoId")]
        public virtual InvImpuestos? Impuesto { get; set; }
    }
}
