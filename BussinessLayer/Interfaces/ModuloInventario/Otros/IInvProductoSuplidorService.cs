using BussinessLayer.DTOs.ModuloGeneral.Monedas;
using BussinessLayer.DTOs.ModuloInventario.Otros;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLayer.Interfaces.ModuloInventario.Otros
{
    public interface IInvProductoSuplidorService
    {
        Task Update(EditInvProductoSuplidorDTO model);
        Task<ViewInvProductoSuplidorDTO> GetById(int id);
        Task<List<ViewInvProductoSuplidorDTO>> GetAllByProduct(int idProducto);
        Task<List<ViewInvProductoSuplidorDTO>> GetAllBySupplier(int idSuplidor);
        Task Delete(int Id);
        Task<int> Add(CreateInvProductoSuplidorDTO model);
    }
}
