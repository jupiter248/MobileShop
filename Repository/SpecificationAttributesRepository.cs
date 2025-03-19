using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MainApi.Data;
using MainApi.Interfaces;
using MainApi.Models.Products.SpecificationAttributes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MainApi.Repository
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

        public Task<Product_SpecificationAttribute_Mapping?> AssignSpecificationToProductAsync(Product_SpecificationAttribute_Mapping product_SpecificationAttribute_Mapping)
        {
            throw new NotImplementedException();
        }

        public async Task<List<SpecificationAttribute>> GetAllSpecificationAttributesAsync()
        {
            List<SpecificationAttribute> specificationAttributes = await _context.SpecificationAttributes.Include(o => o.SpecificationAttributeOptions).ToListAsync();
            return specificationAttributes;
        }

        public Task<List<SpecificationAttributeOption>> GetProductSpecificationAttributesByProductIdAsync(int productId)
        {
            throw new NotImplementedException();
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
    }
}