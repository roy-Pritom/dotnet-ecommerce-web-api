using System.ComponentModel.DataAnnotations;

namespace Ecommerce_Web_Api.Common.Pagination
{
    // Bound from the query string: ?pageNumber=1&pageSize=10
    // Reuse it on any list endpoint (or inherit from it to add filters/search).
    public class PaginationQuery
    {
        public const int DefaultPageSize = 10;
        public const int MaxPageSize = 100;

        [Range(1, int.MaxValue, ErrorMessage = "PageNumber must be 1 or greater.")]
        public int PageNumber { get; set; } = 1;

        [Range(1, MaxPageSize, ErrorMessage = "PageSize must be between 1 and 100.")]
        public int PageSize { get; set; } = DefaultPageSize;
    }
}
