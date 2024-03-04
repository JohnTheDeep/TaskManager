using Microsoft.EntityFrameworkCore;

namespace DataMaster.Core.Pagination
{
    public static class Extensions
    {
        public static async Task<Page<T>> PaginateAsync<T>(this IQueryable<T> query, int skip, int take, CancellationToken cancellationToken = default)
        {
            int totalCount = await query.CountAsync(cancellationToken);

            return new Page<T>(await query.Skip(skip).Take(take).ToListAsync(cancellationToken), totalCount);
        }
    }
}
