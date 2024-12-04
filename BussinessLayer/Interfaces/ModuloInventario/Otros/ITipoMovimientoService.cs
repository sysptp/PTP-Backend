using BussinessLayer.DTOs.ModuloInventario.Otros;
using DataLayer.Models;
using DataLayer.Models.Otros;

namespace BussinessLayer.Interfaces.ModuloInventario.Otros
{
    public interface ITipoMovimientoService
    {
        Task Update(EditTipoMovimientoDto model);
        Task<List<ViewTipoMovimientoDto>> GetByCompany(int idEmpresa);
        Task<ViewTipoMovimientoDto> GetById(int id);
        Task<List<ViewTipoMovimientoDto>> GetAll();
        Task Delete(int Id);
        Task<int> Add(CreateTipoMovimientoDto model);

    }
}