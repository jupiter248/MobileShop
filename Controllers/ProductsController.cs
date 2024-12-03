using MainApi.Dtos.Products;
using MainApi.Extensions;
using MainApi.Interfaces;
using MainApi.Mappers;
using MainApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace MainApi.Controllers;

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
    // [Authorize(Roles = "User")]
    public async Task<IActionResult> GetAllProduct()
    {
        var products = await _productRepo.GetAllProductsAsync();
        if (products == null)
        {
            return BadRequest();
        }
        return Ok(products);
    }
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetProductById([FromRoute] int id)
    {
        // var username = User.GetUsername();
        // var appUser = await _userManager.FindByNameAsync(username);
        // if (appUser == null) return BadRequest("User not found");

        var product = await _productRepo.GetProductByIdAsync(id);
        if (product == null)
            return BadRequest();
        return Ok(product.ToProductDto());
    }
    [HttpPost]
    public async Task<IActionResult> AddProduct([FromBody] CreateProductRequestDto createProductRequestDto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var product = createProductRequestDto.ToProductFromCreateDto();
        await _productRepo.AddProductAsync(product);
        return CreatedAtAction(nameof(GetProductById), new { id = product.Id }, product.ToProductDto());
    }
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> RemoveProduct([FromRoute] int id)
    {
        var product = await _productRepo.RemoveProductAsync(id);
        if (product == null) return NotFound();
        return NoContent();
    }
    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateProduct([FromRoute] int id, [FromBody] UpdateProductRequestDto updateProductRequestDto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var product = await _productRepo.UpdateProductAsync(updateProductRequestDto.ToProductFromUpdateDto(), id);
        if (product == null) return NotFound();
        return Ok(product.ToProductDto());
    }
}