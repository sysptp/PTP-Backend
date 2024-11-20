using BussinessLayer.DTOs.ModuloInventario.Impuestos;
using BussinessLayer.DTOs.ModuloInventario.Versiones;
using DataLayer.Models.ModuloInventario.Impuesto;

namespace BussinessLayer.Interfaces.ModuloInventario.Impuestos
{
    public interface IImpuestosService
    {
        // Obtener data por su id
        Task<ViewTaxDto> GetTaxById(int id);

        // Obtener data por su empresa
        Task<List<ViewTaxDto>> GetTaxByCompany(int id);

        // Crear un nuevo 
        Task<int?> CreateTax(CreateTaxDto create);

        // Editar existente
        Task EditTax(EditTaxDto edit);

        // Servicio para eliminar por id unico
        Task DeleteTaxById(int id);
    }
}
