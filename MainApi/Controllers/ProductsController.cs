using MainApi.Application.Dtos.Filtering;
using MainApi.Application.Dtos.Products;
using MainApi.Application.Extensions;
using MainApi.Application.Interfaces;
using MainApi.Application.Interfaces.Repositories;
using MainApi.Application.Interfaces.Services;
using MainApi.Application.Mappers;
using MainApi.Domain.Models;
using MainApi.Domain.Models.Products;
using MainApi.Domain.Models.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace MainApi.Controllers;

[Route("api/product")]
[ApiController]
public class ProductController : ControllerBase
{
    private readonly IProductService _productService;
    public ProductController(IProductService productService)
    {
        _productService = productService;
    }
    [HttpGet]
    public async Task<IActionResult> GetAllProduct([FromQuery] ProductSortingDto sortingDto, [FromQuery] ProductFilteringDto filteringDto)
    {
        List<ProductDto>? productDtos = await _productService.GetAllProductsAsync(sortingDto, filteringDto);
        return Ok(productDtos);
    }
    [Authorize]
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetProductById([FromRoute] int id)
    {
        ProductDto productDto = await _productService.GetProductByIdAsync(id);
        return Ok(productDto);
    }
    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<IActionResult> AddProduct([FromBody] CreateProductRequestDto createProductRequestDto, int categoryId)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        ProductDto productDto = await _productService.AddProductAsync(createProductRequestDto, categoryId);
        return CreatedAtAction(nameof(GetProductById), new { id = productDto.Id }, productDto);
    }
    [Authorize(Roles = "Admin")]
    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateProduct([FromRoute] int id, [FromBody] UpdateProductRequestDto updateProductRequestDto, int categoryId)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        await _productService.UpdateProductAsync(id, updateProductRequestDto, categoryId);
        return NoContent();
    }
    [Authorize(Roles = "Admin")]
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> RemoveProduct([FromRoute] int id)
    {
        await _productService.DeleteProductAsync(id);
        return NoContent();
    }

}