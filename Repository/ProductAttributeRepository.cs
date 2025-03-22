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
        public async Task<ProductAttributeCombination> AddProductAttributeCombinationAsync(ProductAttributeCombination productAttributeCombination)
        {
            await _context.ProductAttributeCombinations.AddAsync(productAttributeCombination);
            await _context.SaveChangesAsync();
            return productAttributeCombination;
        }

        public async Task<Product_ProductAttribute_Mapping> AddProductAttributeMappingAsync(Product_ProductAttribute_Mapping product_ProductAttribute_Mapping)
        {
            await _context.ProductAttributeMappings.AddAsync(product_ProductAttribute_Mapping);
            await _context.SaveChangesAsync();
            return product_ProductAttribute_Mapping;
        }

        public async Task<List<ProductAttributeCombination>> GetAllProductAttributeCombinationAsync(int productId)
        {
            return await _context.ProductAttributeCombinations.Where(p => p.ProductId == productId).ToListAsync();
        }

        public async Task<List<Product_ProductAttribute_Mapping>> GetAllProductAttributeMappingAsync(int productId)
        {
            return await _context.ProductAttributeMappings.Include(m => m.ProductAttribute).ThenInclude(a => a.PredefinedProductAttributeValues).Include(m => m.ProductAttributeValues).Where(m => m.ProductId == productId).ToListAsync();
        }

        public async Task<List<ProductAttribute>> GetAllProductAttributesAsync()
        {
            return await _context.ProductAttributes.Include(a => a.PredefinedProductAttributeValues).ToListAsync();
        }

        public async Task<List<PredefinedProductAttributeValue>> GetAttributeValuesById(List<int> Ids)
        {
            return await _context.PredefinedProductAttributeValues
            .Where(v => Ids.Contains(v.Id))
            .ToListAsync();
        }

        public async Task<ProductAttribute?> GetProductAttributeByIdAsync(int productAttributeId)
        {
            ProductAttribute? productAttribute = await _context.ProductAttributes.FirstOrDefaultAsync(a => a.Id == productAttributeId);
            if (productAttribute == null)
            {
                return null;
            }
            return productAttribute;
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