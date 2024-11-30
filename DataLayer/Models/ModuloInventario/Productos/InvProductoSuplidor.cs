using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using DataLayer.Models.ModuloGeneral.Monedas;

namespace DataLayer.Models.ModuloInventario.Productos
{
    [Table("InvProductoSuplidor")]
    public class InvProductoSuplidor
    {
        [Key]
        public int? Id { get; set; }  

        public int? ProductoId { get; set; }  

        public int? SuplidorId { get; set; }  

        public int? IdMoneda { get; set; }    

        public decimal? ValorCompra { get; set; }  

        [ForeignKey("ProductoId")]
        public virtual Producto? Producto { get; set; }

        [ForeignKey("SuplidorId")]
        public virtual Suplidores? Suplidor { get; set; }

        [ForeignKey("IdMoneda")]
        public virtual Moneda? Moneda { get; set; }
    }
}
