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

        public async Task<bool> CreateRoleAsync(string roleName, string descripcion, long? idEmpresa)
        {
            try
            {
                if (await _roleManager.RoleExistsAsync(roleName))
                    return false;

                var role = new GnPerfil
                {
                    Name = roleName,
                    NormalizedName = roleName.ToUpper(),
                    Descripcion = descripcion,
                    IDEmpresa = idEmpresa,
                    FechaAdicion = DateTime.Now,
                    UsuarioAdicion = "System"
                };

                var result = await _roleManager.CreateAsync(role);
                return result.Succeeded;
            }
            catch (Exception ex) 
            {
                throw new ArgumentException(ex.Message, ex);
            }
          
        }

        public async Task<bool> RoleExistsAsync(string roleName)
        {
            return await _roleManager.RoleExistsAsync(roleName);
        }
    }
}
