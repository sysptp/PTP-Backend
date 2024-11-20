using Microsoft.AspNetCore.Identity;
using IdentityLayer.Entities;
using BussinessLayer.Interface.IAccount;

namespace IdentityLayer.Services
{
    public class RoleService : IRoleService
    {
        private readonly RoleManager<GnPerfil> _roleManager;

        public RoleService(RoleManager<GnPerfil> roleManager)
        {
            _roleManager = roleManager;
        }

        public async Task<dynamic> CreateRoleAsync(string roleName, string descripcion, long? idEmpresa)
        {
            try
            {
                var role = new GnPerfil
                {
                    Name = roleName,
                    NormalizedName = roleName.ToUpper(),
                    Descripcion = descripcion,
                    IDEmpresa = idEmpresa,
                    FechaAdicion = DateTime.Now,
                    UsuarioAdicion = "System"
                };

                await _roleManager.CreateAsync(role);
                var result = await _roleManager.FindByNameAsync(roleName);
                role.Id = result.Id;
                return role;
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message, ex);
            }

        }
    }
}
