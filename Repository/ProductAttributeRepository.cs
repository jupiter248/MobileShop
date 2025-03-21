using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MainApi.Data;
using MainApi.Interfaces;
using MainApi.Models.Products.ProductAttributes;

namespace MainApi.Repository
{
    public class ProductAttributeRepository : IProductAttributeRepository
    {
        private readonly ApplicationDbContext _context;
        public ProductAttributeRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public Task<PredefinedProductAttributeValue> AddPredefinedProductAttributeValueAsync(PredefinedProductAttributeValue predefinedProductAttributeValue)
        {
            throw new NotImplementedException();
        }

        public Task<ProductAttribute> AddProductAttributeAsync(ProductAttribute productAttribute)
        {
            throw new NotImplementedException();
        }

        public Task<ProductAttributeCombination> AddProductAttributeCombinationAsync(ProductAttributeCombination productAttributeCombination)
        {
            throw new NotImplementedException();
        }

        public Task<Product_ProductAttribute_Mapping> AddProductAttributeMappingAsync(Product_ProductAttribute_Mapping product_ProductAttribute_Mapping)
        {
            throw new NotImplementedException();
        }

        public Task<ProductAttributeCombination> GetAllProductAttributeCombinationAsync(int productId)
        {
            throw new NotImplementedException();
        }

        public Task<Product_ProductAttribute_Mapping> GetAllProductAttributeMappingAsync(int productId)
        {
            throw new NotImplementedException();
        }

        public Task<List<ProductAttribute>> GetAllProductAttributesAsync()
        {
            throw new NotImplementedException();
        }
    }
}