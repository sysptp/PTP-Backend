using DataLayer.Models.Otros;
using DataLayer.Models.Productos;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Models.Cotizaciones
{
    public class DetalleCotizacion : BaseModel
    {
        public int CotizacionId { get; set; }

        [ForeignKey("CotizacionId")]
        public Cotizacion Cotizacion { get; set; }

        public int ProductoId { get; set; }

        [ForeignKey("ProductoId")]
        public Producto Producto { get; set; }

        public int Cantidad { get; set; }

        public decimal Precio { get; set; }

        public decimal Descuento { get; set; }

        public decimal Itbis { get; set; }  
    }
}
