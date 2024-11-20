
using DataLayer.Models.Entities;

namespace BussinessLayer.Interface.IAccount
{
    public interface IRoleService
    {
        Task<dynamic> CreateRoleAsync(string roleName, string descripcion, long? idEmpresa);
    }
}