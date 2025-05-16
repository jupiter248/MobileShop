using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MainApi.Application.Dtos.Category;
using MainApi.Application.Interfaces.Repositories;
using MainApi.Application.Interfaces.Services;
using MainApi.Application.Mappers;
using MainApi.Domain.Models.Products;

namespace MainApi.Infrastructure.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<CategoryDto> AddCategoryAsync(AddCategoryRequestDto addCategoryRequestDto)
        {
            Category category = await _categoryRepository.AddCategoryAsync(addCategoryRequestDto.ToCategoryFromAddCategoryDto());
            return category.ToCategoryDto();
        }

        public async Task DeleteCategoryAsync(int categoryId)
        {
            Category? category = await _categoryRepository.RemoveCategoryAsync(categoryId);
            if (category == null)
            {
                throw new KeyNotFoundException("Category not found");
            }
        }

        public async Task<List<CategoryDto>> GetAllCategoriesAsync()
        {
            List<Category> categories = await _categoryRepository.GetAllCategoriesAsync();
            List<CategoryDto> categoryDtos = categories.Select(c => c.ToCategoryDto()).ToList();
            return categoryDtos;
        }

        public async Task<CategoryDto> GetCategoryByIdAsync(int categoryId)
        {
            Category? category = await _categoryRepository.GetCategoryByIdAsync(categoryId);
            if (category == null)
            {
                throw new KeyNotFoundException("Category not found");
            }
            return category.ToCategoryDto();
        }

        public async Task UpdateCategoryAsync(int categoryId, UpdateCategoryRequestDto updateCategoryRequestDto)
        {
            Category? category = await _categoryRepository.UpdateCategoryAsync(updateCategoryRequestDto.ToCategoryFromUpdateCategoryDto(), categoryId);
            if (category == null)
            {
                throw new KeyNotFoundException("Category not found");
            }
        }
    }
}