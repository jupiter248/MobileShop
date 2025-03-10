using MainApi.Dtos.Products;
using MainApi.Extensions;
using MainApi.Interfaces;
using MainApi.Mappers;
using MainApi.Models;
using MainApi.Models.Products;
using MainApi.Models.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace MainApi.Controllers;
[Authorize]
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
    public async Task<IActionResult> GetAllProduct()
    {
        List<Product>? products = await _productRepo.GetAllProductsAsync();
        if (products == null)
        {
            return BadRequest();
        }
        List<ProductDto>? productsDto = products.Select(p => p.ToProductDto()).ToList();
        return Ok(productsDto);
    }
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetProductById([FromRoute] int id)
    {
        // var username = User.GetUsername();
        // var appUser = await _userManager.FindByNameAsync(username);
        // if (appUser == null) return BadRequest("User not found");

        Product? product = await _productRepo.GetProductByIdAsync(id);
        if (product == null)
            return BadRequest();
        return Ok(product.ToProductDto());
    }
    [HttpPost]
    public async Task<IActionResult> AddProduct([FromBody] CreateProductRequestDto createProductRequestDto, int categoryId)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        Product? product = createProductRequestDto.ToProductFromCreateDto(categoryId);
        await _productRepo.AddProductAsync(product);
        return CreatedAtAction(nameof(GetProductById), new { id = product.Id }, product.ToProductDto());
    }
    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateProduct([FromRoute] int id, [FromBody] UpdateProductRequestDto updateProductRequestDto, int categoryId)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        Product? product = await _productRepo.UpdateProductAsync(updateProductRequestDto.ToProductFromUpdateDto(categoryId), id);
        if (product == null) return NotFound();
        return NoContent();
    }
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> RemoveProduct([FromRoute] int id)
    {
        Product? product = await _productRepo.RemoveProductAsync(id);
        if (product == null) return NotFound();
        return NoContent();
    }

}