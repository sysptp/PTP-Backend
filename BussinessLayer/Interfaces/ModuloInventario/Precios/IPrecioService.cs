using BussinessLayer.DTOs.ModuloInventario.Precios;

namespace BussinessLayer.Interfaces.ModuloInventario.Precios
{
    public interface IPrecioService
    {
        Task<ViewPreciosDto> GetPriceById(int id);
        Task<List<ViewPreciosDto>> GetPricesByIdProduct(int idProduct);
        Task<CreatePreciosDto> CreatePrices(CreatePreciosDto productosPrecio);
        Task<EditPricesDto> EditPrice(EditPricesDto price);
        Task SetSamePrice(List<EditPricesDto> prices, decimal newPrice);
        Task DeletePriceById(int id);
    }
}