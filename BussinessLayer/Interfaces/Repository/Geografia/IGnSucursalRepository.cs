using BussinessLayer.Interfaces.Repositories;
using DataLayer.Models.Empresa;

namespace BussinessLayer.Interfaces.Repository.Geografia
{
    public interface IGnSucursalRepository : IGenericRepository<GnSucursal>
    {
        Task<GnSucursal> GetBySucursalCode(long? id);
    }
}
