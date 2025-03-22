using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MainApi.Models.Products.ProductAttributes;

namespace MainApi.Interfaces
{
    public interface IProductAttributeRepository
    {
        Task<List<ProductAttribute>> GetAllProductAttributesAsync();
        Task<ProductAttribute> AddProductAttributeAsync(ProductAttribute productAttribute);
        Task<PredefinedProductAttributeValue> AddPredefinedProductAttributeValueAsync(PredefinedProductAttributeValue predefinedProductAttributeValue);
        Task<Product_ProductAttribute_Mapping> AddProductAttributeMappingAsync(Product_ProductAttribute_Mapping product_ProductAttribute_Mapping);
        Task<List<Product_ProductAttribute_Mapping>> GetAllProductAttributeMappingAsync(int productId);
        Task<ProductAttributeCombination> AddProductAttributeCombinationAsync(ProductAttributeCombination productAttributeCombination);
        Task<List<ProductAttributeCombination>> GetAllProductAttributeCombinationAsync(int productId);
        Task<bool> ProductAttributeExistsByName(string name);
        Task<ProductAttribute?> GetProductAttributeByIdAsync(int ProductAttributeId);
        Task<bool> PredefinedProductAttributeValueExistsByName(string name);
        Task<List<PredefinedProductAttributeValue>> GetAttributeValuesById(List<int> Ids);

    }
}