using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MainApi.Persistence.Data;
using MainApi.Application.Interfaces;
using MainApi.Domain.Models.Products.ProductAttributes;
using MainApi.Domain.Models.Products.SpecificationAttributes;
using Microsoft.EntityFrameworkCore;
using MainApi.Application.Interfaces.Repositories;

namespace MainApi.Persistence.Repository
{
    public class SpecificationAttributesRepository : ISpecificationAttributesRepository
    {
        private readonly ApplicationDbContext _context;
        public SpecificationAttributesRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<SpecificationAttribute?> AddSpecificationAttributeAsync(SpecificationAttribute specificationAttribute)
        {
            await _context.SpecificationAttributes.AddAsync(specificationAttribute);
            await _context.SaveChangesAsync();
            return specificationAttribute;
        }

        public async Task<SpecificationAttributeOption?> AddSpecificationOptionAsync(SpecificationAttributeOption option)
        {
            await _context.SpecificationAttributeOptions.AddAsync(option);
            await _context.SaveChangesAsync();
            return option;
        }

        public async Task<Product_SpecificationAttribute_Mapping?> AssignSpecificationToProductAsync(Product_SpecificationAttribute_Mapping product_Specification)
        {
            Product_SpecificationAttribute_Mapping? mappedModel = await _context.SpecificationAttributeMappings
            .FirstOrDefaultAsync(m => m.ProductId == product_Specification.ProductId && m.SpecificationAttributeOptionId == product_Specification.SpecificationAttributeOptionId);
            if (mappedModel == null)
            {
                await _context.SpecificationAttributeMappings.AddAsync(product_Specification);
                await _context.SaveChangesAsync();
                return product_Specification;
            }
            return null;
        }

        public async Task<List<SpecificationAttribute>> GetAllSpecificationAttributesAsync()
        {
            List<SpecificationAttribute> specificationAttributes = await _context.SpecificationAttributes.Include(o => o.SpecificationAttributeOptions).ToListAsync();
            return specificationAttributes;
        }

        public async Task<List<SpecificationAttributeOption>?> GetProductSpecificationAttributesByProductIdAsync(int productId)
        {
            List<Product_SpecificationAttribute_Mapping> product_Specifications = await _context.SpecificationAttributeMappings.Include(o => o.SpecificationAttributeOption).ThenInclude(a => a.SpecificationAttribute)
            .Where(m => m.ProductId == productId).ToListAsync();
            List<SpecificationAttributeOption?> options = product_Specifications.Select(m => m.SpecificationAttributeOption).ToList();
            if (options == null)
            {
                return null;
            }
            return options;
        }

        public async Task<bool> SpecificationAttributeExistsAsync(string name)
        {
            SpecificationAttribute? specificationAttribute = await _context.SpecificationAttributes.FirstOrDefaultAsync(s => s.Name.ToLower() == name.ToLower());
            if (specificationAttribute == null)
            {
                return false;
            }
            return true;
        }

        public async Task<SpecificationAttribute?> GetSpecificationAttributeByName(string name)
        {
            SpecificationAttribute? specificationAttribute = await _context.SpecificationAttributes.FirstOrDefaultAsync(s => s.Name.ToLower() == name.ToLower());
            if (specificationAttribute == null)
            {
                return null;
            }
            return specificationAttribute;
        }

        public async Task<SpecificationAttributeOption?> GetSpecificationAttributeOptionById(int optionId)
        {
            SpecificationAttributeOption? option = await _context.SpecificationAttributeOptions.FirstOrDefaultAsync(o => o.Id == optionId);
            if (option == null)
            {
                return null;
            }
            return option;
        }

        public async Task<bool> DeleteSpecificationAttributeAsync(int specificationAttributeId)
        {
            SpecificationAttribute? specification = await _context.SpecificationAttributes.FindAsync(specificationAttributeId);
            if (specification == null)
            {
                return false;
            }
            _context.Remove(specification);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteSpecificationOptionAsync(int specificationOptionId)
        {
            SpecificationAttributeOption? option = await _context.SpecificationAttributeOptions.FindAsync(specificationOptionId);
            if (option == null)
            {
                return false;
            }
            _context.Remove(option);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAssignedSpecificationAsync(int mappingId)
        {
            Product_SpecificationAttribute_Mapping? mappedModel = await _context.SpecificationAttributeMappings.FindAsync(mappingId);
            if (mappedModel == null)
            {
                return false;
            }
            _context.Remove(mappedModel);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}