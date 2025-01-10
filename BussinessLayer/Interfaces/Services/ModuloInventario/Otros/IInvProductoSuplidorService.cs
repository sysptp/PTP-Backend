using BussinessLayer.DTOs.ModuloGeneral.Monedas;
using BussinessLayer.DTOs.ModuloInventario.Otros;

namespace BussinessLayer.Interfaces.Services.ModuloInventario.Otros
{
    public interface IInvProductoSuplidorService
    {
        Task Update(EditInvProductoSuplidorDTO model);
        Task<ViewInvProductoSuplidorDTO> GetById(int id);
        Task<List<ViewInvProductoSuplidorDTO>> GetAllByProduct(int idProducto);
        Task<List<ViewInvProductoSuplidorDTO>> GetAllBySupplier(int idSuplidor);
        Task<List<ViewInvProductoSuplidorDTO>> GetAll();
        Task Delete(int Id);
        Task<int> Add(CreateInvProductoSuplidorDTO model);
    }
}
