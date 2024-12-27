using BussinessLayer.DTOs.ModuloInventario.Suplidores;

public interface ISuplidoresService 
{
    // Obtener data por su id
    Task<ViewSuppliersDto> GetById(int id);

    // Obtener data por su empresa
    Task<List<ViewSuppliersDto>> GetByCompany(int id);

    // Crear un nuevo 
    Task<int?> Create(CreateSuppliersDto create);

    // Editar existente
    Task Edit(EditSuppliersDto edit);

    // Servicio para eliminar por id unico
    Task DeleteById(int id);

}
