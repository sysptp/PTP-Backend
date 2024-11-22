using AutoMapper;
using BussinessLayer.DTOs.Configuracion.Account;
using BussinessLayer.DTOs.Configuracion.Seguridad.Usuario;
using BussinessLayer.Interface.IAccount;
using BussinessLayer.Interfaces.ISeguridad;
using BussinessLayer.Interfaces.Repository.Seguridad;
using DataLayer.Models.Seguridad;

namespace BussinessLayer.Services.SSeguridad.SUsuario
{
    public class UsuarioService : GenericService<RegisterRequest, UserResponse, Usuario>, IUsuarioService
    {
        private readonly IAccountService _accountService;
        private readonly IMapper _mapper;
        private readonly IUsuarioRepository _repository;

        public UsuarioService(IUsuarioRepository repository, IMapper mapper, IAccountService accountService) : base(repository, mapper)
        {
            _accountService = accountService;
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<List<UserResponse>> GetAllWithFilters(long? companyId, long? sucursalId, int? roleId)
        {
           var users = await _accountService.GetAllUsers();

            if (companyId != null)
            {
                users = users.Where(x => x.CompanyId == companyId).ToList();
            }

            if (sucursalId != null)
            {
                users = users.Where(x => x.SucursalId == sucursalId).ToList();
            }

            if (roleId != null && roleId != 0)
            {
                users = users.Where(x => x.RoleId == roleId).ToList();
            }

            return users;
        }

        public override async Task<UserResponse> GetByIdResponse(int id)
        {
            var users = await _accountService.GetAllUsers();
            return users.FirstOrDefault(x => x.Id == id);
        }

        public async Task UpdateUser(UpdateUserRequest request)
        {
            var user = _mapper.Map<Usuario>(request);
            await _repository.Update(user,user.Id);
        }
    }
}
