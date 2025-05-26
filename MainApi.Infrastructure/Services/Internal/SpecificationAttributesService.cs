using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MainApi.Application.CustomException;
using MainApi.Application.Dtos.SpecificationAttributes;
using MainApi.Application.Interfaces.Repositories;
using MainApi.Application.Interfaces.Services;
using MainApi.Application.Mappers;
using MainApi.Domain.Models.Products;
using MainApi.Domain.Models.Products.SpecificationAttributes;

namespace MainApi.Infrastructure.Services.Internal
{
    public class SpecificationAttributesService : ISpecificationAttributesService
    {
        private readonly ISpecificationAttributesRepository _specificationAttributesRepo;
        private readonly IProductRepository _productRepo;
        public SpecificationAttributesService(ISpecificationAttributesRepository specificationAttributesRepo, IProductRepository productRepo)
        {
            _specificationAttributesRepo = specificationAttributesRepo;
            _productRepo = productRepo;
        }
        public async Task<SpecificationAttributeDto> AddSpecificationAttributeAsync(AddSpecificationAttributeRequestDto addSpecificationAttributeRequestDto)
        {
            SpecificationAttribute specification = addSpecificationAttributeRequestDto.ToSpecificationAttributeFromAddDto();
            bool specificationExists = await _specificationAttributesRepo.SpecificationAttributeExistsAsync(specification.Name);
            if (specificationExists == true)
            {
                throw new ConflictException("Data already exists");
            }
            SpecificationAttribute specificationAttribute = await _specificationAttributesRepo.AddSpecificationAttributeAsync(specification);
            return specificationAttribute.ToSpecificationAttributeDto();
        }

        public async Task<SpecificationAttributeOptionDto> AddSpecificationAttributeOptionAsync(AddSpecificationAttributeOptionRequestDto option)
        {
            SpecificationAttribute? specificationAttribute = await _specificationAttributesRepo.GetSpecificationAttributeByNameAsync(option.AttributeName) ?? throw new KeyNotFoundException("Attribute name not found");
            SpecificationAttributeOption optionModel = new SpecificationAttributeOption()
            {
                Name = option.AttributeValue,
                SpecificationAttributeId = specificationAttribute.Id,
                SpecificationAttribute = specificationAttribute
            };
            SpecificationAttributeOption specificationAttributeOption = await _specificationAttributesRepo.AddSpecificationOptionAsync(optionModel);
            return specificationAttributeOption.ToSpecificationAttributeOptionDto();
        }

        public async Task AssignSpecificationToProductAsync(AddAssignToProductRequestDto addAssignToProductRequestDto)
        {
            Product? product = await _productRepo.GetProductByIdAsync(addAssignToProductRequestDto.ProductId) ?? throw new KeyNotFoundException("Product not found");

            SpecificationAttributeOption? option = await _specificationAttributesRepo.GetSpecificationAttributeOptionByIdAsync(addAssignToProductRequestDto.SpecificationAttributeOptionId) ?? throw new KeyNotFoundException("Option not found");

            Product_SpecificationAttribute_Mapping mappedModel = addAssignToProductRequestDto.ToProductSpecificationAttributeMappingFromAdd();
            mappedModel.SpecificationAttributeOption = option;
            mappedModel.Product = product;

            await _specificationAttributesRepo.AssignSpecificationToProductAsync(mappedModel);
        }

        public async Task DeleteSpecificationAssignedAsync(int assignedId)
        {
            Product_SpecificationAttribute_Mapping mapped = await _specificationAttributesRepo.GetMappedSpecificationByIdAsync(assignedId) ?? throw new KeyNotFoundException("AssignedSpecification not found");
            await _specificationAttributesRepo.DeleteAssignedSpecificationAsync(mapped);
        }

        public async Task DeleteSpecificationAsync(int specificationId)
        {
            SpecificationAttribute specificationAttribute = await _specificationAttributesRepo.GetSpecificationAttributeByIdAsync(specificationId) ?? throw new KeyNotFoundException("Specification not found");
            await _specificationAttributesRepo.DeleteSpecificationAttributeAsync(specificationAttribute);
        }

        public async Task DeleteSpecificationOptionAsync(int specificationOptionId)
        {
            SpecificationAttributeOption specificationAttributeOption = await _specificationAttributesRepo.GetSpecificationAttributeOptionByIdAsync(specificationOptionId) ?? throw new KeyNotFoundException("SpecificationOption not found");
            await _specificationAttributesRepo.DeleteSpecificationOptionAsync(specificationAttributeOption);
        }

        public async Task<List<SpecificationAttributeDto>> GetAllSpecificationAttributesAsync()
        {
            List<SpecificationAttribute> specificationAttributes = await _specificationAttributesRepo.GetAllSpecificationAttributesAsync();
            List<SpecificationAttributeDto> specificationAttributeDtos = specificationAttributes.Select(s => s.ToSpecificationAttributeDto()).ToList();
            return specificationAttributeDtos;
        }

        public async Task<List<ProductSpecificationAttributeDto>> GetProductSpecificationOptionsAsync(int productId)
        {
            List<SpecificationAttributeOption> options = await _specificationAttributesRepo.GetProductSpecificationAttributesByProductIdAsync(productId);
            List<ProductSpecificationAttributeDto> optionDtos = options
            .Select(o =>
            {
                return new ProductSpecificationAttributeDto()
                {
                    SpecificationName = o.SpecificationAttribute.Name,
                    SpecificationValue = o.Name
                };
            }
            ).ToList();
            return optionDtos;
        }
    }
}