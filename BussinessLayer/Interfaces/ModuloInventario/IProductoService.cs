using BussinessLayer.DTOs.ModuloInventario;

namespace BussinessLayer.Interfaces.ModuloInventario
{
    public interface IProductoService
    {
        Task<CreateProductsDto> CreateProduct(CreateProductsDto producto);
        Task<List<ViewProductsDto>> GetProducts();
        Task<List<ViewProductsDto>> GetProductByIdCompany(long idCompany);
        Task<ViewProductsDto> GetProductById(int idProduct);
        Task<ViewProductsDto> GetProductByCodeInCompany(string codeProduct, long idEmpresa);
        Task DeleteProductById(int id);
        Task DeleteProductByCodigo(string codigo, long idEmpresa);
        Task<bool> CheckCodeExist(string productCode);
        Task EditProduct(ViewProductsDto producto);
        Task<List<ViewProductsDto>> GetAllFacturacion(long idEmpresa);
        Task<ViewProductsDto> GetProductoByBarCode(long idEmpresa, string codigoBarra);
        Task<ViewProductsDto> GetProductoByBarCodeFactura(long idEmpresa, string codigoBarra);
        Task<List<ViewProductsDto>> GetAllAgotados(long idEmpresa);
        //Task<List<ViewProductsDto>> GetProductsBySuplidor(int idSuplidor, long idEMpresa);
    }
}