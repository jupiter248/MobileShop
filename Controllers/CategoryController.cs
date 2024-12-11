using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using MainApi.Dtos.Category;
using MainApi.Interfaces;
using MainApi.Mappers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace MainApi.Controllers
{
    [Route("api/category")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepository;
        public CategoryController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllCategories()
        {
            var categories = await _categoryRepository.GetAllCategoriesAsync();
            if (categories == null) return BadRequest();
            return Ok(categories);
        }
        [HttpPost]
        public async Task<IActionResult> AddCategory([FromBody] AddCategoryRequestDto addCategoryRequestDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var category = addCategoryRequestDto.ToCategoryFromAddCategoryDto();
            await _categoryRepository.AddCategoryAsync(category);
            return Ok(category);
        }
    }
}