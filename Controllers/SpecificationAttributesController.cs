using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MainApi.Dtos.SpecificationAttributes;
using MainApi.Interfaces;
using MainApi.Mappers;
using MainApi.Models.Products.SpecificationAttributes;
using Microsoft.AspNetCore.Mvc;

namespace MainApi.Controllers
{
    [ApiController]
    [Route("api/specifications")]
    public class SpecificationAttributesController : ControllerBase
    {
        private readonly ISpecificationAttributesRepository _specificationAttributesRepo;
        public SpecificationAttributesController(ISpecificationAttributesRepository specificationAttributesRepo)
        {
            _specificationAttributesRepo = specificationAttributesRepo;
        }
        [HttpPost]
        public async Task<IActionResult> AddSpecificationAttribute([FromBody] AddSpecificationAttributeRequestDto addSpecificationAttributeRequestDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            SpecificationAttribute specification = addSpecificationAttributeRequestDto.ToSpecificationAttributeFromAddDto();
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
    }
}