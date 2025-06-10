using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MainApi.Application.Dtos.ProductAttributes;
using MainApi.Domain.Models.Products.ProductAttributes;

namespace MainApi.Application.Mappers
{
    public static class ProductAttributeMappers
    {
        public static ProductAttributeDto ToProductAttributeDto(this ProductAttribute productAttribute)
        {
            return new ProductAttributeDto()
            {
                Id = productAttribute.Id,
                Name = productAttribute.Name,
                Description = productAttribute.Description,
                Values = productAttribute.PredefinedProductAttributeValues.Select(p => p.ToPredefinedProductAttributeValueDto()).ToList()
            };
        }
        public static PredefinedProductAttributeValueDto ToPredefinedProductAttributeValueDto(this PredefinedProductAttributeValue predefinedProductAttributeValue)
        {
            return new PredefinedProductAttributeValueDto()
            {
                Id = predefinedProductAttributeValue.Id,
                Name = predefinedProductAttributeValue.Name
            };
        }
        public static ProductAttribute ToProductAttributeFromAdd(this AddProductAttributeRequestDto addProductAttributeRequestDto)
        {
            return new ProductAttribute()
            {
                Name = addProductAttributeRequestDto.Name,
                Description = addProductAttributeRequestDto.Description
            };
        }
        public static PredefinedProductAttributeValue ToPredefinedProductAttributeValueFromAdd(this AddPredefinedProductAttributeValueRequestDto valueRequestDto, ProductAttribute productAttribute)
        {
            return new PredefinedProductAttributeValue()
            {
                Name = valueRequestDto.Name,
                ProductAttributeId = valueRequestDto.ProductAttributeId,
                ProductAttribute = productAttribute
            };
        }
        public static ProductAttributeMappingDto ToProductMappingDto(this ProductAttributeMapping productAttributeMapping )
        {
            return new ProductAttributeMappingDto
            {
                Attribute = productAttributeMapping.ProductAttribute.ToProductAttributeDto(),
                IsRequired = productAttributeMapping.IsRequired

            };
        }
    }
}