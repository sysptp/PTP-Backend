using DataLayer.Enums;
using DataLayer.Models.Otros;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Models.Productos
{
    public class Descuentos : BaseModel
   {
        public int IdProducto { get; set; }

        [ForeignKey("IdProducto")]
        public virtual Producto Producto { get; set; }

        public int DescuentoPorcentaje { get; set; }

        public int DescuentoFijo { get; set; }
         
        public DateTime FechaInicio { get; set; }

        public DateTime FechaFin { get; set; }

        public TipoDescuento TipoDescuento { get; set; }

        public bool Activo { get; set; }

        public virtual string NombreProducto { get; set; }
    }
}
