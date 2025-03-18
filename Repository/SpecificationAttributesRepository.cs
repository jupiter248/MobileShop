using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MainApi.Data;
using MainApi.Interfaces;
using MainApi.Models.Products.SpecificationAttributes;
using Microsoft.AspNetCore.Mvc;

namespace MainApi.Repository
{
    public class SpecificationAttributesRepository : ISpecificationAttributesRepository
    {
        private readonly ApplicationDbContext _context;
        public SpecificationAttributesRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public Task<SpecificationAttribute> AddSpecificationAttributeAsync(SpecificationAttribute specificationAttribute)
        {
            throw new NotImplementedException();
        }

        public Task<SpecificationAttributeOption> AddSpecificationOptionAsync(SpecificationAttributeOption option)
        {
            throw new NotImplementedException();
        }

        public Task<Product_SpecificationAttribute_Mapping> AssignSpecificationToProductAsync(Product_SpecificationAttribute_Mapping product_SpecificationAttribute_Mapping)
        {
            throw new NotImplementedException();
        }

        public List<Task<SpecificationAttribute>> GetAllSpecificationAttributesAsync()
        {
            throw new NotImplementedException();
        }

        public List<Task<SpecificationAttributeOption>> GetProductAttributesByProductIdAsync(int productId)
        {
            throw new NotImplementedException();
        }
    }
}