using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce_Web_Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce_Web_Api.Controllers
{
    [Route("api/categories")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        List<Category> categories = new List<Category>();


        [HttpGet]
        public IActionResult GetCategories([FromQuery] string searchTerm)
        {
            var searchedCategories = categories.Where(c => !string.IsNullOrEmpty(c.Name) && c.Name.Contains(searchTerm, StringComparison.OrdinalIgnoreCase)).ToList();

            return Ok(searchedCategories);
        }


        [HttpPost]
        public IActionResult CreateCategory([FromBody] Category categoryData)
        {
            Console.WriteLine($"Received category data: {categoryData}");

            if (!string.IsNullOrEmpty(categoryData.Name))
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

            return Created($"/api/categories/{newCategory.Id}", newCategory);

        }


        [HttpGet("{id:guid}")]
        public IActionResult GetCategoryById(Guid id)
        {
            var foundCategory = categories.FirstOrDefault(c => c.Id == id);
            if (foundCategory == null)
            {
                return NotFound("Category not found");
            }
            return Ok(foundCategory);
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
        public IActionResult UpdateCategory(Guid id, [FromBody] Category updatedCategory)
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