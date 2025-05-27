using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.Arm;
using System.Threading.Tasks;
using MainApi.Application.Dtos.SpecificationAttributes;
using MainApi.Application.Interfaces;
using MainApi.Application.Interfaces.Repositories;
using MainApi.Application.Interfaces.Services;
using MainApi.Application.Mappers;
using MainApi.Domain.Models.Products;
using MainApi.Domain.Models.Products.SpecificationAttributes;
using Microsoft.AspNetCore.Mvc;

namespace MainApi.Api.Controllers
{
    [ApiController]
    [Route("api/specifications")]
    public class SpecificationAttributesController : ControllerBase
    {
        private readonly ISpecificationAttributesService _specificationAttributesService;
        public SpecificationAttributesController(ISpecificationAttributesService specificationAttributesService)
        {
            _specificationAttributesService = specificationAttributesService;
        }
        [HttpPost]
        public async Task<IActionResult> AddSpecificationAttribute([FromBody] AddSpecificationAttributeRequestDto addSpecificationAttributeRequestDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            SpecificationAttributeDto specificationAttributeDto = await _specificationAttributesService.AddSpecificationAttributeAsync(addSpecificationAttributeRequestDto);
            return Created();
        }
        [HttpGet]
        public async Task<IActionResult> GetAllSpecificationAttributes()
        {
            List<SpecificationAttributeDto> specificationAttributeDtos = await _specificationAttributesService.GetAllSpecificationAttributesAsync();
            return Ok(specificationAttributeDtos);
        }
        [HttpPost("option")]
        public async Task<IActionResult> AddSpecificationAttributeOption([FromBody] AddSpecificationAttributeOptionRequestDto option)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            await _specificationAttributesService.AddSpecificationAttributeOptionAsync(option);
            return Created();
        }
        [HttpPost("assign-to-product")]
        public async Task<IActionResult> AssignToProduct([FromBody] AddAssignToProductRequestDto addAssignToProductRequestDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            await _specificationAttributesService.AssignSpecificationToProductAsync(addAssignToProductRequestDto);

            return Created();
        }
        [HttpGet("product-specification/{productId:int}")]
        public async Task<IActionResult> GetProductSpecification([FromRoute] int productId)
        {
            List<ProductSpecificationAttributeDto> specificationAttributeOptionDto = await _specificationAttributesService.GetProductSpecificationOptionsAsync(productId);
            return Ok(specificationAttributeOptionDto);
        }
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteSpecification([FromRoute] int id)
        {
            await _specificationAttributesService.DeleteSpecificationAsync(id);
            return NoContent();
        }
        [HttpDelete("option{id:int}")]
        public async Task<IActionResult> DeleteSpecificationOption([FromRoute] int id)
        {
            await _specificationAttributesService.DeleteSpecificationOptionAsync(id);
            return NoContent();
        }
        [HttpDelete("assigned{id:int}")]
        public async Task<IActionResult> DeleteSpecificationAssigned([FromRoute] int id)
        {
            await _specificationAttributesService.DeleteSpecificationAssignedAsync(id);

            return NoContent();
        }
    }
}