using AutoMapper;
using BussinessLayer.DTOs.Configuracion.Menu;
using BussinessLayer.Interfaces.ModuloGeneral.Menu;
using BussinessLayer.Interfaces.Repository.Configuracion.Menu;
using DataLayer.Models.MenuApp;

namespace BussinessLayer.Services.ModuloGeneral.Menu
{

    // CREADO POR DOMINGO 3/11/2024
    public class GnMenuService : GenericService<SaveGnMenuRequest, GnMenuResponse, GnMenu>, IGnMenuService
    {
        private readonly IGnMenuRepository _menuRepository;
        private readonly IMapper _mapper;

        public GnMenuService(IGnMenuRepository repository, IMapper mapper) : base(repository, mapper)
        {
            _menuRepository = repository;
            _mapper = mapper;
        }

        public async Task<List<GnMenuResponse>> GetMenuHierarchy(int? RoleId, long? companyId, bool isHierarchy)
        {
            var result = await _menuRepository.ExecuteStoredProcedureAsync<GnMenu>(
                "sp_GetMenuHierarchy",
                new { CompanyId = companyId, ProfileId = RoleId }
            );

            return isHierarchy ? BuildMenuHierarchy(result) : _mapper.Map<List<GnMenuResponse>>(result);

        }

        private List<GnMenuResponse> BuildMenuHierarchy(IEnumerable<GnMenu> menus, int? parentId = 0)
        {
            return menus
                .Where(menu => menu.MenuPadre == parentId)
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
                    Query = menu.Consultar,
                    Create = menu.Crear,
                    Edit = menu.Editar,
                    Delete = menu.Eliminar,
                    SubMenus = BuildMenuHierarchy(menus, menu.IDMenu)
                })
                .ToList();
        }

    }
}