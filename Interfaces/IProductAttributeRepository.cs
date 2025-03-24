using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MainApi.Models.Products.ProductAttributes;

namespace MainApi.Interfaces
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
        Task<ProductAttributeCombination> AddProductAttributeCombinationAsync(ProductAttributeCombination productAttributeCombination);
        Task<List<ProductAttributeCombination>> GetAllProductAttributeCombinationAsync(int productId);
        // Task<ProductAttributeCombination?> GetAttributeCombinationByAttributeIdsAsync(List<int> Ids);
        Task<ProductAttributeCombination?> DeleteProductAttributeCombination(int ProductAttributeCombinationId);



    }
}