using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLayer.DTOs.ModuloInventario.Pedidos
{
    public class ViewDetallePedidoDto
    {
        public int Id { get; set; }
        public int IdProducto { get; set; }
        public long? IdEmpresa { get; set; }
        public int? IdPedido { get; set; }
        public int Cantidad { get; set; }
    }
}
