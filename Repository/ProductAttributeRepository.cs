using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MainApi.Data;
using MainApi.Interfaces;
using MainApi.Models.Products.ProductAttributes;
using Microsoft.EntityFrameworkCore;

namespace MainApi.Repository
{
    public class ProductAttributeRepository : IProductAttributeRepository
    {
        private readonly ApplicationDbContext _context;
        public ProductAttributeRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<PredefinedProductAttributeValue> AddPredefinedProductAttributeValueAsync(PredefinedProductAttributeValue predefinedProductAttributeValue)
        {
            await _context.PredefinedProductAttributeValues.AddAsync(predefinedProductAttributeValue);
            await _context.SaveChangesAsync();
            return predefinedProductAttributeValue;
        }

        public async Task<ProductAttribute> AddProductAttributeAsync(ProductAttribute productAttribute)
        {
            await _context.ProductAttributes.AddAsync(productAttribute);
            await _context.SaveChangesAsync();
            return productAttribute;
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

        public async Task<List<ProductAttribute>> GetAllProductAttributesAsync()
        {
            return await _context.ProductAttributes.Include(a => a.PredefinedProductAttributeValues).ToListAsync();
        }

        public async Task<bool> PredefinedProductAttributeValueExistsByName(string name)
        {
            PredefinedProductAttributeValue? PredefinedProductAttributeValue = await _context.PredefinedProductAttributeValues.FirstOrDefaultAsync(a => a.Name.ToLower() == name.ToLower());
            if (PredefinedProductAttributeValue == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public async Task<bool> ProductAttributeExistsByName(string name)
        {
            ProductAttribute? productAttribute = await _context.ProductAttributes.FirstOrDefaultAsync(a => a.Name.ToLower() == name.ToLower());
            if (productAttribute == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}