using AutoMapper;
using BussinessLayer.DTOs.Configuracion.Menu;
using BussinessLayer.Interfaces.IMenu;
using BussinessLayer.Interfaces.Repositories;
using BussinessLayer.Interfaces.Repository.Configuracion.Menu;
using DataLayer.Models.MenuApp;

namespace BussinessLayer.Services.SMenu
{

    // CREADO POR DOMINGO 3/11/2024
    public class GnMenuService : GenericService<SaveGnMenuRequest,GnMenuResponse, GnMenu>, IGnMenuService
    {
        private readonly IGnMenuRepository _menuRepository;

        public GnMenuService(IGnMenuRepository repository, IMapper mapper) : base(repository, mapper)
        {
            _menuRepository = repository;
        }

        public async Task<List<GnMenuResponse>> GetMenuHierarchy(int RoleId, long companyId)
        {
            var result = await _menuRepository.ExecuteStoredProcedureAsync<GnMenu>(
                "sp_GetMenuHierarchy",
                new { CompanyId = companyId, ProfileId = RoleId }
            );

            var menus = BuildMenuHierarchy(result);
            return menus;
        }

        private List<GnMenuResponse> BuildMenuHierarchy(IEnumerable<GnMenu> menus, int? parentId = 0)
        {
            return menus
                .Where(menu => menu.menupadre == parentId)
                .OrderBy(menu => menu.Orden)
                .Select(menu => new GnMenuResponse
                {
                    MenuID = menu.IDMenu,
                    Name = menu.Menu,
                    Level = menu.Nivel,
                    Order = menu.Orden,
                    Url = menu.URL,
                    Icon = menu.MenuIcon,
                    ModuleID = menu.IdModulo,
                    SubMenus = BuildMenuHierarchy(menus, menu.IDMenu)
                })
                .ToList();
        }

    }
}