using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MainApi.Domain.Models.Products.ProductAttributes;

namespace MainApi.Application.Interfaces.Repositories
{
    public interface IProductAttributeRepository
    {
        //ProductAttributes
        Task<List<ProductAttribute>> GetAllProductAttributesAsync();
        Task<ProductAttribute> AddProductAttributeAsync(ProductAttribute productAttribute);
        Task<bool> ProductAttributeExistsByName(string name);
        Task<ProductAttribute?> DeleteProductAttribute(int productAttributeId);
        Task<ProductAttribute?> GetProductAttributeByIdAsync(int ProductAttributeId);

        //PredefinedAttributeValues
        Task<PredefinedProductAttributeValue> AddPredefinedProductAttributeValueAsync(PredefinedProductAttributeValue predefinedProductAttributeValue);
        Task<bool> PredefinedProductAttributeValueExistsByName(string name);
        Task<PredefinedProductAttributeValue?> DeletePredefinedProductAttributeValue(int PredefinedProductAttributeValueId);
        Task<List<PredefinedProductAttributeValue>> GetAttributeValuesById(List<int> Ids);

        //AttributeMapping
        Task<Product_ProductAttribute_Mapping> AddProductAttributeMappingAsync(Product_ProductAttribute_Mapping product_ProductAttribute_Mapping);
        Task<List<Product_ProductAttribute_Mapping>> GetAllProductAttributeMappingAsync(int productId);

        //AttributeCombination
        Task<ProductCombination> AddProductAttributeCombinationAsync(ProductCombination productCombination);
        Task<List<ProductCombination>> GetAllProductAttributeCombinationAsync(int productId);
        Task<ProductCombination?> DeleteProductAttributeCombination(int ProductAttributeCombinationId);
    }
}