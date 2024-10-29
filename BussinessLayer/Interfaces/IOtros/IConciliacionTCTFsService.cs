using DataLayer.Models.Caja;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BussinessLayer.Interface.IOtros
{
    public interface IConciliacionTCTFsService
    {
        Task Add(ConciliacionTCTF model);

        Task Delete(ConciliacionTCTF model);

        Task<List<ConciliacionTCTF>> GetAll();

        Task<List<ConciliacionTCTF>> GetAllIndex();

        Task<ConciliacionTCTF> GetById(int id);

        Task Update(ConciliacionTCTF model);
    }
}
