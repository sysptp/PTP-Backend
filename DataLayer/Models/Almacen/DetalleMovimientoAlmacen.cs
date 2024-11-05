using DataLayer.Models.ModuloInventario;
using DataLayer.Models.Otros;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Models.Almacen
{
    public class DetalleMovimientoAlmacen : BaseModel
    {
        public int IdMovimiento { get; set; }
        [ForeignKey("IdMovimiento")]
        public virtual MovimientoAlmacen MovimientoAlmacen { get; set; }

        public int IdProducto { get; set; }
        [ForeignKey("IdProducto")]
        public virtual Producto Producto { get; set; }

        public int Cantidad { get; set; }
       
    }
}
