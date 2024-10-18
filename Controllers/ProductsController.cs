using MainApi.Dtos.Products;
using MainApi.Interfaces;
using MainApi.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace MainApi.Controllers;

[Route("api/product")]
[ApiController]
public class ProductController : ControllerBase
{
    private readonly IProductRepository _productRepo;
    public ProductController(IProductRepository productRepo)
    {
        _productRepo = productRepo;
    }
    [HttpGet]
    public async Task<IActionResult> GetAllProduct()
    {
        var products = await _productRepo.GetAllProductsAsync();
        if (products == null)
        {
            return BadRequest();
        }
        return Ok(products);
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> GetProductById([FromRoute] int id)
    {
        var product = await _productRepo.GetProductByIdAsync(id);
        if (product == null)
            return BadRequest();
        return Ok(product);
    }
    [HttpPost]
    public async Task<IActionResult> AddProduct([FromBody] CreateProductRequestDto createProductRequestDto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var product = createProductRequestDto.ToProductFromCreateDto();
        await _productRepo.AddProductAsync(product);
        return CreatedAtAction(nameof(GetProductById), new { id = product.Id }, product);
    }
}