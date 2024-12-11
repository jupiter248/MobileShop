using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MainApi.Dtos.Category;
using MainApi.Models;

namespace MainApi.Mappers
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
    }
}