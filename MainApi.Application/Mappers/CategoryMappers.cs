using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MainApi.Application.Dtos.Category;
using MainApi.Domain.Models;
using MainApi.Domain.Models.Products;

namespace MainApi.Application.Mappers
{
    public static class CategoryMappers
    {
        public static Category ToCategoryFromAddCategoryDto(this AddCategoryRequestDto addCategoryRequestDto)
        {
            return new Category()
            {
                CategoryName = addCategoryRequestDto.CategoryName,
                Description = addCategoryRequestDto.Description
            };
        }
        public static CategoryDto ToCategoryDto(this Category category)
        {
            return new CategoryDto()
            {
                Id = category.Id,
                CategoryName = category.CategoryName,
                Description = category.Description,
                ProductsDtos = category.Products.Select(p => p.ToProductDto()).ToList()
            };
        }
        public static Category ToCategoryFromUpdateCategoryDto(this UpdateCategoryRequestDto updateCategoryRequestDto)
        {
            return new Category()
            {
                CategoryName = updateCategoryRequestDto.CategoryName,
                Description = updateCategoryRequestDto.Description
            };
        }
    }
}