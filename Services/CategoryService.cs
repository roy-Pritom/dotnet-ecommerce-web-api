using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Ecommerce_Web_Api.Context;
using Ecommerce_Web_Api.DTOs;
using Ecommerce_Web_Api.Interfaces;
using Ecommerce_Web_Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce_Web_Api.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly EcommerceWebApiDbContext _ecommerceWebApiDbContext;
        private readonly IMapper _mapper;

        public CategoryService(EcommerceWebApiDbContext ecommerceWebApiDbContext, IMapper mapper)
        {
            _ecommerceWebApiDbContext = ecommerceWebApiDbContext;
            _mapper = mapper;
        }

        public async Task<List<CategoryReadDto>> GetAllCategories(CancellationToken cancellationToken = default)
        {
            // Read-only query: no tracking, and project straight to the DTO in SQL
            return await _ecommerceWebApiDbContext.Categories
                .AsNoTracking()
                .OrderBy(c => c.Name)
                .ProjectTo<CategoryReadDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
        }

        public async Task<CategoryReadDto?> GetCategoryById(Guid id, CancellationToken cancellationToken = default)
        {
            return await _ecommerceWebApiDbContext.Categories
                .AsNoTracking()
                .Where(c => c.Id == id)
                .ProjectTo<CategoryReadDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<CategoryReadDto> CreateCategory(CategoryCreateDto categoryCreateDto, CancellationToken cancellationToken = default)
        {
            var newCategory = new Category(
                categoryCreateDto.Name,
                categoryCreateDto.Description,
                categoryCreateDto.ImageUrl);

            _ecommerceWebApiDbContext.Categories.Add(newCategory);
            await _ecommerceWebApiDbContext.SaveChangesAsync(cancellationToken);

            return _mapper.Map<CategoryReadDto>(newCategory);
        }

        public async Task<CategoryReadDto?> UpdateCategory(Guid id, CategoryUpdateDto categoryUpdateDto, CancellationToken cancellationToken = default)
        {
            var foundCategory = await _ecommerceWebApiDbContext.Categories.FindAsync([id], cancellationToken);
            if (foundCategory == null)
            {
                return null;
            }

            foundCategory.Update(
                categoryUpdateDto.Name,
                categoryUpdateDto.Description,
                categoryUpdateDto.ImageUrl);

            // The entity is already tracked by EF, so no Update() call is needed
            await _ecommerceWebApiDbContext.SaveChangesAsync(cancellationToken);

            return _mapper.Map<CategoryReadDto>(foundCategory);
        }




        public async Task<bool> DeleteCategory(Guid id, CancellationToken cancellationToken = default)
        {
            var foundCategory = await _ecommerceWebApiDbContext.Categories.FindAsync([id], cancellationToken);
            if (foundCategory == null)
            {
                return false;
            }

            _ecommerceWebApiDbContext.Categories.Remove(foundCategory);
            await _ecommerceWebApiDbContext.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}
