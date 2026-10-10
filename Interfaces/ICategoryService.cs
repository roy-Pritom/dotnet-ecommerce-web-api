using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce_Web_Api.DTOs;

namespace Ecommerce_Web_Api.Interfaces
{
    public interface ICategoryService
    {
        List<CategoryReadDto> GetAllCategories();

        CategoryReadDto CreateCategory(CategoryCreateDto categoryCreateDto);
        CategoryReadDto? GetCategoryById(Guid id);
        bool DeleteCategory(Guid id);

        CategoryReadDto? UpdateCategory(Guid id, CategoryUpdateDto categoryUpdateDto);

    }
}