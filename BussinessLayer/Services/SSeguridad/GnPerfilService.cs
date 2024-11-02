using AutoMapper;
using BussinessLayer.DTOs.Seguridad;
using BussinessLayer.Interface.IAccount;
using BussinessLayer.Interfaces.ISeguridad;
using BussinessLayer.Repository.RSeguridad;
using DataLayer.Models.Entities;

namespace BussinessLayer.Services.SSeguridad
{
    public class GnPerfilService : GenericService<GnPerfilRequest,GnPerfilResponse,GnPerfil>, IGnPerfilService
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
                await _roleService.CreateRoleAsync(vm.Name, vm.Descripcion, vm.IDEmpresa);
                return _mapper.Map<GnPerfilResponse>(vm);
            }
            catch (ArgumentException ex)
            {
                throw new ArgumentException(ex.Message, ex);
            }
        }

    }
}
