using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MainApi.Application.CustomException;
using MainApi.Application.Dtos.ProductAttributes;
using MainApi.Application.Interfaces.Repositories;
using MainApi.Application.Interfaces.Services;
using MainApi.Application.Mappers;
using MainApi.Domain.Models.Products.ProductAttributes;

namespace MainApi.Infrastructure.Services.Internal
{
    public class ProductAttributeService : IProductAttributeService
    {
        private readonly IProductAttributeRepository _productAttributeRepo;
        private readonly IProductRepository _productRepo;
        private readonly ISKUService _sKUService;
        public ProductAttributeService(IProductAttributeRepository productAttributeRepo, IProductRepository productRepo, ISKUService sKUService)
        {
            _productAttributeRepo = productAttributeRepo;
            _productRepo = productRepo;
            _sKUService = sKUService;
        }
        public Task<ProductCombinationDto> AddAttributeCombinationAsync(AddProductCombinationRequestDto addProductCombinationRequestDto)
        {
            throw new NotImplementedException();
        }

        public Task<PredefinedProductAttributeValueDto> AddPredefinedProductAttributeValueAsync(AddPredefinedProductAttributeValueRequestDto addPredefinedValueDto)
        {
            throw new NotImplementedException();
        }

        public async Task<ProductAttributeDto> AddProductAttributeAsync(AddProductAttributeRequestDto addProductAttributeRequestDto)
        {
            bool attributeModelExists = await _productAttributeRepo.ProductAttributeExistsByName(addProductAttributeRequestDto.Name);
            if (attributeModelExists == true)
            {
                throw new ConflictException("Data already exists.");
            }
            ProductAttribute productAttribute = await _productAttributeRepo.AddProductAttributeAsync(addProductAttributeRequestDto.ToProductAttributeFromAdd());
            return productAttribute.ToProductAttributeDto();
        }

        public Task<ProductAttributeMappingDto> AssignAttributeToProductAsync(AddProductAttributeMappingRequestDto addProductAttributeMappingRequestDto)
        {
            throw new NotImplementedException();
        }

        public Task DeletePredefinedProductAttributeValue(int predefinedProductAttributeId)
        {
            throw new NotImplementedException();
        }

        public Task DeleteProductAttributeAsync(int productAttributeId)
        {
            throw new NotImplementedException();
        }

        public Task DeleteProductAttributeCombination(int combinationId)
        {
            throw new NotImplementedException();
        }

        public Task<ProductAttributeMappingDto> GetAllAssignedProductAttributeAsync(int productId)
        {
            throw new NotImplementedException();
        }

        public Task<ProductCombinationDto> GetAllAttributeCombinationsAsync(int productId)
        {
            throw new NotImplementedException();
        }

        public Task<List<ProductAttributeDto>> GetAllProductAttributesAsync()
        {
            throw new NotImplementedException();
        }
    }
}