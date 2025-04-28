using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MainApi.Persistence.Data;
using MainApi.Application.Interfaces;
using MainApi.Domain.Models.Products.ProductAttributes;
using Microsoft.EntityFrameworkCore;

namespace MainApi.Persistence.Repository
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
        public async Task<ProductCombination> AddProductAttributeCombinationAsync(ProductCombination productAttributeCombination)
        {
            await _context.ProductCombinations.AddAsync(productAttributeCombination);
            await _context.SaveChangesAsync();
            return productAttributeCombination;
        }

        public async Task<Product_ProductAttribute_Mapping> AddProductAttributeMappingAsync(Product_ProductAttribute_Mapping product_ProductAttribute_Mapping)
        {
            await _context.ProductAttributeMappings.AddAsync(product_ProductAttribute_Mapping);
            await _context.SaveChangesAsync();
            return product_ProductAttribute_Mapping;
        }

        public async Task<PredefinedProductAttributeValue?> DeletePredefinedProductAttributeValue(int predefinedProductAttributeValueId)
        {
            PredefinedProductAttributeValue? value = await _context.PredefinedProductAttributeValues.FirstOrDefaultAsync(v => v.Id == predefinedProductAttributeValueId);
            if (value == null) return null;
            _context.Remove(value);
            await _context.SaveChangesAsync();
            return value;
        }

        public async Task<ProductAttribute?> DeleteProductAttribute(int productAttributeId)
        {
            ProductAttribute? value = await _context.ProductAttributes.FirstOrDefaultAsync(v => v.Id == productAttributeId);
            if (value == null) return null;
            _context.Remove(value);
            await _context.SaveChangesAsync();
            return value;
        }

        public async Task<ProductCombination?> DeleteProductAttributeCombination(int ProductAttributeCombinationId)
        {
            ProductCombination? value = await _context.ProductCombinations.FirstOrDefaultAsync(v => v.Id == ProductAttributeCombinationId);
            if (value == null) return null;
            _context.Remove(value);
            await _context.SaveChangesAsync();
            return value;
        }

        public async Task<List<ProductCombination>> GetAllProductAttributeCombinationAsync(int productId)
        {
            return await _context.ProductCombinations.Include(c => c.CombinationAttributes)
            .ThenInclude(c => c.AttributeValue).ThenInclude(c => c.ProductAttribute)
            .Where(p => p.ProductId == productId).ToListAsync();
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
            .Include(v => v.ProductAttribute)
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