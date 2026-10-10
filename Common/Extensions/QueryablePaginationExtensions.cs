using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Ecommerce_Web_Api.Common.Pagination;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce_Web_Api.Common.Extensions
{
    public static class QueryablePaginationExtensions
    {
        // Runs COUNT + a Skip/Take query in the database. The source must already be
        // ordered (OrderBy...), otherwise page contents are not stable between requests.
        public static async Task<PagedResult<T>> ToPagedResultAsync<T>(
            this IQueryable<T> source,
            PaginationQuery pagination,
            CancellationToken cancellationToken = default)
        {
            var totalCount = await source.CountAsync(cancellationToken);

            var items = await source
                .Skip((pagination.PageNumber - 1) * pagination.PageSize)
                .Take(pagination.PageSize)
                .ToListAsync(cancellationToken);

            return new PagedResult<T>(items, totalCount, pagination.PageNumber, pagination.PageSize);
        }
    }
}
