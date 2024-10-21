using System;
using MainApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace MainApi.Interfaces;

public interface IProductRepository
{
    Task<List<Products>> GetAllProductsAsync();
    Task<Products> GetProductByIdAsync(int productId);
    Task<Products> AddProductAsync(Products product);
    Task<Products> RemoveProductAsync(int productId);
    Task<Products> UpdateProductAsync(Products product , int productId);
}
