using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Ecommerce_Web_Api.Common.Responses;
using Ecommerce_Web_Api.DTOs;
using Ecommerce_Web_Api.Interfaces;
using Ecommerce_Web_Api.Models;
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
        public IActionResult GetCategories([FromQuery] string searchTerm = "")
        {
            // var searchedCategories = categories.Where(c => !string.IsNullOrEmpty(c.Name) && c.Name.Contains(searchTerm, StringComparison.OrdinalIgnoreCase)).ToList();
            var categoryList = _categoryService.GetAllCategories();
            return Ok(ApiResponse<List<CategoryReadDto>>.SuccessResponse(categoryList, StatusCodes.Status200OK, "Categories retrieved successfully."));
        }


        [HttpPost]
        public IActionResult CreateCategory([FromBody] CategoryCreateDto categoryCreateDto)
        {
            // Console.WriteLine($"Received category data: {JsonSerializer.Serialize(categoryData, new JsonSerializerOptions { WriteIndented = true })}");

            // Console.WriteLine($"Category Name: {(string.IsNullOrEmpty(categoryData.Name) ? "null or empty" : categoryData.Name)}");

            var categoryReadDto = _categoryService.CreateCategory(categoryCreateDto);
            return Created(nameof(GetCategoryById), ApiResponse<CategoryReadDto>.SuccessResponse(categoryReadDto, StatusCodes.Status201Created, "Category created successfully."));
        }


        [HttpGet("{id:guid}")]
        public IActionResult GetCategoryById(Guid id)
        {
            var category = _categoryService.GetCategoryById(id);
            if (category == null)
            {
                return NotFound(ApiResponse<object>.ErrorResponse(new List<string> { $"Category with ID {id} not found." }, StatusCodes.Status404NotFound, "Validation failed."));
            }
            return Ok(ApiResponse<CategoryReadDto>.SuccessResponse(category, StatusCodes.Status200OK, "Category retrieved successfully."));
        }

        [HttpDelete("{id:guid}")]
        public IActionResult DeleteCategory(Guid id)
        {
            var foundCategory = _categoryService.DeleteCategory(id);
            if (!foundCategory)
            {
                return NotFound(ApiResponse<object>.ErrorResponse(new List<string> { $"Category with ID {id} not found." }, StatusCodes.Status404NotFound, "Validation failed."));
            }
            // return NoContent();
            return Ok(ApiResponse<object>.SuccessResponse(null, StatusCodes.Status204NoContent, "Category deleted successfully."));
        }

        [HttpPut("{id:guid}")]
        public IActionResult UpdateCategory(Guid id, [FromBody] CategoryUpdateDto updatedCategory)
        {

            var categoryReadDto = _categoryService.UpdateCategory(id, updatedCategory);
            if (categoryReadDto == null)
            {
                return NotFound(ApiResponse<object>.ErrorResponse(new List<string> { $"Category with ID {id} not found." }, StatusCodes.Status404NotFound, "Validation failed."));
            }

            return Ok(ApiResponse<CategoryReadDto>.SuccessResponse(categoryReadDto, StatusCodes.Status200OK, "Category updated successfully."));
        }
    }
}