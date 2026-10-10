using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Ecommerce_Web_Api.Common.Pagination;
using Ecommerce_Web_Api.DTOs;

namespace Ecommerce_Web_Api.Interfaces
{
    public interface ICategoryService
    {
        Task<PagedResult<CategoryReadDto>> GetAllCategories(PaginationQuery pagination, CancellationToken cancellationToken = default);

        Task<CategoryReadDto?> GetCategoryById(Guid id, CancellationToken cancellationToken = default);

        Task<CategoryReadDto> CreateCategory(CategoryCreateDto categoryCreateDto, CancellationToken cancellationToken = default);

        Task<CategoryReadDto?> UpdateCategory(Guid id, CategoryUpdateDto categoryUpdateDto, CancellationToken cancellationToken = default);


        Task<bool> DeleteCategory(Guid id, CancellationToken cancellationToken = default);
    }
}
