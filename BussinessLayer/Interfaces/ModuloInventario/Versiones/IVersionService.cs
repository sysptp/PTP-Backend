using BussinessLayer.Interface.IOtros;
using DataLayer.Models.ModuloInventario.Version;


public interface IVersionService : IBaseService<Versiones>
{
    Task<IList<Versiones>> GetVersionesByMarca(int? id, long idempresa);
}
