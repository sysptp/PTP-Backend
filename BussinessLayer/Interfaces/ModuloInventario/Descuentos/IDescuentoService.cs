using BussinessLayer.DTOs.ModuloInventario.Descuentos;
using BussinessLayer.DTOs.ModuloInventario.Impuestos;
using BussinessLayer.Interface.IOtros;
using DataLayer.Models.ModuloInventario.Descuento;

public interface IDescuentoService
{
    // Obtener data por su id
    Task<ViewDiscountDto> GetDiscountById(int id);

    // Obtener data por su empresa
    Task<List<ViewDiscountDto>> GetDiscountByCompany(int id);

    // Crear un nuevo 
    Task<int?> CreateDiscount(CreateDiscountDto create);

    // Editar existente
    Task EditDiscount(EditDiscountDto edit);

    // Servicio para eliminar por id unico
    Task DeleteDiscountById(int id);

}
