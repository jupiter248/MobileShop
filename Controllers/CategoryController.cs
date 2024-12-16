using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using MainApi.Dtos.Category;
using MainApi.Interfaces;
using MainApi.Mappers;
using MainApi.Models;
using Microsoft.AspNetCore.Mvc;

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
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetCategoryById([FromRoute] int id)
        {
            var category = await _categoryRepository.GetCategoryByIdAsync(id);
            if (category == null) return BadRequest();
            return Ok(category);
        }
        [HttpPost]
        public async Task<IActionResult> AddCategory([FromBody] AddCategoryRequestDto addCategoryRequestDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            Category? category = addCategoryRequestDto.ToCategoryFromAddCategoryDto();
            await _categoryRepository.AddCategoryAsync(addCategoryRequestDto.ToCategoryFromAddCategoryDto());
            return CreatedAtAction(nameof(GetCategoryById), new { id = category.Id }, category.ToCategoryDto());
        }
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateCategory([FromRoute] int id, [FromBody] UpdateCategoryRequestDto updateCategoryRequestDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var categoryModel = updateCategoryRequestDto.ToCategoryFromUpdateCategoryDto();
            var category = await _categoryRepository.UpdateCategoryAsync(categoryModel, id);
            if (category == null) return NotFound();
            return NoContent();
        }
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> RemoveCategory([FromRoute] int id)
        {
            var category = await _categoryRepository.RemoveCategoryAsync(id);
            if (category == null) return NotFound();
            return NoContent();
        }
    }
}