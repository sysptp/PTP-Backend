using BussinessLayer.DTOs.ModuloInventario.Versiones;
using Microsoft.EntityFrameworkCore;

public interface IVersionService 
{
    // Obtener data por su id
    Task<ViewVersionsDto> GetVersionById(int id);

    // Obtener data por su empresa
    Task<List<ViewVersionsDto>> GetVersionByCompany(int id);

    // Crear un nuevo 
    Task<int?> CreateVersion(CreateVersionsDto create);

    // Editar existente
    Task EditVersion(EditVersionsDto edit);

    // Servicio para eliminar por id unico
    Task DeleteVersionById(int id);
}
