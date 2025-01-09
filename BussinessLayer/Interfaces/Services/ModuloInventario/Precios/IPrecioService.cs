using BussinessLayer.DTOs.ModuloInventario.Precios;

namespace BussinessLayer.Interfaces.Services.ModuloInventario.Precios
{
    public interface IPrecioService
    {
        Task<ViewPreciosDto> GetPriceById(int id);
        Task<List<ViewPreciosDto>> GetPricesByFilters(long idCompany, int? idProduct);
        Task<int?> CreatePrices(CreatePreciosDto productosPrecio);
        Task EditPrice(EditPricesDto price);
        Task SetSamePrice(List<EditPricesDto> prices, decimal newPrice);
        Task DeletePriceById(int id);
    }
}