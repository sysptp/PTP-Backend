using BussinessLayer.DTOs.ModuloInventario.Precios;
using BussinessLayer.DTOs.ModuloInventario.Productos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLayer.Interfaces.ModuloInventario.Productos
{
    public interface ITipoProductoService
    {
        Task<ViewProductTypeDto> GetProductTypeById(int id);
        Task<List<ViewProductTypeDto>> GetAllProductsTypeByComp(int idCompany);
        Task<int?> CreateProductType(CreateTipoProductoDto productType);
        Task EditProductType(EditProductTypeDto type);
        Task DeleteProductTypeById(int id);
    }
}
