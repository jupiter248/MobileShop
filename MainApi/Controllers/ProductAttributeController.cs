using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MainApi.Application.Dtos.ProductAttributes;
using MainApi.Application.Interfaces;
using MainApi.Application.Interfaces.Repositories;
using MainApi.Application.Interfaces.Services;
using MainApi.Application.Mappers;
using MainApi.Domain.Models.Products;
using MainApi.Domain.Models.Products.ProductAttributes;
using Microsoft.AspNetCore.Mvc;

namespace MainApi.Api.Controllers
{
    [ApiController]
    [Route("api/product-attribute")]
    public class ProductAttributeController : ControllerBase
    {
        private readonly IProductAttributeService _productAttributeService;
        private readonly IProductRepository _productRepo;
        private readonly ISKUService _sKUService;
        public ProductAttributeController(IProductAttributeService productAttributeService, IProductRepository productRepo, ISKUService sKUService)
        {
            _productAttributeService = productAttributeService;
            _productRepo = productRepo;
            _sKUService = sKUService;
        }
        [HttpPost]
        public async Task<IActionResult> AddProductAttribute([FromBody] AddProductAttributeRequestDto addProductAttributeRequestDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            await _productAttributeService.AddProductAttributeAsync(addProductAttributeRequestDto);
            return Created();
        }
        [HttpGet]
        public async Task<IActionResult> GetAllProductAttributes()
        {
            List<ProductAttributeDto> productAttributeDtos = await _productAttributeService.GetAllProductAttributesAsync();
            return Ok(productAttributeDtos);
        }
        [HttpPost("predefined-value")]
        public async Task<IActionResult> AddPredefinedProductAttributeValue([FromBody] AddPredefinedProductAttributeValueRequestDto addPredefinedProductAttributeValueRequestDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            await _productAttributeService.AddPredefinedProductAttributeValueAsync(addPredefinedProductAttributeValueRequestDto);
            return Created();
        }
        [HttpPost("assign-to-product")]
        public async Task<IActionResult> AssignToProduct([FromBody] AddProductAttributeMappingRequestDto addProductAttributeMappingRequestDto)
        {
            await _productAttributeService.AssignAttributeToProductAsync(addProductAttributeMappingRequestDto);
            return Created();
        }
        [HttpGet("assigned-attribute{productId:int}")]
        public async Task<IActionResult> GetAllAssignedProductAttribute([FromRoute] int productId)
        {
            List<ProductAttributeMappingDto> productAttributeMappingDtos = await _productAttributeService.GetAllAssignedProductAttributeAsync(productId);
            return Ok(productAttributeMappingDtos);
        }
        [HttpPost("combination")]
        public async Task<IActionResult> AddAttributeCombination([FromBody] AddProductCombinationRequestDto requestDto)
        {
            await _productAttributeService.AddAttributeCombinationAsync(requestDto);
            return Created();
        }
        [HttpGet("combination{productId:int}")]
        public async Task<IActionResult> GetAllAttributeCombinations([FromRoute] int productId)
        {
            List<ProductCombinationDto> combinationDtos = await _productAttributeService.GetAllAttributeCombinationsAsync(productId);

            return Ok(combinationDtos);
        }
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> RemoveProductAttribute([FromRoute] int id)
        {
            await _productAttributeService.DeleteProductAttributeAsync(id);
            return NoContent();
        }
        [HttpDelete("predefined{id:int}")]
        public async Task<IActionResult> RemovePredefinedProductAttributeValue([FromRoute] int id)
        {
            await _productAttributeService.DeletePredefinedProductAttributeValue(id);
            return NoContent();
        }
        [HttpDelete("combination{id:int}")]
        public async Task<IActionResult> RemoveProductAttributeCombination([FromRoute] int id)
        {
            await _productAttributeService.DeleteProductAttributeCombination(id);
            return NoContent();
        }

    }
}