
namespace BussinessLayer.Interfaces.Repositories
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T> GetById(int id);
        Task<T> GetById(object id);
        Task<IList<T>> GetAll();
        Task<T> Add(T entity);
        Task AddRangeAsync(IEnumerable<T> entities);
        Task Update(T oldentity, int id);
        Task Update(T entity, object id);
        Task Delete(int id);
        Task Delete(object id);
        Task<IEnumerable<TResult>> ExecuteStoredProcedureAsync<TResult>(string storedProcedure, object parameters = null);
        Task<List<T>> GetAllWithIncludeAsync(List<string> properties);
    }
}