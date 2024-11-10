using BussinessLayer.DTOs.ModuloInventario.Productos;

namespace BussinessLayer.Interfaces.ModuloInventario.Productos
{
    public interface IProductoService
    {
        Task<int?> CreateProduct(CreateProductsDto producto);
        Task<List<ViewProductsDto>> GetProducts();
        Task<List<ViewProductsDto>> GetProductByIdCompany(long idCompany);
        Task<ViewProductsDto> GetProductById(int idProduct);
        Task<ViewProductsDto> GetProductByCodeInCompany(string codeProduct, long idEmpresa);
        Task DeleteProductById(int id);
        Task DeleteProductByCodigo(string codigo, long idEmpresa);
        Task<bool> CheckCodeExist(string productCode, long idEmpresa);
        Task EditProduct(EditProductDto producto);
        Task<List<ViewProductsDto>> GetAllFacturacion(long idEmpresa);
        Task<ViewProductsDto> GetProductoByBarCode(long idEmpresa, string codigoBarra);
        Task<ViewProductsDto> GetProductoByBarCodeFactura(long idEmpresa, string codigoBarra);
        Task<List<ViewProductsDto>> GetAllAgotados(long idEmpresa);
    }
}