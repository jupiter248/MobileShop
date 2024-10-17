using System;
using MainApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace MainApi.Interfaces;

public interface IProductRepository
{
    Task<List<ProductModel>> GetAllProductsAsync();
    Task<ProductModel> GetProductByIdAsync(int productId);
}
