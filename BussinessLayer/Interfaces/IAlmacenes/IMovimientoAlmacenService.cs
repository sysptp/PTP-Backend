using BussinessLayer.Interface.IOtros;
using DataLayer.Models.Almacen;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BussinessLayer.Interface.IAlmacenes
{
    public interface IMovimientoAlmacenService : IBaseService<MovimientoAlmacen>
    {
         Task Create(MovimientoAlmacen mov, List<DetalleMovimientoAlmacen> dma, string fechaLimite, decimal montoInicial);
    }
}