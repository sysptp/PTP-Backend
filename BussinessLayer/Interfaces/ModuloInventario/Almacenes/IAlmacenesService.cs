using BussinessLayer.Interface.IOtros;
using DataLayer.Models.ModuloInventario.Almacen;

public interface IAlmacenesService : IBaseService<Almacenes>
{
    Task<IList<Almacenes>> GetPrincipal(long idEmpresa);

}
