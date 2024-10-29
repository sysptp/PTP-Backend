using DataLayer.Models;
using DataLayer.Models.Almacen;
using System.Collections.Generic;

namespace BussinessLayer.ViewModels
{
    public  class ProductosFromPedidoVm
    {
        //Header
        public long TotalFacturado { get; set; }

        public int IdAlmacen { get; set; }

        public int IdTipoPago { get; set; }

        public int IdSuplidor { get; set; }

        public int IdTipoMovimiento { get; set; }

        public string NoFactura { get; set; }


        //Desglose

        public int IdProducto { get; set; }

        public string Cantidad { get; set; }

        public long Existencia { get; set; }

        public string CodProducto { get; set; }

        public string Producto { get; set; }

        public string  Subtotal { get; set; }

        public string Eliminar { get; set; }
        public string Envase { get; set; }

        public decimal  Precio { get; set; }

    }

    public class ViewModelOfViewModel
    {
        public virtual List<ProductosFromPedidoVm> ProductosFromPedidos { get; set; }

        public virtual  MovimientoAlmacen MovimientoAlmacen { get; set; }
    }
}
