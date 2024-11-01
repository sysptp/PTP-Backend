using System.Collections.Generic;
using System.Threading.Tasks;
using BussinessLayer.DTOs.Productos;
using BussinessLayer.Interface.IOtros;
using BussinessLayer.ViewModels;
using DataLayer.Models;
using DataLayer.Models.Productos;

namespace BussinessLayer.Interface.IProductos
{
    public interface IProductoService : IBaseService<Producto>
    {
        Task CreateProduct(CrearProductoDto producto);

        //
        ProductoInfoViewModel GetInfoViewModel(Producto producto, long idEMpresa);
        Task<List<ProductoInfoViewModel>> GetInfoViewModelList(long idEMpresa);
        CrearProductoDto GetCreateViewModel(Producto producto, long idEMpresa);
        Task<bool> CheckCodeExist(string productCode);
        Task<Producto> GetProductoByCB(long idEmpresa ,string cb = "");
        Task<List<Producto>> GetProductoWithPrice(int priceNumber, long idEMpresa);
        Task<List<ProductoInfoViewModel>> GetProductoBySuplidor(int idSuplidor, long idEMpresa);
        Producto GetProductoFromViewModel(CrearProductoDto producto, long idEMpresa);
        Task<List<Producto>> GetProductListById(int[] productsIdList, long idEMpresa);
        Task<ProductPhotosViewModel> GetPhotoViewModel(int productId, long idEMpresa);
        Task<bool> SetProductPicture(int productId, string image, long idEMpresa);
        Task<bool> ChangeProductPicture(int imageId, string image, long idEMpresa);
        Task DeleteProducto(ProductoInfoViewModel producto, long idEMpresa);
        Task<List<ProductoInfoViewModel>> GetInfoViewModelListAgotado(long idEMpresa);
        Task<IList<Producto>> GetAllFacturacion(long idEMpresa);
        Task<Producto> GetProductoByCBFactura(long idEmpresa, string cb = "");
        Task<Producto> GetByIdDesc(int id);
    }
}