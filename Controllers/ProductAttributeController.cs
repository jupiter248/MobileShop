using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MainApi.Dtos.ProductAttributes;
using MainApi.Interfaces;
using MainApi.Mappers;
using MainApi.Models.Products;
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
        private readonly ISKUService _sKUService;
        public ProductAttributeController(IProductAttributeRepository productAttributeRepo, IProductRepository productRepo, ISKUService sKUService)
        {
            _productAttributeRepo = productAttributeRepo;
            _productRepo = productRepo;
            _sKUService = sKUService;
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
        [HttpPost("assign-to-product")]
        public async Task<IActionResult> AssignToProduct([FromBody] AddProductAttributeMappingRequestDto addProductAttributeMappingRequestDto)
        {
            Product? product = await _productRepo.GetProductByIdAsync(addProductAttributeMappingRequestDto.ProductId);
            if (product == null)
            {
                return NotFound("Product not found");
            }
            ProductAttribute? productAttribute = await _productAttributeRepo.GetProductAttributeByIdAsync(addProductAttributeMappingRequestDto.ProductAttributeId);
            if (productAttribute == null)
            {
                return NotFound("Product attribute not found");
            }

            Product_ProductAttribute_Mapping? mappingModel = new Product_ProductAttribute_Mapping()
            {
                IsRequired = addProductAttributeMappingRequestDto.IsRequired,
                Product = product,
                ProductAttribute = productAttribute,
                ProductAttributeId = addProductAttributeMappingRequestDto.ProductAttributeId,
                ProductId = addProductAttributeMappingRequestDto.ProductId
            };

            await _productAttributeRepo.AddProductAttributeMappingAsync(mappingModel);
            return Created();
        }
        [HttpGet("assigned-attribute{productId:int}")]
        public async Task<IActionResult> GetAllAssignedProductAttribute([FromRoute] int productId)
        {
            List<Product_ProductAttribute_Mapping> product_ProductAttribute_Mappings = await _productAttributeRepo.GetAllProductAttributeMappingAsync(productId);
            List<ProductAttributeMappingDto> productAttributeMappingDtos = product_ProductAttribute_Mappings.Select(m =>
            {
                return new ProductAttributeMappingDto()
                {
                    Id = m.Id,
                    IsRequired = m.IsRequired,
                    AttributeDto = m.ProductAttribute.ToProductAttributeDto()
                };
            }).ToList();
            return Ok(productAttributeMappingDtos);
        }
        [HttpPost("combination")]
        public async Task<IActionResult> AddAttributeCombination([FromBody] AddProductAttributeCombinationRequestDto requestDto)
        {
            Product? product = await _productRepo.GetProductByIdAsync(requestDto.ProductId);
            if (product == null)
            {
                return NotFound("Product not found");
            }

            List<PredefinedProductAttributeValue> selectedValues = await _productAttributeRepo.GetAttributeValuesById(requestDto.SelectedValueIds);
            if (selectedValues.Count != requestDto.SelectedValueIds.Count)
            {
                return BadRequest("Invalid attribute selections");
            }

            string attributeString = string.Join(" - ", selectedValues.Select(v => v.Name));
            string Sku = _sKUService.GenerateSKU(product.ProductName, selectedValues.Select(s => s.Name).ToList());

            ProductAttributeCombination combination = new ProductAttributeCombination()
            {
                ProductId = requestDto.ProductId,
                FinalPrice = requestDto.FinalPrice,
                Quantity = requestDto.Quantity,
                Product = product,
                AttributeCombination = attributeString,
                Sku = Sku
            };
            await _productAttributeRepo.AddProductAttributeCombinationAsync(combination);

            return Created();
        }
        [HttpGet("combination{productId:int}")]
        public async Task<IActionResult> GetAllAttributeCombinations([FromRoute] int productId)
        {
            List<ProductAttributeCombination> combinations = await _productAttributeRepo.GetAllProductAttributeCombinationAsync(productId);
            List<ProductAttributeCombinationDto> combinationDtos = combinations.Select(c => c.ToProductAttributeCombinationDto()).ToList();
            return Ok(combinationDtos);
        }

    }
}