using AutoMapper;
using BussinessLayer.DTOs.Configuracion.Menu;
using BussinessLayer.Interfaces.IMenu;
using BussinessLayer.Interfaces.Repository.Configuracion.Menu;
using DataLayer.Models.MenuApp;

namespace BussinessLayer.Services.SMenu
{

    // CREADO POR DOMINGO 3/11/2024
    public class GnMenuService : GenericService<SaveGnMenuRequest,GnMenuResponse, GnMenu>, IGnMenuService
    {
        private readonly IGnMenuRepository _menuRepository;
        private readonly IMapper _mapper;

        public GnMenuService(IGnMenuRepository repository, IMapper mapper) : base(repository, mapper)
        {
            _menuRepository = repository;
            _mapper = mapper;
        }

        public async Task<List<GnMenuResponse>> GetMenuHierarchy(int? RoleId, long? companyId)
        {
            var result = await _menuRepository.ExecuteStoredProcedureAsync<GnMenu>(
                "sp_GetMenuHierarchy",
                new { CompanyId = companyId, ProfileId = RoleId }
            );

            return _mapper.Map<List<GnMenuResponse>>(result);
        }

    }
}