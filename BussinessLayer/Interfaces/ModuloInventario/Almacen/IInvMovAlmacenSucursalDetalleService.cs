using BussinessLayer.DTOs.ModuloInventario.Almacenes;
using BussinessLayer.Interfaces.IOtros;
using DataLayer.Models.ModuloInventario.Almacen;

namespace BussinessLayer.Interfaces.ModuloInventario.Almacen
{
    public interface IInvMovAlmacenSucursalDetalleService : IGenericService<InvMovAlmacenSucursalDetalleRequest, InvMovAlmacenSucursalDetalleReponse, InvMovAlmacenSucursalDetalle>
    {
    }
}
