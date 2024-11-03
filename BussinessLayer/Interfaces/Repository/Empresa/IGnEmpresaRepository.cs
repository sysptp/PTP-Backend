using BussinessLayer.Interfaces.Repositories;
using DataLayer.Models.Empresa;

namespace BussinessLayer.Interfaces.Repository.Empresa
{
    public interface IGnEmpresaRepository : IGenericRepository<GnEmpresa>
    {
        Task<GnEmpresa> GetByEmpCode(long empCode);
    }
}
