using BussinessLayer.DTOs.ModuloInventario.Suplidores;
using BussinessLayer.Interface.IOtros;

namespace BussinessLayer.Interfaces.ModuloInventario.Suplidores
{
    public interface IContactosSuplidoresService
    {
        Task Update(EditContactosSuplidoresDto model);
        Task<List<ViewContactosSuplidoresDto>> GetByCompany(int idEmpresa);
        Task<ViewContactosSuplidoresDto> GetById(int id);
        Task<List<ViewContactosSuplidoresDto>> GetAll();
        Task Delete(int Id);
        Task<int> Add(CreateContactosSuplidoresDto model);
    }
}