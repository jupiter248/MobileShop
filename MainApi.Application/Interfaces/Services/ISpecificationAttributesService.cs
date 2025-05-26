using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MainApi.Application.Dtos.SpecificationAttributes;
using MainApi.Application.Mappers;
using MainApi.Domain.Models.Products.SpecificationAttributes;

namespace MainApi.Application.Interfaces.Services
{
    public interface ISpecificationAttributesService
    {
        Task<SpecificationAttributeDto> AddSpecificationAttributeAsync(AddSpecificationAttributeRequestDto addSpecificationAttributeRequestDto);
        Task<List<SpecificationAttributeDto>> GetAllSpecificationAttributesAsync();
        Task<SpecificationAttributeOptionDto> AddSpecificationAttributeOptionAsync(AddSpecificationAttributeOptionRequestDto addSpecificationAttributeOptionRequestDto);
        Task AssignSpecificationToProductAsync(AddAssignToProductRequestDto addAssignToProductRequestDto);
        Task<List<ProductSpecificationAttributeDto>> GetProductSpecificationOptionsAsync(int productId);
        Task DeleteSpecificationAsync(int specificationId);
        Task DeleteSpecificationOptionAsync(int specificationOptionId);
        Task DeleteSpecificationAssignedAsync(int assignedId);
    }
}