using System.Collections.Generic;
using System.Threading.Tasks;
using DataLayer.Models;
using DataLayer.Models.Otros;

namespace BussinessLayer.Interface.IOtros
{
    public interface IDgiiNcfService : IBaseService<DgiiNcfSecuencia>
    {
        Task<IEnumerable<DgiiNcfSecuencia>> GetLastSecuence();

        Task<string> GetSeqNcfByTipoNcf(int tipoNcf);

        Task<DgiiNcfSecuencia> FindNcfBySequencial(string ncfSeq);

        Task<IList<DgiiNcf>> GetAllDgii();

        Task<DgiiNcf> GetByIdDgii(int id);

        Task AddDgii(DgiiNcf entity);

        Task EditDgii(DgiiNcf entity);
    }

}