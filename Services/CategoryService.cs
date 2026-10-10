using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Ecommerce_Web_Api.DTOs;
using Ecommerce_Web_Api.Interfaces;
using Ecommerce_Web_Api.Models;

namespace Ecommerce_Web_Api.Services
{
    public class CategoryService : ICategoryService
    {

        private static readonly List<Category> _categories = new List<Category>();
        private readonly AutoMapper.IMapper _mapper;

        public CategoryService(AutoMapper.IMapper mapper)
        {
            _mapper = mapper;
        }

        public List<CategoryReadDto> GetAllCategories()
        {
            var categoryList = _mapper.Map<List<CategoryReadDto>>(_categories);
            return categoryList;
        }


        public CategoryReadDto CreateCategory(CategoryCreateDto categoryCreateDto)
        {
            // var newCategory = new Category
            // {
            //     Name = categoryCreateDto.Name,
            //     Description = categoryCreateDto.Description,
            //     ImageUrl = categoryCreateDto.ImageUrl
            // };
            var newCategory = _mapper.Map<Category>(categoryCreateDto);
            _categories.Add(newCategory);

            return _mapper.Map<CategoryReadDto>(newCategory);

        }


        public CategoryReadDto? GetCategoryById(Guid id)
        {
            var foundCategory = _categories.FirstOrDefault(c => c.Id == id);
            if (foundCategory == null)
            {
                return null;
            }
            var categoryDto = _mapper.Map<CategoryReadDto>(foundCategory);
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
            // foundCategory.Name = categoryUpdateDto.Name ?? foundCategory.Name;
            // foundCategory.Description = categoryUpdateDto.Description ?? foundCategory.Description;
            // foundCategory.ImageUrl = categoryUpdateDto.ImageUrl ?? foundCategory.ImageUrl;

            _mapper.Map(categoryUpdateDto, foundCategory);

            return _mapper.Map<CategoryReadDto>(foundCategory);
        }


    }
}