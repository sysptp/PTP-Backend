using System.Collections.Generic;
using System.Threading.Tasks;
using DataLayer.Models;
using DataLayer.Models.Otros;

namespace BussinessLayer.Interface.IOtros
{
    public interface IVersionService : IBaseService<Versiones>
    {
        Task<IList<Versiones>> GetVersionesByMarca(int? id, long idempresa);
    }
}