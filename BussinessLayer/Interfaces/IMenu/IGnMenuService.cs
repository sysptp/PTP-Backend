using BussinessLayer.DTOs.Configuracion.Menu;
using BussinessLayer.Interfaces.IOtros;
using DataLayer.Models.MenuApp;

namespace BussinessLayer.Interfaces.IMenu
{

    // CREADO POR DOMINGO 3/11/2024 - INTERFAZ PARA EL MANEJO DEL MENU
    public interface IGnMenuService : IGenericService<SaveGnMenuRequest, GnMenuResponse, GnMenu>
    {
        Task<List<GnMenuResponse>> GetMenuHierarchy(int? RoleId, long? companyId);
    }
}
