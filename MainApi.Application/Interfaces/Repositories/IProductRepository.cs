using System;
using MainApi.Application.Dtos.Filtering;
using MainApi.Domain.Models;
using MainApi.Domain.Models.Products;

namespace MainApi.Application.Interfaces.Repositories;

public interface IProductRepository
{
    Task<List<Product>?> GetAllProductsAsync(ProductSortingDto sortingDto, ProductFilteringDto filteringDto);
    Task<Product?> GetProductByIdAsync(int productId);
    Task<Product?> AddProductAsync(Product product);
    Task<Product?> RemoveProductAsync(int productId);
    Task<Product?> UpdateProductAsync(Product product, int productId);
    Task<bool> ProductExistsAsync(int productId);
}
