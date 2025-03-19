using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MainApi.Dtos.SpecificationAttributes;
using MainApi.Models.Products.SpecificationAttributes;

namespace MainApi.Mappers
{
    public static class SpecificationAttributeMappers
    {
        public static SpecificationAttributeDto ToSpecificationAttributeDto(this SpecificationAttribute specificationAttribute)
        {
            return new SpecificationAttributeDto()
            {
                Id = specificationAttribute.Id,
                Description = specificationAttribute.Description,
                Name = specificationAttribute.Name,
                Options = specificationAttribute.SpecificationAttributeOptions.Select(o => o.ToSpecificationAttributeOptionDto()).ToList()
            };
        }
        public static SpecificationAttribute ToSpecificationAttributeFromAddDto(this AddSpecificationAttributeRequestDto addSpecificationAttributeRequestDto)
        {
            return new SpecificationAttribute()
            {
                Name = addSpecificationAttributeRequestDto.Name,
                Description = addSpecificationAttributeRequestDto.Description
            };
        }
        public static SpecificationAttributeOptionDto ToSpecificationAttributeOptionDto(this SpecificationAttributeOption specificationAttributeOption)
        {
            return new SpecificationAttributeOptionDto()
            {
                Id = specificationAttributeOption.Id,
                Name = specificationAttributeOption.Name
            };
        }
    }
}
