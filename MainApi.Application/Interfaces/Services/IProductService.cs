using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MainApi.Application.Dtos.Address;
using MainApi.Application.Dtos.Filtering;
using MainApi.Application.Dtos.Products;
using MainApi.Domain.Models.Products;

namespace MainApi.Application.Interfaces.Services
{
    public interface IProductService
    {
        Task<List<ProductDto>> GetAllProductsAsync(ProductSortingDto productSortingDto, ProductFilteringDto productFilteringDto);
        Task<ProductDto> GetProductByIdAsync(int productId);
        Task<ProductDto> AddProductAsync(CreateProductRequestDto createProductRequestDto, int categoryId);
        Task UpdateProductAsync(int productId, UpdateProductRequestDto updateProductRequestDto, int categoryId);
        Task DeleteProductAsync(int productId);
    }
}