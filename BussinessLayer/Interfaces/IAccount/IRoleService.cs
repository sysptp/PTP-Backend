
namespace BussinessLayer.Interface.IAccount
{
    public interface IRoleService
    {
        Task<bool> CreateRoleAsync(string roleName, string descripcion, long? idEmpresa);
        Task<bool> RoleExistsAsync(string roleName);
    }
}