using System.Linq.Expressions;
using TaskManager.Core.DTOs.Requests.Common;

namespace TaskManager.Core.Interfaces
{
    public interface IRepository<T> : IDisposable where T : class
    {
        Task<Guid> CreateAsync(T entity);
        Task<List<T>> GetWhereAsync(Expression<Func<T, bool>> predicate, SearchOptions? searchOptions = null);
        Task<List<T>> GetListAsync(SearchOptions? searchOptions = null);
        Task<T?> GetByGuidAsync(Guid guid);
        Task<T?> GetSingleBy(Expression<Func<T, bool>> predicate);
        Task<bool> UpdateAsync(T entity);
        Task<bool> UpdateRangeAsync(IEnumerable<T> entities);
        Task<bool> DeleteAsync(T entity);
    }
}
