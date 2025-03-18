using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MainApi.Models.Products;
using MainApi.Models.Products.ProductAttributes;
using MainApi.Models.Products.SpecificationAttributes;

namespace MainApi.Interfaces
{
    public interface ISpecificationAttributesRepository
    {
        List<Task<SpecificationAttribute>> GetAllSpecificationAttributesAsync();
        List<Task<SpecificationAttributeOption>> GetProductAttributesByProductIdAsync(int productId);
        Task<SpecificationAttribute> AddSpecificationAttributeAsync(SpecificationAttribute specificationAttribute);
        Task<SpecificationAttributeOption> AddSpecificationOptionAsync(SpecificationAttributeOption option);
        Task<Product_SpecificationAttribute_Mapping> AssignSpecificationToProductAsync(Product_SpecificationAttribute_Mapping product_SpecificationAttribute_Mapping);
    }
}