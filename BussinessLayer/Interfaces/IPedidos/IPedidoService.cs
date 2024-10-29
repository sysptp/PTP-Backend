using System.Collections.Generic;
using System.Threading.Tasks;
using BussinessLayer.Interface.IOtros;
using DataLayer.Models.Pedidos;
using DataLayer.Models.Productos;

namespace BussinessLayer.Interface.IPedido
{
    public interface IPedidoService : IBaseService<Pedido>
    {
        Task<IList<DetallePedido>> GetDetallesByPedido(int pedidoId, long idEMpresa);
        Task<IEnumerable<Pedido>> GetDetallePedidoByPedidoId(int idPedido);
        Task<Pedido> GetHeaderFromDetalle(int pedidoId);
    }
}