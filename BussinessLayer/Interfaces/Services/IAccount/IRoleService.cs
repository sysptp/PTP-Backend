namespace BussinessLayer.Interfaces.Services.IAccount
{
    public interface IRoleService
    {
        Task<dynamic> CreateRoleAsync(string roleName, string descripcion, long? idEmpresa);
    }
}