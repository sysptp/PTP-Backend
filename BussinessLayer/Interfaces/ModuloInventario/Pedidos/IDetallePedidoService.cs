using BussinessLayer.DTOs.ModuloInventario.Pedidos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLayer.Interfaces.ModuloInventario.Pedidos
{
    public interface IDetallePedidoService
    {
        Task<int> Add(CreateDetallePedidoDto model);
        Task Update(EditDetallePedidoDto model);
        Task Delete(int id);
        Task<ViewDetallePedidoDto> GetById(int id);
        Task<List<ViewDetallePedidoDto>> GetAll();
        Task<List<ViewDetallePedidoDto>> GetByCompany(long idEmpresa);
    }
}
