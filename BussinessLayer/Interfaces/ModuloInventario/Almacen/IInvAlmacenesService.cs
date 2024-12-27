using BussinessLayer.DTOs.ModuloInventario.Almacenes;
using BussinessLayer.Interfaces.IOtros;
using DataLayer.Models.ModuloInventario.Almacen;


namespace BussinessLayer.Interfaces.ModuloInventario.Almacen
{
    public interface IInvAlmacenesService : IGenericService<InvAlmacenesRequest, InvAlmacenesReponse, InvAlmacenes>
    {
    }
}
