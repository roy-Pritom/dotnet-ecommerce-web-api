using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Ecommerce_Web_Api.DTOs;
using Ecommerce_Web_Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce_Web_Api.Controllers
{
    [Route("api/categories/")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private static List<Category> categories = new List<Category>();


        [HttpGet]
        public IActionResult GetCategories([FromQuery] string searchTerm = "")
        {
            // var searchedCategories = categories.Where(c => !string.IsNullOrEmpty(c.Name) && c.Name.Contains(searchTerm, StringComparison.OrdinalIgnoreCase)).ToList();

            // return Ok(searchedCategories);

            var categoryList = categories.Select(c => new CategoryReadDto
            {
                Id = c.Id,
                Name = c.Name,
                Description = c.Description ?? string.Empty,
                ImageUrl = c.ImageUrl,
                IsDeleted = c.IsDeleted,
                CreatedAt = c.CreatedAt,
                UpdatedAt = c.UpdatedAt
            }).ToList();

            return Ok(categoryList);
        }


        [HttpPost]
        public IActionResult CreateCategory([FromBody] CategoryCreateDto categoryData)
        {
            Console.WriteLine($"Received category data: {JsonSerializer.Serialize(categoryData, new JsonSerializerOptions { WriteIndented = true })}");

            // Console.WriteLine($"Category Name: {(string.IsNullOrEmpty(categoryData.Name) ? "null or empty" : categoryData.Name)}");

            if (string.IsNullOrEmpty(categoryData.Name))
            {
                return BadRequest("Category name is required");
            }

            var newCategory = new Category
            {
                Name = categoryData.Name,
                Description = categoryData.Description,
                ImageUrl = categoryData.ImageUrl
            };

            categories.Add(newCategory);

            var createdCategoryDto = new CategoryReadDto
            {
                Id = newCategory.Id,
                Name = newCategory.Name,
                Description = newCategory.Description ?? string.Empty,
                ImageUrl = newCategory.ImageUrl,
                IsDeleted = newCategory.IsDeleted,
                CreatedAt = newCategory.CreatedAt,
                UpdatedAt = newCategory.UpdatedAt
            };

            return Created($"/api/categories/{newCategory.Id}", createdCategoryDto);
        }


        [HttpGet("{id:guid}")]
        public IActionResult GetCategoryById(Guid id)
        {
            var foundCategory = categories.FirstOrDefault(c => c.Id == id);
            if (foundCategory == null)
            {
                return NotFound("Category not found");
            }
            var categoryDto = new CategoryReadDto
            {
                Id = foundCategory.Id,
                Name = foundCategory.Name,
                Description = foundCategory.Description ?? string.Empty,
                ImageUrl = foundCategory.ImageUrl,
                IsDeleted = foundCategory.IsDeleted,
                CreatedAt = foundCategory.CreatedAt,
                UpdatedAt = foundCategory.UpdatedAt
            };
            return Ok(categoryDto);
        }

        [HttpDelete("{id:guid}")]
        public IActionResult DeleteCategory(Guid id)
        {
            var foundCategory = categories.FirstOrDefault(c => c.Id == id);
            if (foundCategory == null)
            {
                return NotFound("Category Which you are trying to delete is not found");
            }
            categories.Remove(foundCategory);
            return NoContent();
        }

        [HttpPut("{id:guid}")]
        public IActionResult UpdateCategory(Guid id, [FromBody] CategoryUpdateDto updatedCategory)
        {
            var foundCategory = categories.FirstOrDefault(c => c.Id == id);
            if (foundCategory == null)
            {
                return NotFound("Category not found");
            }
            if (updatedCategory == null)
            {
                return BadRequest("Invalid category data");
            }

            // Update the properties of the found category
            foundCategory.Name = updatedCategory.Name ?? foundCategory.Name;
            foundCategory.Description = updatedCategory.Description ?? foundCategory.Description;
            foundCategory.ImageUrl = updatedCategory.ImageUrl ?? foundCategory.ImageUrl;


            return Ok(foundCategory);
        }
    }
}