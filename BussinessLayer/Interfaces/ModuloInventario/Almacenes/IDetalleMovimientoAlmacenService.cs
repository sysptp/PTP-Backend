using BussinessLayer.Interface.IOtros;
using DataLayer.Models.ModuloInventario.Almacen;

public interface IDetalleMovimientoAlmacenService : IBaseService<DetalleMovimientoAlmacen>
{
    Task<IEnumerable<DetalleMovimientoAlmacen>> GetDetalleMovimientoByMovimientoId(int id, long idEmpresa);
}
