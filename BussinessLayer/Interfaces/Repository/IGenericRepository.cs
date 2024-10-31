
namespace BussinessLayer.Interfaces.Repositories
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T> GetById(int id);
        Task<IList<T>> GetAll();
        Task<T> Add(T entity);
        Task Update(T entity);
        Task Delete(int id);
    }
}