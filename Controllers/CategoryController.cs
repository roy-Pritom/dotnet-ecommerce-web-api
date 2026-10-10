using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Ecommerce_Web_Api.Common.Pagination;
using Ecommerce_Web_Api.Common.Responses;
using Ecommerce_Web_Api.DTOs;
using Ecommerce_Web_Api.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce_Web_Api.Controllers
{
    [Route("api/v1/categories/")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetCategories([FromQuery] PaginationQuery pagination, CancellationToken cancellationToken)
        {
            var categoryList = await _categoryService.GetAllCategories(pagination, cancellationToken);
            return Ok(ApiResponse<PagedResult<CategoryReadDto>>.SuccessResponse(categoryList, StatusCodes.Status200OK, "Categories retrieved successfully."));
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetCategoryById(Guid id, CancellationToken cancellationToken)
        {
            var category = await _categoryService.GetCategoryById(id, cancellationToken);
            if (category == null)
            {
                return CategoryNotFound(id);
            }
            return Ok(ApiResponse<CategoryReadDto>.SuccessResponse(category, StatusCodes.Status200OK, "Category retrieved successfully."));
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory([FromBody] CategoryCreateDto categoryCreateDto, CancellationToken cancellationToken)
        {
            var categoryReadDto = await _categoryService.CreateCategory(categoryCreateDto, cancellationToken);
            return CreatedAtAction(
                nameof(GetCategoryById),
                new { id = categoryReadDto.Id },
                ApiResponse<CategoryReadDto>.SuccessResponse(categoryReadDto, StatusCodes.Status201Created, "Category created successfully."));
        }

        // PUT replaces the whole category: all required fields must be sent
        [HttpPatch("{id:guid}")]
        public async Task<IActionResult> UpdateCategory(Guid id, [FromBody] CategoryUpdateDto categoryUpdateDto, CancellationToken cancellationToken)
        {
            var categoryReadDto = await _categoryService.UpdateCategory(id, categoryUpdateDto, cancellationToken);
            if (categoryReadDto == null)
            {
                return CategoryNotFound(id);
            }
            return Ok(ApiResponse<CategoryReadDto>.SuccessResponse(categoryReadDto, StatusCodes.Status200OK, "Category updated successfully."));
        }



        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteCategory(Guid id, CancellationToken cancellationToken)
        {
            var deleted = await _categoryService.DeleteCategory(id, cancellationToken);
            if (!deleted)
            {
                return CategoryNotFound(id);
            }
            return NoContent();
        }

        private NotFoundObjectResult CategoryNotFound(Guid id) =>
            NotFound(ApiResponse<object>.ErrorResponse(
                new List<string> { $"Category with ID {id} not found." },
                StatusCodes.Status404NotFound,
                "Category not found."));
    }
}
