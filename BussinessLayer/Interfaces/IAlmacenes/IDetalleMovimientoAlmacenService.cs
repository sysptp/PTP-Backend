using BussinessLayer.Interface.IOtros;
using DataLayer.Models.Almacen;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BussinessLayer.Interface.IAlmacenes
{
    public interface IDetalleMovimientoAlmacenService : IBaseService<DetalleMovimientoAlmacen>
    {
        Task<IEnumerable<DetalleMovimientoAlmacen>> GetDetalleMovimientoByMovimientoId(int id, long idEmpresa);
    }
}