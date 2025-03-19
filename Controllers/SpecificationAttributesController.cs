using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.Arm;
using System.Threading.Tasks;
using MainApi.Dtos.SpecificationAttributes;
using MainApi.Interfaces;
using MainApi.Mappers;
using MainApi.Models.Products;
using MainApi.Models.Products.SpecificationAttributes;
using Microsoft.AspNetCore.Mvc;

namespace MainApi.Controllers
{
    [ApiController]
    [Route("api/specifications")]
    public class SpecificationAttributesController : ControllerBase
    {
        private readonly ISpecificationAttributesRepository _specificationAttributesRepo;
        private readonly IProductRepository _productRepo;
        public SpecificationAttributesController(ISpecificationAttributesRepository specificationAttributesRepo, IProductRepository productRepo)
        {
            _specificationAttributesRepo = specificationAttributesRepo;
            _productRepo = productRepo;
        }
        [HttpPost]
        public async Task<IActionResult> AddSpecificationAttribute([FromBody] AddSpecificationAttributeRequestDto addSpecificationAttributeRequestDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            SpecificationAttribute specification = addSpecificationAttributeRequestDto.ToSpecificationAttributeFromAddDto();
            bool specificationExists = await _specificationAttributesRepo.SpecificationAttributeExistsAsync(specification.Name);
            if (specificationExists == true)
            {
                return BadRequest("This attribute already made");
            }
            await _specificationAttributesRepo.AddSpecificationAttributeAsync(specification);
            return Created();
        }
        [HttpGet]
        public async Task<IActionResult> GetAllSpecificationAttributes()
        {
            List<SpecificationAttribute> specificationAttributes = await _specificationAttributesRepo.GetAllSpecificationAttributesAsync();
            List<SpecificationAttributeDto> specificationAttributeDtos = specificationAttributes.Select(s => s.ToSpecificationAttributeDto()).ToList();
            return Ok(specificationAttributeDtos);
        }
        [HttpPost("option")]
        public async Task<IActionResult> AddSpecificationAttributeOption([FromBody] AddSpecificationAttributeOptionRequestDto option)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            SpecificationAttribute? specificationAttribute = await _specificationAttributesRepo.GetSpecificationAttributeByName(option.AttributeName);
            if (specificationAttribute == null) return NotFound("Attribute name not found");
            SpecificationAttributeOption optionModel = new SpecificationAttributeOption()
            {
                Name = option.AttributeValue,
                SpecificationAttributeId = specificationAttribute.Id,
                SpecificationAttribute = specificationAttribute
            };
            await _specificationAttributesRepo.AddSpecificationOptionAsync(optionModel);
            return Created();
        }
        [HttpPost("assign-to-product")]
        public async Task<IActionResult> AssignToProduct([FromBody] AddAssignToProductRequestDto addAssignToProductRequestDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            Product? product = await _productRepo.GetProductByIdAsync(addAssignToProductRequestDto.ProductId);
            if (product == null) return BadRequest("Product not found");

            SpecificationAttributeOption? option = await _specificationAttributesRepo.GetSpecificationAttributeOptionById(addAssignToProductRequestDto.SpecificationAttributeOptionId);
            if (option == null) return BadRequest("Option not found");

            Product_SpecificationAttribute_Mapping mappedModel = addAssignToProductRequestDto.ToProductSpecificationAttributeMappingFromAdd();
            mappedModel.SpecificationAttributeOption = option;
            mappedModel.Product = product;

            await _specificationAttributesRepo.AssignSpecificationToProductAsync(mappedModel);

            if (mappedModel == null)
            {
                return BadRequest("Can not create");
            }

            return Created();
        }
        [HttpGet("product-specification/{productId:int}")]
        public async Task<IActionResult> GetProductSpecification([FromRoute] int productId)
        {
            List<SpecificationAttributeOption>? options = await _specificationAttributesRepo.GetProductSpecificationAttributesByProductIdAsync(productId);
            if (options == null) return NotFound();
            List<SpecificationAttributeOptionDto> optionDtos = options.Select(o => o.ToSpecificationAttributeOptionDto()).ToList();
            return Ok(optionDtos);
        }
    }
}