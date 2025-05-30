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

        public async Task<SpecificationAttribute> AddSpecificationAttributeAsync(SpecificationAttribute specificationAttribute)
        {
            await _context.SpecificationAttributes.AddAsync(specificationAttribute);
            await _context.SaveChangesAsync();
            return specificationAttribute;
        }

        public async Task<SpecificationAttributeOption> AddSpecificationOptionAsync(SpecificationAttributeOption option)
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

        public async Task<List<SpecificationAttributeOption>> GetProductSpecificationAttributesByProductIdAsync(int productId)
        {
            List<Product_SpecificationAttribute_Mapping> product_Specifications = await _context.SpecificationAttributeMappings.Include(o => o.SpecificationAttributeOption).ThenInclude(a => a.SpecificationAttribute)
            .Where(m => m.ProductId == productId).ToListAsync();
            List<SpecificationAttributeOption?> options = product_Specifications.Select(m => m.SpecificationAttributeOption).ToList();
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

        public async Task<SpecificationAttribute?> GetSpecificationAttributeByIdAsync(int specificationId)
        {
            SpecificationAttribute? specificationAttribute = await _context.SpecificationAttributes.FirstOrDefaultAsync(s => s.Id == specificationId);
            if (specificationAttribute == null)
            {
                return null;
            }
            return specificationAttribute;
        }

        public async Task<SpecificationAttributeOption?> GetSpecificationAttributeOptionByIdAsync(int optionId)
        {
            SpecificationAttributeOption? option = await _context.SpecificationAttributeOptions.FirstOrDefaultAsync(o => o.Id == optionId);
            if (option == null)
            {
                return null;
            }
            return option;
        }

        public async Task DeleteSpecificationAttributeAsync(SpecificationAttribute specificationAttribute)
        {
            _context.Remove(specificationAttribute);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteSpecificationOptionAsync(SpecificationAttributeOption specificationAttributeOption)
        {
            _context.Remove(specificationAttributeOption);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAssignedSpecificationAsync(Product_SpecificationAttribute_Mapping product_SpecificationAttribute_Mapping)
        {

            _context.Remove(product_SpecificationAttribute_Mapping);
            await _context.SaveChangesAsync();
        }


        public async Task<Product_SpecificationAttribute_Mapping?> GetMappedSpecificationByIdAsync(int mappedId)
        {
            Product_SpecificationAttribute_Mapping? product_SpecificationAttribute_Mapping = await _context.SpecificationAttributeMappings.FirstOrDefaultAsync(m => m.Id == mappedId);
            if (product_SpecificationAttribute_Mapping == null)
            {
                return null;
            }
            return product_SpecificationAttribute_Mapping;
        }

        public async Task<SpecificationAttribute?> GetSpecificationAttributeByNameAsync(string specificationName)
        {
            SpecificationAttribute? specificationAttribute = await _context.SpecificationAttributes.FirstOrDefaultAsync(s => s.Name.ToLower() == specificationName.ToLower());
            if (specificationAttribute == null)
            {
                return null;
            }
            return specificationAttribute;
        }
    }
}