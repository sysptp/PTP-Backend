using System.Collections.Generic;
using System.Threading.Tasks;
using BussinessLayer.Interface.IOtros;
using DataLayer.Models;
using DataLayer.Models.ModuloInventario;

namespace BussinessLayer.Interfaces.ModuloInventario
{
    public interface IVersionService : IBaseService<Versiones>
    {
        Task<IList<Versiones>> GetVersionesByMarca(int? id, long idempresa);
    }
}