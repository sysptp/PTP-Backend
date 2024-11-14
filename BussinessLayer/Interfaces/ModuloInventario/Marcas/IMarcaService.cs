using BussinessLayer.DTOs.ModuloInventario.Marcas;

public interface IMarcaService
{
    Task<ViewBrandDto> GetBrandById(int id);

    // Obtener data por su empresa
    Task<List<ViewBrandDto>> GetBrandsByCompany(int id);

    // Crear un nuevo 
    Task<int?> CreateBrand(CreateBrandDto create);

    // Editar existente
    Task EditBrand(EditBrandDto edit);

    // Servicio para eliminar por id unico
    Task DeleteBrandById(int id);
}
