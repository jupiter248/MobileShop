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
        Task<List<SpecificationAttribute>> GetAllSpecificationAttributesAsync();
        Task<List<SpecificationAttributeOption>?> GetProductSpecificationAttributesByProductIdAsync(int productId);
        Task<SpecificationAttribute?> AddSpecificationAttributeAsync(SpecificationAttribute specificationAttribute);
        Task<SpecificationAttributeOption?> AddSpecificationOptionAsync(SpecificationAttributeOption option);
        Task<Product_SpecificationAttribute_Mapping?> AssignSpecificationToProductAsync(Product_SpecificationAttribute_Mapping product_SpecificationAttribute_Mapping);
        Task<bool> SpecificationAttributeExistsAsync(string name);
        Task<SpecificationAttribute?> GetSpecificationAttributeByName(string name);
        Task<SpecificationAttributeOption?> GetSpecificationAttributeOptionById(int optionId);
        Task<bool> DeleteSpecificationAttributeAsync(int specificationAttributeId);
        Task<bool> DeleteSpecificationOptionAsync(int specificationOptionId);
        Task<bool> DeleteAssignedSpecificationAsync(int mappingId);



    }
}