using AutoMapper;
using BussinessLayer.DTOs.ModuloGeneral.Configuracion.Account;
using BussinessLayer.DTOs.ModuloGeneral.Seguridad.Usuario;
using BussinessLayer.Interfaces.Repository.ModuloGeneral.Seguridad;
using BussinessLayer.Interfaces.Services.ModuloGeneral.Seguridad;
using DataLayer.Models.ModuloGeneral.Seguridad;

namespace BussinessLayer.Services.ModuloGeneral.Seguridad
{
    public class UsuarioService : GenericService<RegisterRequest, UserResponse, Usuario>, IUsuarioService
    {
        private readonly IMapper _mapper;
        private readonly IUsuarioRepository _repository;

        public UsuarioService(IUsuarioRepository repository, IMapper mapper) : base(repository, mapper)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<List<UserResponse>> GetAllWithFilters(long? companyId, long? sucursalId, int? roleId, bool? areActive)
        {
            var users = await _repository.GetAllWithIncludeAsync(new List<string> { "GnEmpresa", "GnSucursal", "GnPerfil" });

            if (companyId != null)
            {
                users = users.Where(x => x.GnEmpresa != null && x.GnEmpresa.CODIGO_EMP == companyId).ToList();
            }

            if (sucursalId != null)
            {
                users = users.Where(x => x.GnSucursal != null && x.GnSucursal.CodigoSuc == sucursalId).ToList();
            }

            if (roleId != null && roleId != 0)
            {
                users = users.Where(x => x.GnPerfil != null && x.GnPerfil.Id == roleId).ToList();
            }

            if (areActive != null)
            {
                users = users.Where(x => x.IsActive == areActive).ToList();
            }

            return _mapper.Map<List<UserResponse>>(users);
        }

        public override async Task<UserResponse> GetByIdResponse(int id)
        {
            var users = await GetAllWithFilters(null,null,null,null);
            return users.FirstOrDefault(x => x.Id == id);
        }

        public async Task<UserResponse> GetByUserNameResponse(string userName)
        {
            var users = await GetAllWithFilters(null, null, null, null);
            return users.FirstOrDefault(x => x.UserName == userName);
        }

        public async Task UpdateUser(UpdateUserRequest request)
        {
            var user = _mapper.Map<Usuario>(request);
            await _repository.Update(user, user.Id);
        }
    }
}
