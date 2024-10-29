using BussinessLayer.Interface.IOtros;
using DataLayer.Models.Almacen;

namespace BussinessLayer.Interface.IAlmacenes
{
    public interface IAlmacenesService : IBaseService<Almacenes>
    {
        Task<IList<Almacenes>> GetPrincipal(long idEmpresa);

    }
}