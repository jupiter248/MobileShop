using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MainApi.Application.Dtos.Category;

namespace MainApi.Application.Interfaces.Services
{
    public interface ICategoryService
    {
        Task<List<CategoryDto>> GetAllCategoriesAsync();
        Task<CategoryDto> GetCategoryByIdAsync(int categoryId);
        Task<CategoryDto> AddCategoryAsync(AddCategoryRequestDto addCategoryRequestDto);
        Task UpdateCategoryAsync(int categoryId, UpdateCategoryRequestDto updateCategoryRequestDto);
        Task DeleteCategoryAsync(int categoryId);
    }
}