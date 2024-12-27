using AutoMapper;
using BussinessLayer.DTOs.ModuloGeneral.Seguridad.Perfil;
using BussinessLayer.Interfaces.Repository.ModuloGeneral.Seguridad;
using BussinessLayer.Interfaces.Services.IAccount;
using BussinessLayer.Interfaces.Services.ModuloGeneral.Seguridad;
using DataLayer.Models.ModuloGeneral.Seguridad;

namespace BussinessLayer.Services.ModuloGeneral.Seguridad
{
    public class GnPerfilService : GenericService<GnPerfilRequest, GnPerfilResponse, GnPerfil>, IGnPerfilService
    {
        private readonly IGnPerfilRepository _repository;
        private readonly IMapper _mapper;
        private readonly IRoleService _roleService;

        public GnPerfilService(IGnPerfilRepository repository, IMapper mapper, IRoleService roleService) : base(repository, mapper)
        {
            _repository = repository;
            _mapper = mapper;
            _roleService = roleService;
        }

        public override async Task<GnPerfilResponse> Add(GnPerfilRequest vm)
        {
            try
            {
                var response = await _roleService.CreateRoleAsync(vm.Name, vm.Descripcion, vm.CompanyId);
                return MapDynamicToPerfilResponse(response);
            }
            catch (ArgumentException ex)
            {
                throw new ArgumentException(ex.Message, ex);
            }
        }

        public GnPerfilResponse MapDynamicToPerfilResponse(dynamic request)
        {
            var perfil = new GnPerfilResponse()
            {
                CompanyId = request.IDEmpresa,
                Name = request.Name,
                Descripcion = request.Descripcion,
                IdRole = request.Id,
            };

            return perfil;
        }
    }
}
