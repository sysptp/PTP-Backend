using BussinessLayer.DTOs.ModuloGeneral.Menu;
using BussinessLayer.Interfaces.Services.IOtros;
using DataLayer.Models.ModuloGeneral.Menu;

namespace BussinessLayer.Interfaces.Services.ModuloGeneral.Menu
{

    // CREADO POR DOMINGO 3/11/2024 - INTERFAZ PARA EL MANEJO DEL MENU
    public interface IGnMenuService : IGenericService<SaveGnMenuRequest, GnMenuResponse, GnMenu>
    {
        Task<List<GnMenuResponse>> GetMenuHierarchy(int? RoleId, long? companyId, bool isHierarchy);
    }
}
