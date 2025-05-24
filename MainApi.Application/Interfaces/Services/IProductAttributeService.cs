using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MainApi.Application.Dtos.ProductAttributes;
using MainApi.Domain.Models.Products.ProductAttributes;

namespace MainApi.Application.Interfaces.Services
{
    public interface IProductAttributeService
    {
        Task<ProductAttributeDto> AddProductAttributeAsync(AddProductAttributeRequestDto addProductAttributeRequestDto);
        Task<List<ProductAttributeDto>> GetAllProductAttributesAsync();
        Task<PredefinedProductAttributeValueDto> AddPredefinedProductAttributeValueAsync(AddPredefinedProductAttributeValueRequestDto addPredefinedValueDto);
        Task<ProductAttributeMappingDto> AssignAttributeToProductAsync(AddProductAttributeMappingRequestDto addProductAttributeMappingRequestDto);
        Task<List<ProductAttributeMappingDto>> GetAllAssignedProductAttributeAsync(int productId);
        Task AddAttributeCombinationAsync(AddProductCombinationRequestDto addProductCombinationRequestDto);
        Task<List<ProductCombinationDto>> GetAllAttributeCombinationsAsync(int productId);
        Task DeleteProductAttributeAsync(int productAttributeId);
        Task DeletePredefinedProductAttributeValue(int predefinedProductAttributeId);
        Task DeleteProductAttributeCombination(int combinationId);


    }
}