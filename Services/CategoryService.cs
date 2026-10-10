using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Ecommerce_Web_Api.DTOs;
using Ecommerce_Web_Api.Models;

namespace Ecommerce_Web_Api.Services
{
    public class CategoryService
    {

        private static readonly List<Category> _categories = new List<Category>();


        public List<CategoryReadDto> GetAllCategories()
        {
            var categoryList = _categories.Select(c => new CategoryReadDto
            {
                Id = c.Id,
                Name = c.Name,
                Description = c.Description ?? string.Empty,
                ImageUrl = c.ImageUrl,
                IsDeleted = c.IsDeleted,
                CreatedAt = c.CreatedAt,
                UpdatedAt = c.UpdatedAt
            }).ToList();

            return categoryList;
        }


        public CategoryReadDto CreateCategory(CategoryCreateDto categoryCreateDto)
        {
            var newCategory = new Category
            {
                Name = categoryCreateDto.Name,
                Description = categoryCreateDto.Description,
                ImageUrl = categoryCreateDto.ImageUrl
            };

            _categories.Add(newCategory);

            var categoryReadDto = new CategoryReadDto
            {
                Id = newCategory.Id,
                Name = newCategory.Name,
                Description = newCategory.Description ?? string.Empty,
                ImageUrl = newCategory.ImageUrl,
                IsDeleted = newCategory.IsDeleted,
                CreatedAt = newCategory.CreatedAt,
                UpdatedAt = newCategory.UpdatedAt
            };
            return categoryReadDto;

        }


        public CategoryReadDto? GetCategoryById(Guid id)
        {
            var foundCategory = _categories.FirstOrDefault(c => c.Id == id);
            if (foundCategory == null)
            {
                return null;
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
            return categoryDto;
        }

        public bool DeleteCategory(Guid id)
        {
            var foundCategory = _categories.FirstOrDefault(c => c.Id == id);
            if (foundCategory == null)
            {
                return false;
            }
            _categories.Remove(foundCategory);
            return true;

        }


        public CategoryReadDto? UpdateCategory(Guid id, CategoryUpdateDto categoryUpdateDto)
        {
            var foundCategory = _categories.FirstOrDefault(c => c.Id == id);
            if (foundCategory == null)
            {
                return null;
            }

            // Update the properties of the found category
            foundCategory.Name = categoryUpdateDto.Name ?? foundCategory.Name;
            foundCategory.Description = categoryUpdateDto.Description ?? foundCategory.Description;
            foundCategory.ImageUrl = categoryUpdateDto.ImageUrl ?? foundCategory.ImageUrl;
            foundCategory.UpdatedAt = DateTime.UtcNow;

            return new CategoryReadDto
            {
                Id = foundCategory.Id,
                Name = foundCategory.Name,
                Description = foundCategory.Description ?? string.Empty,
                ImageUrl = foundCategory.ImageUrl,
                IsDeleted = foundCategory.IsDeleted,
                CreatedAt = foundCategory.CreatedAt,
                UpdatedAt = foundCategory.UpdatedAt
            };
        }


    }
}