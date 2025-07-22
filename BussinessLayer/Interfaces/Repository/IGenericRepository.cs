
using System.Linq.Expressions;
using BussinessLayer.DTOs.Common;

namespace BussinessLayer.Interfaces.Repositories
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T> GetById(object id);
        Task<IList<T>> GetAll();
        Task<T> Add(T entity);
        Task AddRangeAsync(IEnumerable<T> entities);
        Task Update(T entity, object id);
        Task Delete(object id);
        Task<IEnumerable<TResult>> ExecuteStoredProcedureAsync<TResult>(string storedProcedure, object parameters = null);
        Task<List<T>> GetAllWithIncludeAsync(List<string> properties);
        Task<T> GetAllWithIncludeByIdAsync(object id, List<string>? properties = null);
        Task AddRangeCompositeKeyAsync(IEnumerable<T> entities);
        Task<(IList<T> Data, int TotalCount)> GetAllWithIncludePaginatedAsync(
           List<string> includeProperties,
           PaginationRequest pagination,
           Expression<Func<T, bool>>? filter = null);
        Task<(IList<T> Data, int TotalCount)> GetAllPaginatedAsync(
   PaginationRequest pagination,
   Expression<Func<T, bool>>? filter = null,
   Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
   string includeProperties = "");
    }
}