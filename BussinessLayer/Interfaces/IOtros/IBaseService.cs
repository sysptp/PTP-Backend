using System.Collections.Generic;
using System.Threading.Tasks;

namespace BussinessLayer.Interface.IOtros
{
    public interface IBaseService <T> where T: class
    {
        Task Add(T entity);

        Task Edit(T entity);

        Task<T> GetById(int id, long idEmpresa);

        Task<IList<T>> GetAll(long idEmpresa);

        Task Delete(int id, long idEmpresa);
    }
}