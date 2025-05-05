using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using MainApi.Application.Dtos.Category;
using MainApi.Application.Interfaces;
using MainApi.Application.Interfaces.Repositories;
using MainApi.Application.Mappers;
using MainApi.Domain.Models.Products;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MainApi.Api.Controllers
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
            List<Category>? categories = await _categoryRepository.GetAllCategoriesAsync();
            if (categories == null) return BadRequest();
            return Ok(categories);
        }
        [Authorize(Roles = "Admin")]
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetCategoryById([FromRoute] int id)
        {
            Category? category = await _categoryRepository.GetCategoryByIdAsync(id);
            if (category == null) return BadRequest();
            return Ok(category.ToCategoryDto());
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> AddCategory([FromBody] AddCategoryRequestDto addCategoryRequestDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            Category? category = addCategoryRequestDto.ToCategoryFromAddCategoryDto();
            await _categoryRepository.AddCategoryAsync(addCategoryRequestDto.ToCategoryFromAddCategoryDto());
            return CreatedAtAction(nameof(GetCategoryById), new { id = category.Id }, category.ToCategoryDto());
        }
        [Authorize(Roles = "Admin")]
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateCategory([FromRoute] int id, [FromBody] UpdateCategoryRequestDto updateCategoryRequestDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            Category? category = await _categoryRepository.UpdateCategoryAsync(updateCategoryRequestDto.ToCategoryFromUpdateCategoryDto(), id);
            if (category == null) return NotFound();
            return NoContent();
        }
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> RemoveCategory([FromRoute] int id)
        {
            Category? category = await _categoryRepository.RemoveCategoryAsync(id);
            if (category == null) return NotFound();
            return NoContent();
        }
    }
}