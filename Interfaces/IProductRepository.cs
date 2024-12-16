using System;
using MainApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace MainApi.Interfaces;

public interface IProductRepository
{
    Task<List<Product>?> GetAllProductsAsync();
    Task<Product?> GetProductByIdAsync(int productId);
    Task<Product?> AddProductAsync(Product product);
    Task<Product?> RemoveProductAsync(int productId);
    Task<Product?> UpdateProductAsync(Product product, int productId);
    Task<Boolean> ProductExistsAsync(int productId);
}
