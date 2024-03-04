using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TaskManager.Core.DTOs.Requests.Common;
using TaskManager.Core.Enums;
using TaskManager.Core.Interfaces;
using TaskManager.Core.Models;

namespace TaskManager.Infrastructure.Repositories
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly TaskManagerDbContext _dbContext;
        private readonly DbSet<T> _dbSet;

        private bool _disposed = false;

        public Repository(TaskManagerDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = dbContext.Set<T>();
        }

        public async System.Threading.Tasks.Task<Guid> CreateAsync(T entity)
        {
            _dbSet.Add(entity);

            await _dbContext.SaveChangesAsync();

            return entity.id;
        }

        public async Task<List<T>> GetListAsync(SearchOptions? searchOptions = null)
        {
            try
            {
                var query = _dbContext.Set<T>().AsQueryable();

                var navigations = _dbContext.Model.FindEntityType(typeof(T))?
                            .GetDerivedTypesInclusive()
                            .SelectMany(type => type.GetNavigations())
                            .Distinct();

                if (navigations?.Any() is true)
                    foreach (var property in navigations)
                        query = query.Include(property.Name);

                var data = query
                    .Skip(searchOptions?.From ?? 0)
                    .Take(searchOptions?.Size ?? 20);

                return await data.ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Failed get list", ex);
            }
        }

        public async Task<List<T>> GetWhereAsync(System.Linq.Expressions.Expression<Func<T, bool>> predicate, SearchOptions? searchOption = null)
        {
            try
            {
                var query = _dbContext.Set<T>().AsQueryable();

                var navigations = _dbContext.Model.FindEntityType(typeof(T))?
                            .GetDerivedTypesInclusive()
                            .SelectMany(type => type.GetNavigations())
                            .Distinct();

                if (navigations?.Any() is true)
                    foreach (var property in navigations)
                        query = query.Include(property.Name);

                var data = query
                    .Where(predicate)
                    .Skip(searchOption?.From ?? 0)
                    .Take(searchOption?.Size ?? 20);

                if (searchOption?.OrderOptions?.Any() is true)
                {
                    IOrderedQueryable<T>? orderedData = null;

                    foreach (var orderBy in searchOption.OrderOptions)
                    {
                        if (orderBy.OrderBy == OrderBy.Asc)
                        {
                            orderedData = orderedData == null ? data.OrderBy(_ => orderBy.OrderPropertyName) :
                                orderedData.OrderBy(_ => orderBy.OrderPropertyName);
                        }
                        else
                        {
                            orderedData = orderedData == null ? data.OrderByDescending(_ => orderBy.OrderPropertyName) :
                                orderedData.ThenByDescending(_ => orderBy.OrderPropertyName);
                        }
                    }

                    return await orderedData.ToListAsync();
                }

                return await data.ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Failed GetWhereAsync", ex);
            }
        }

        public async Task<bool> UpdateAsync(T entity)
        {
            _dbSet.Update(entity);

            await _dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<T?> GetByGuidAsync(Guid guid)
        {
            var query = _dbContext.Set<T>().AsQueryable();

            var navigations = _dbContext.Model.FindEntityType(typeof(T))?
                        .GetDerivedTypesInclusive()
                        .SelectMany(type => type.GetNavigations())
                        .Distinct();

            if (navigations?.Any() is true)
                foreach (var property in navigations)
                    query = query.Include(property.Name);

            return await query.SingleOrDefaultAsync(_ => _.id == guid);
        }

        public async Task<bool> DeleteAsync(T entity)
        {
            _dbSet.Remove(entity);

            await _dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<T?> GetSingleBy(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.SingleOrDefaultAsync(predicate);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _dbContext.Dispose();
                }
            }
            _disposed = true;
        }

        public async Task<bool> UpdateRangeAsync(IEnumerable<T> entities)
        {
            _dbSet.UpdateRange(entities);

            await _dbContext.SaveChangesAsync();

            return true;
        }
    }
}
