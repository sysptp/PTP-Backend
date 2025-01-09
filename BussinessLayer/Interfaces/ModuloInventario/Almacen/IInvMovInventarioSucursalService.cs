using BussinessLayer.DTOs.ModuloInventario.Almacenes;
using BussinessLayer.Interfaces.Services.IOtros;
using DataLayer.Models.ModuloInventario.Almacen;

namespace BussinessLayer.Interfaces.ModuloInventario.Almacen
{
    public interface IInvMovInventarioSucursalService : IGenericService<InvMovInventarioSucursalRequest, InvMovInventarioSucursalReponse, InvMovInventarioSucursal>
    {
        Task<List<InvMovInventarioSucursalReponse>> GetAllByFilters(long? idSucursal, long? idCompany);
    }
}
