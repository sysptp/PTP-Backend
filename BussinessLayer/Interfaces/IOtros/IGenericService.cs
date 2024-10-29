using System.Collections.Generic;
using System.Threading.Tasks;

namespace BussinessLayer.Interface.IOtros
{
    public interface IGenericService<T> where T : class
    {
        Task<T> GetById(int id);
        Task<IList<T>> GetAll();
        Task Add(T entity);
        Task Update(T entity);
        Task Delete(int id);
    }
}
