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

        public Task<SpecificationAttributeOption?> AddSpecificationOptionAsync(SpecificationAttributeOption option)
        {
            throw new NotImplementedException();
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
    }
}