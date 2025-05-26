using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MainApi.Domain.Models.Products;
using MainApi.Domain.Models.Products.ProductAttributes;
using MainApi.Domain.Models.Products.SpecificationAttributes;

namespace MainApi.Application.Interfaces.Repositories
{
    public interface ISpecificationAttributesRepository
    {
        Task<List<SpecificationAttribute>> GetAllSpecificationAttributesAsync();
        Task<List<SpecificationAttributeOption>> GetProductSpecificationAttributesByProductIdAsync(int productId);
        Task<SpecificationAttribute> AddSpecificationAttributeAsync(SpecificationAttribute specificationAttribute);
        Task<SpecificationAttributeOption> AddSpecificationOptionAsync(SpecificationAttributeOption option);
        Task<Product_SpecificationAttribute_Mapping?> AssignSpecificationToProductAsync(Product_SpecificationAttribute_Mapping product_SpecificationAttribute_Mapping);
        Task<bool> SpecificationAttributeExistsAsync(string name);
        Task<SpecificationAttribute?> GetSpecificationAttributeByIdAsync(int specificationId);
        Task<SpecificationAttribute?> GetSpecificationAttributeByNameAsync(string specificationName);

        Task<SpecificationAttributeOption?> GetSpecificationAttributeOptionByIdAsync(int optionId);
        Task<Product_SpecificationAttribute_Mapping?> GetMappedSpecificationByIdAsync(int mappedId);
        Task DeleteSpecificationAttributeAsync(SpecificationAttribute specificationAttribute);
        Task DeleteSpecificationOptionAsync(SpecificationAttributeOption specificationAttributeOption);
        Task DeleteAssignedSpecificationAsync(Product_SpecificationAttribute_Mapping product_SpecificationAttribute_Mapping);



    }
}