using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MainApi.Application.Dtos.SpecificationAttributes;
using MainApi.Domain.Models.Products.SpecificationAttributes;

namespace MainApi.Application.Mappers
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
        public static Product_SpecificationAttribute_Mapping ToProductSpecificationAttributeMappingFromAdd(this AddAssignToProductRequestDto addAssignToProductRequestDto)
        {
            return new Product_SpecificationAttribute_Mapping()
            {
                ProductId = addAssignToProductRequestDto.ProductId,
                SpecificationAttributeOptionId = addAssignToProductRequestDto.SpecificationAttributeOptionId,
                AllowFiltering = addAssignToProductRequestDto.AllowFiltering,
                ShowOnProductPage = addAssignToProductRequestDto.ShowOnProductPage,
                
            };
        }
    }
}
