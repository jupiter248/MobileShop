using MainApi.Application.Dtos.Filtering;
using MainApi.Application.Dtos.Products;
using MainApi.Application.Extensions;
using MainApi.Application.Interfaces;
using MainApi.Application.Mappers;
using MainApi.Domain.Models;
using MainApi.Domain.Models.Products;
using MainApi.Domain.Models.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace MainApi.Api.Controllers;
[Route("api/product")]
[ApiController]
public class ProductController : ControllerBase
{
    private readonly IProductRepository _productRepo;
    private readonly UserManager<AppUser> _userManager;
    public ProductController(IProductRepository productRepo, UserManager<AppUser> userManager)
    {
        _productRepo = productRepo;
        _userManager = userManager;
    }
    [HttpGet]
    public async Task<IActionResult> GetAllProduct([FromQuery] ProductSortingDto sortingDto, [FromQuery] ProductFilteringDto filteringDto)
    {
        List<Product>? products = await _productRepo.GetAllProductsAsync(sortingDto, filteringDto);
        if (products == null)
        {
            return BadRequest();
        }
        List<ProductDto>? productsDto = products.Select(p => p.ToProductDto()).ToList();
        return Ok(productsDto);
    }
    [Authorize(Roles = "Admin")]
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetProductById([FromRoute] int id)
    {
        Product? product = await _productRepo.GetProductByIdAsync(id);
        if (product == null)
            return BadRequest();
        return Ok(product.ToProductDto());
    }
    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<IActionResult> AddProduct([FromBody] CreateProductRequestDto createProductRequestDto, int categoryId)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        Product? product = createProductRequestDto.ToProductFromCreateDto(categoryId);
        await _productRepo.AddProductAsync(product);
        return CreatedAtAction(nameof(GetProductById), new { id = product.Id }, product.ToProductDto());
    }
    [Authorize(Roles = "Admin")]
    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateProduct([FromRoute] int id, [FromBody] UpdateProductRequestDto updateProductRequestDto, int categoryId)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        Product? product = await _productRepo.UpdateProductAsync(updateProductRequestDto.ToProductFromUpdateDto(categoryId), id);
        if (product == null) return NotFound();
        return NoContent();
    }
    [Authorize(Roles = "Admin")]
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> RemoveProduct([FromRoute] int id)
    {
        Product? product = await _productRepo.RemoveProductAsync(id);
        if (product == null) return NotFound();
        return NoContent();
    }

}