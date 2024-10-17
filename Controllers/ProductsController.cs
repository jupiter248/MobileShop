using MainApi.Interfaces;
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
}