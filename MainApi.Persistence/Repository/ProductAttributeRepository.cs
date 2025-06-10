using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MainApi.Persistence.Data;
using MainApi.Application.Interfaces;
using MainApi.Domain.Models.Products.ProductAttributes;
using Microsoft.EntityFrameworkCore;
using MainApi.Application.Interfaces.Repositories;

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

        public async Task<ProductAttributeMapping> AddProductAttributeMappingAsync(ProductAttributeMapping ProductAttributeMapping)
        {
            await _context.ProductAttributeMappings.AddAsync(ProductAttributeMapping);
            await _context.SaveChangesAsync();
            return ProductAttributeMapping;
        }

        public async Task DeletePredefinedProductAttributeValueAsync(PredefinedProductAttributeValue predefinedProductAttributeValue)
        {
            _context.Remove(predefinedProductAttributeValue);
            await _context.SaveChangesAsync();
        }



        public async Task DeleteProductAttributeAsync(ProductAttribute productAttribute)
        {
            _context.Remove(productAttribute);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteProductAttributeCombinationAsync(ProductCombination productCombination)
        {
            _context.Remove(productCombination);
            await _context.SaveChangesAsync();
        }

        public async Task<List<ProductCombination>> GetAllProductAttributeCombinationAsync(int productId)
        {
            return await _context.ProductCombinations.Include(c => c.CombinationAttributes)
            .ThenInclude(c => c.AttributeValue).ThenInclude(c => c.ProductAttribute)
            .Where(p => p.ProductId == productId).ToListAsync();
        }

        public async Task<List<ProductAttributeMapping>> GetAllProductAttributeMappingAsync(int productId)
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

        public async Task<PredefinedProductAttributeValue?> GetPredefinedAttributeValueByIdAsync(int predefinedProductAttributeValueId)
        {
            PredefinedProductAttributeValue? predefinedProductAttributeValue = await _context.PredefinedProductAttributeValues.FirstOrDefaultAsync(p => p.Id == predefinedProductAttributeValueId);
            if (predefinedProductAttributeValue == null)
            {
                return null;
            }
            return predefinedProductAttributeValue;
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

        public async Task<ProductCombination?> GetProductCombinationByIdAsync(int combinationId)
        {
            ProductCombination? productCombination = await _context.ProductCombinations.FirstOrDefaultAsync(c => c.Id == combinationId);
            if (productCombination == null)
            {
                return null;
            }
            return productCombination;
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