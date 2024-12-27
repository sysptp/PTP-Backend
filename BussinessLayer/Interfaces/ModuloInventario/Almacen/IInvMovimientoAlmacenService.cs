using BussinessLayer.DTOs.ModuloInventario.Almacenes;
using BussinessLayer.Interfaces.Services.IOtros;
using DataLayer.Models.ModuloInventario.Almacen;


namespace BussinessLayer.Interfaces.ModuloInventario.Almacen
{
    public interface IInvMovimientoAlmacenService : IGenericService<InvMovimientoAlmacenRequest, InvMovimientoAlmacenReponse, InvMovimientoAlmacen>
    {
    }
}
