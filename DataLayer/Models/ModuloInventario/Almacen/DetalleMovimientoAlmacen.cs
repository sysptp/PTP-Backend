using DataLayer.Models.ModuloInventario.Productos;
using DataLayer.Models.Otros;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Models.ModuloInventario.Almacen
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
