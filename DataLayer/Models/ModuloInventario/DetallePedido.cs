using DataLayer.Models.Otros;
using DataLayer.Models.Pedidos;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Models.ModuloInventario
{
    public class DetallePedido : BaseModel
    {
        public int IdProducto { get; set; }

        [ForeignKey("IdProducto")]
        public virtual Producto Producto { get; set; }

        [NotMapped]
        public virtual string NombreProducto { get; set; }

        public int Cantidad { get; set; }

        public virtual IList<Pedido> Pedido { get; set; }
    }
}