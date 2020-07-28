using System.Linq;

namespace Rumox.API.Extensions
{
    public static class QueryableExtensions
    {
        public static IQueryable<T> Paginar<T>(this IQueryable<T> queryable, int offset, int limit)
        {
            return queryable
                .Skip(offset)
                .Take(limit);
        }
    }
}
