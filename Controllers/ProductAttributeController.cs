using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MainApi.Dtos.ProductAttributes;
using MainApi.Interfaces;
using MainApi.Mappers;
using MainApi.Models.Products.ProductAttributes;
using Microsoft.AspNetCore.Mvc;

namespace MainApi.Controllers
{
    [ApiController]
    [Route("api/product-attribute")]
    public class ProductAttributeController : ControllerBase
    {
        private readonly IProductAttributeRepository _productAttributeRepo;
        private readonly IProductRepository _productRepo;
        public ProductAttributeController(IProductAttributeRepository productAttributeRepo, IProductRepository productRepo)
        {
            _productAttributeRepo = productAttributeRepo;
            _productRepo = productRepo;
        }
        [HttpPost]
        public async Task<IActionResult> AddProductAttribute([FromBody] AddProductAttributeRequestDto addProductAttributeRequestDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            bool attributeModelExists = await _productAttributeRepo.ProductAttributeExistsByName(addProductAttributeRequestDto.Name);
            if (attributeModelExists == true)
            {
                return BadRequest("This attribute name already made");
            }

            await _productAttributeRepo.AddProductAttributeAsync(addProductAttributeRequestDto.ToProductAttributeFromAdd());
            return Created();
        }
        [HttpGet]
        public async Task<IActionResult> GetAllProductAttributes()
        {
            List<ProductAttribute> productAttribute = await _productAttributeRepo.GetAllProductAttributesAsync();
            List<ProductAttributeDto> productAttributeDtos = productAttribute.Select(a => a.ToProductAttributeDto()).ToList();
            return Ok(productAttributeDtos);
        }
        [HttpPost("predefined-value")]
        public async Task<IActionResult> AddPredefinedProductAttributeValue([FromBody] AddPredefinedProductAttributeValueRequestDto addPredefinedProductAttributeValueRequestDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            bool attributeModelExists = await _productAttributeRepo.PredefinedProductAttributeValueExistsByName(addPredefinedProductAttributeValueRequestDto.Name);
            if (attributeModelExists == true)
            {
                return BadRequest("This predefined attribute value already made");
            }

            await _productAttributeRepo.AddPredefinedProductAttributeValueAsync(addPredefinedProductAttributeValueRequestDto.ToPredefinedProductAttributeValueFromAdd());
            return Created();
        }
    }
}