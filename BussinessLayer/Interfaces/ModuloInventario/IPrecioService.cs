using BussinessLayer.DTOs.Precios;

namespace BussinessLayer.Interfaces.ModuloInventario
{
    public interface IPrecioService
    {
        Task<List<ViewPreciosDto>> GetPricesByIdProduct(int idProduct);
        Task CreatePrices(CreatePreciosDto productosPrecio);
        Task EditPrice(ViewPreciosDto price);
        Task SetSamePrice(List<ViewPreciosDto> prices, decimal newPrice);
    }
}